using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading;
using ExecutionPipeline.MediatRPipeline.ExceptionHandling;
using Localization.Accessor.Service.Accessors.BaseLocalizationSourceAccessor;
using Localization.Accessor.Service.Accessors.CacheAccessor.Contracts;
using Localization.Accessor.Service.Accessors.FileAccessor.Contracts;
using Localization.Accessor.Service.Service.Infrastructure;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Localization.Accessor.Service.Service
{
    public partial class LocalizationEngine : IStringLocalizer
    {
        private readonly IFileLocalizationsAccessor _fileLocalizationsLocalizationsAccessor;

        private readonly ICacheLocalizationAccessor _cacheLocalization;

        private readonly ILogger<LocalizationEngine> _logger;

        private readonly IOptions<LocalizationStoreInformation> _localizationsStore;

        private string _currentLanguage;

        private const string _cache = "DistributedCache";

        private const string _cacheChekKey = "DistributedCacheCheckKey";

        private const string _cacheChekLanguage = "EN";

        public LocalizationEngine(
            IFileLocalizationsAccessor fileLocalizationsLocalizationsAccessor,
            ICacheLocalizationAccessor cacheLocalization,
            ILogger<LocalizationEngine> logger, IOptions<LocalizationStoreInformation> localizationsStore)
        {
            _fileLocalizationsLocalizationsAccessor = fileLocalizationsLocalizationsAccessor;
            _cacheLocalization = cacheLocalization;
            _logger = logger;
            _localizationsStore = localizationsStore;

            _cacheLocalization.GetLanguagePreference(CancellationToken.None)
                .Result
                .Act(onSuccess: result => { _currentLanguage = result.ToUpperInvariant()[..2]; });


            if (_currentLanguage.Length == 2 && _currentLanguage != _localizationsStore.Value.DefaultLanguage)
            {
                _currentLanguage = _localizationsStore.Value.Locales
                    .FirstOrDefault(x => x.Name.ToUpperInvariant() == _currentLanguage)?.Name;
            }
        }

        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
        {
            var response = new List<LocalizedString>();

            _cacheLocalization.UpsertLocalization(
                request: new UpsertLocalizationCommand()
                {
                    Key = _cacheChekKey,
                    Locale = _cacheChekLanguage,
                    Value = "checkTranslationValue"
                }, cancellationToken: CancellationToken.None);

            foreach (var lang in _localizationsStore.Value.Locales)
            {
                var fileExists = _fileLocalizationsLocalizationsAccessor.IsResourceAvailable(
                    request: new IsResourceAvailableQuery()
                    {
                        Locale = lang.Name
                    },
                    cancellationToken: CancellationToken.None).Result;

                if (fileExists.IsFailure)
                {
                    throw new CultureNotFoundException("File missing for selected locale");
                }

                if (fileExists.Value)
                {
                    var fileLocalizations =
                        _fileLocalizationsLocalizationsAccessor.GetLocalizations(new GetLocalizationsQuery()
                        {
                            Locale = lang.Name
                        }, CancellationToken.None).Result;

                    if (fileLocalizations.IsFailure)
                    {
                        throw new InvalidOperationException(fileLocalizations.StackTrace);
                    }

                    foreach (var fileLocalization in fileLocalizations.Value)
                    {
                        response.Add(fileLocalization);
                        _cacheLocalization.UpsertLocalization(
                            request: new UpsertLocalizationCommand()
                            {
                                Key = fileLocalization.Name,
                                Locale = lang.Name,
                                Value = fileLocalization.Value
                            }, cancellationToken: CancellationToken.None);
                    }
                }
                else
                {
                    _logger.LogCritical($"File missing for selected locale : {lang.Name}");
                }
            }

            return response;
        }

        [Obsolete("Method not used anywhere, and obsolete from interface inheritance")]
        public IStringLocalizer WithCulture(CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public LocalizedString this[string name]
        {
            get
            {
                //Check if the localization dictionaries were cached by checking on the default localization marker.
                Response<LocalizedString> cachedTranslationCheck = _cacheLocalization.GetLocalizedString(
                    request: new GetLocalizationQuery()
                    {
                        Key = _cacheChekKey,
                        Locale = _cacheChekLanguage
                    }, cancellationToken: CancellationToken.None).Result;

                if (cachedTranslationCheck.IsSuccess && cachedTranslationCheck.Value != null)
                {
                    GetAllStrings(false);
                }

                var cachedTranslation = _cacheLocalization
                    .GetLocalizedString(
                        request: new GetLocalizationQuery()
                        {
                            Key = name,
                            Locale = _currentLanguage
                        }, cancellationToken: CancellationToken.None).Result;

                if (cachedTranslation.IsSuccess && cachedTranslation.Value != null)
                {
                    return new LocalizedString(
                        name: name,
                        value: cachedTranslation.Value.Value,
                        resourceNotFound: false,
                        searchedLocation: _cache);
                }
                else if (cachedTranslation.IsFailure)
                {
                    _logger.LogCritical(message:$"Error encountered during fetch of cached lexeme for key :'{name}' , error : '{cachedTranslation.StackTrace}'");
                }
                return new LocalizedString(name, $"[{name}]", true);
            }
        }

        public LocalizedString this[string name, params object[] arguments]
        {
            get
            {
                if (arguments.Length == 1 && (arguments[0] is string))
                {
                    _currentLanguage = arguments.First().ToString()?.ToUpperInvariant()[..2];
                    if (_currentLanguage.Length == 2 && _currentLanguage != _localizationsStore.Value.DefaultLanguage)
                    {
                        _currentLanguage = _localizationsStore.Value.Locales
                            .FirstOrDefault(x => x.Name.ToUpperInvariant() == _currentLanguage)?.Name;
                    }
                    return this[name];
                }

                if (arguments.Length == 1 && (arguments[0] is DefaultValueParam))
                {
                    var defaultValueParam = (arguments[0] as DefaultValueParam);

                    if (!string.IsNullOrWhiteSpace(defaultValueParam.Language))
                    {
                        _currentLanguage = _localizationsStore.Value.Locales
                            .FirstOrDefault(x => x.Name.ToUpperInvariant() == defaultValueParam.Language)?.Name;
                    }

                    var result = this[name];

                    if (!string.IsNullOrWhiteSpace(defaultValueParam.Value) && result.Value == $"[{name}]")
                    {
                        result = new LocalizedString(name, defaultValueParam.Value);
                    }

                    return result;
                }

                return this[name];
            }
        }
    }
}