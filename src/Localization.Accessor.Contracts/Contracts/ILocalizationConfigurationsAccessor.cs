using System.Collections.Generic;
using System.Threading.Tasks;
using ExecutionPipeline.MediatRPipeline.ExceptionHandling;

namespace Localization.Accessor.Contracts.Contracts
{
    public interface ILocalizationConfigurationsAccessor
    {
        Task<Response<HashSet<Locale>>> GetAvailableLocales();

        Task<Response<string>> DefaultLocale();
    }
}