using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ExecutionPipeline.MediatRPipeline.ExceptionHandling;
using Localization.Accessor.Service.Accessors.BaseLocalizationSourceAccessor;
using Localization.Accessor.Service.Accessors.FileAccessor.Contracts.Queries;
using Microsoft.Extensions.Localization;

namespace Localization.Accessor.Service.Accessors.FileAccessor.Contracts
{
    public interface IFileLocalizationsAccessor : ILocalizationSourceAccessor
    {
        /// <summary>
        /// Get all available localizations.
        /// </summary>
        /// <returns></returns>
        Task<Response<List<LocalizedString>>> GetLocalizations(GetLocalizationsQuery request, CancellationToken cancellationToken);

        Task<Response<bool>> IsResourceAvailable(IsResourceAvailableQuery request, CancellationToken cancellationToken);
    }
}