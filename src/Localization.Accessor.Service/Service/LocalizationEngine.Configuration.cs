using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ExecutionPipeline.MediatRPipeline.ExceptionHandling;
using Localization.Accessor.Contracts.Contracts;

namespace Localization.Accessor.Service.Service
{
    /// <summary>
    /// Configuration part taken into separate partial class.
    /// </summary>
    public partial class LocalizationEngine : ILocalizationConfigurationsAccessor
    {
        public async Task<Response<HashSet<Locale>>> GetAvailableLocales()
        {
            return Response.Ok(_lexemesStore.Value.Locales);
        }

        public Task<Response<string>> DefaultLocale()
        {
            return _cacheLocalization.GetLanguagePreference(CancellationToken.None);
        }
    }
}