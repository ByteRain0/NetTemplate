using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using ExecutionPipeline.MediatRPipeline.ExceptionHandling;
using Localization.Accessor.Service.Accessors.BaseLocalizationSourceAccessor;
using Localization.Accessor.Service.Accessors.CacheAccessor.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;

namespace Localization.Accessor.Service.Accessors.CacheAccessor.Service
{
    public class CacheAccessor : ICacheLocalizationAccessor
    {
        private readonly IOptions<CacheLocalizationConfig> _options;

        private readonly IDistributedCache _cache;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public CacheAccessor(IHttpContextAccessor httpContextAccessor, IDistributedCache cache,
            IOptions<CacheLocalizationConfig> options)
        {
            _httpContextAccessor = httpContextAccessor;
            _cache = cache;
            _options = options;
        }

        public async Task<Response<LocalizedString>> GetLocalizedString(GetLocalizationQuery request, CancellationToken cancellationToken)
        {
            request.Locale = request.Locale.StripLocale();

            var cacheKey = GetObjectLockKey(key: request.Key, language: request.Locale);

            var value = await _cache.GetStringAsync(cacheKey, token: cancellationToken);

            if (string.IsNullOrEmpty(value))
            {
                return Response.Ok<LocalizedString>(default);
            }

            return Response.Ok(new LocalizedString(name: request.Key, value: value));
        }

        public async Task<Response> UpsertLocalization(UpsertLocalizationCommand request, CancellationToken cancellationToken)
        {
            request.Locale = request.Locale.StripLocale();

            var cacheKey = GetObjectLockKey(key: request.Key, language: request.Locale);

            await _cache.SetStringAsync(
                key: cacheKey,
                value: request.Value,
                options: new DistributedCacheEntryOptions
                {
                    AbsoluteExpiration = DateTimeOffset.MaxValue
                },
                token: cancellationToken);

            return Response.Ok();
        }

        /// <param name="key">EntityType_EntityKey</param>
        public async Task<Response> RemoveLocalization(RemoveLocalizationCommand request, CancellationToken cancellationToken)
        {
            request.Locale = request.Locale.StripLocale();
            var cacheKey = GetObjectLockKey(key: request.Key, language: request.Locale);
            await _cache.RemoveAsync(key: cacheKey, token: cancellationToken);
            return Response.Ok();
        }

        public Task<Response<bool>> IsResourceAvailable(CancellationToken cancellationToken)
        {
            var isContextAvailable = _httpContextAccessor != null;
            return Task.FromResult(Response.Ok(isContextAvailable));
        }

        public Task<Response<string>> GetLanguagePreference(CancellationToken cancellationToken)
        {
            var defaultLocale = _options.Value.DefaultLanguage;
            var cachedLocale = _httpContextAccessor.HttpContext != null
                ? _httpContextAccessor.HttpContext.Session.GetString(_options.Value.SessionStoreKeyName)
                : defaultLocale;
            return Task.FromResult(Response.Ok<string>(string.IsNullOrEmpty(cachedLocale)
                ? defaultLocale.ToUpperInvariant()
                : cachedLocale.ToUpperInvariant()));
        }

        private string GetObjectLockKey(string key, string language) =>
            $"{_options.Value.SessionStoreKeyName}_{language.ToUpperInvariant()}_{key.ToLowerInvariant()}";
    }
}