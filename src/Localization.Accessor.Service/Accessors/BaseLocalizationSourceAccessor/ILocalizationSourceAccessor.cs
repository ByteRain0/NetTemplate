using System.Threading;
using System.Threading.Tasks;
using ExecutionPipeline.MediatRPipeline.ExceptionHandling;
using Localization.Accessor.Service.Accessors.BaseLocalizationSourceAccessor.Commands;
using Localization.Accessor.Service.Accessors.BaseLocalizationSourceAccessor.Queries;
using Microsoft.Extensions.Localization;

namespace Localization.Accessor.Service.Accessors.BaseLocalizationSourceAccessor
{
    public interface ILocalizationSourceAccessor
    {
        /// <summary>
        /// Get Specific localization.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Response<LocalizedString>> GetLocalizedString(GetLocalizationQuery request, CancellationToken cancellationToken);
        
        /// <summary>
        /// Update or Insert new localization data.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Response> UpsertLocalization(UpsertLocalizationCommand request, CancellationToken cancellationToken);
        
        /// <summary>
        /// Check if resource is healthy and available for requests.
        /// </summary>
        /// <returns></returns>
        Task<Response<bool>> IsResourceAvailable(CancellationToken cancellationToken);
        
        /// <summary>
        /// Remove specific localization
        /// </summary>
        /// <returns></returns>
        Task<Response> RemoveLocalization(RemoveLocalizationCommand request, CancellationToken cancellationToken);
        
        /// <summary>
        /// Get language preference for User / Application.
        /// </summary>
        /// <returns></returns>
        Task<Response<string>> GetLanguagePreference(CancellationToken cancellationToken);
    }
}