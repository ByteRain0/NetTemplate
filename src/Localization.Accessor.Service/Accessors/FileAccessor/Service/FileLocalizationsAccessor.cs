using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using ExecutionPipeline.MediatRPipeline.ExceptionHandling;
using Localization.Accessor.Service.Accessors.BaseLocalizationSourceAccessor;
using Localization.Accessor.Service.Accessors.FileAccessor.Contracts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Localization.Accessor.Service.Accessors.FileAccessor.Service
{

    /// <summary>
    /// Accessor that works with specific files.
    /// </summary>
    public partial class FileLocalizationsAccessor : IFileLocalizationsAccessor
    {
        private readonly IHostingEnvironment _env;
        
        private readonly string _path;

        private readonly string _defaultLocale;

        private readonly ILogger<FileAccess> _logger;

        public FileLocalizationsAccessor(IHostingEnvironment env, IOptions<FileLocalizationConfig> locConfig, ILogger<FileAccess> logger)
        {
            _env = env;
            _logger = logger;
            _path = locConfig.Value.Path;
            _defaultLocale = locConfig.Value.DefaultLocale;
        }

        private T PullDeserialize<T>(string propertyName, Stream str)
        {
            if (propertyName == null)
                throw new System.ArgumentNullException(nameof(propertyName));

            if (str == null)
                throw new System.ArgumentNullException(nameof(str));

            using (str)
            using (var sReader = new StreamReader(str))
            using (var reader = new JsonTextReader(sReader))
            {
                while (reader.Read())
                {
                    if (reader.TokenType == JsonToken.PropertyName
                        && reader.Value.ToString().ToLowerInvariant() == propertyName.ToLowerInvariant())
                    {
                        reader.Read();
                        var serializer = new JsonSerializer();
                        return serializer.Deserialize<T>(reader);
                    }
                }

                return default(T);
            }
        }

        private string GetFilePath(string language)
        {
            var paths = new string[]
            {
                _env.ContentRootPath,
                $"{_path}/{language}.json"
            };
            return Path.Combine(paths);
        }
        
        [Obsolete("For this resource use the overload with the specific locale.")]
        public Task<Response<bool>> IsResourceAvailable(CancellationToken cancellationToken)
        {
            return Task.FromResult(Response.Fail<bool>("For this resource use the overload with the specific locale."));
        }
        
        public Task<Response<bool>> IsResourceAvailable(IsResourceAvailableQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var fileCheck = new FileStream(GetFilePath(request.Locale), FileMode.Open, FileAccess.Read, FileShare.Read);
                return Task.FromResult(Response.Ok(true));
            }
            catch (Exception e)
            {
                _logger.LogError(e,e.Message);
                return Task.FromResult(Response.Ok(false));
            }
        }
        
        public Task<Response<LocalizedString>> GetLocalizedString(GetLocalizationQuery request, CancellationToken cancellationToken)
        {
            Stream fileStream = new FileStream(GetFilePath(request.Locale), FileMode.Open, FileAccess.Read, FileShare.Read);
            var lexeme = new LocalizedString(request.Key, PullDeserialize<string>(request.Key, fileStream), false);
            return Task.FromResult(Response.Ok(lexeme));
        }

        [Obsolete("Method not currently supported")]
        public Task<Response> UpsertLocalization(UpsertLocalizationCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(Response.Fail("Method not currently supported"));
        }
        
        public Task<Response> RemoveLocalization(RemoveLocalizationCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(Response.Fail("Operation not available for this resource at the present moment", HttpStatusCode.Forbidden));
        }

        public Task<Response<string>> GetLanguagePreference(CancellationToken cancellationToken)
        {
            return Task.FromResult(Response.Ok<string>(_defaultLocale));
        }

        public Task<Response<List<LocalizedString>>> GetLocalizations(GetLocalizationsQuery request, CancellationToken cancellationToken)
        {
            var filePath = GetFilePath(request.Locale);

            var resultList = new List<LocalizedString>();

            using (Stream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (var sReader = new StreamReader(stream))
                {
                    using (var reader = new JsonTextReader(sReader))
                    {
                        reader.SupportMultipleContent = true;
                        var obj = JObject.Load(reader);
                        var properties = obj.Properties();
                        foreach (var property in properties)
                        {
                            var value = property.Value?.Value<string>();
                            var name = property.Name;
                            resultList.Add(new LocalizedString(name, value ?? $"[{name}]", property.Value == null));
                        }
                    } 
                }
            }
            
            return Task.FromResult(Response.Ok(resultList));
        }
    }
}