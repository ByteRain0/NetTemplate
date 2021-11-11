using System;
using System.Threading;
using System.Threading.Tasks;
using ExecutionPipeline.MediatRPipeline.ExceptionHandling;
using Localization.Accessor.Service.Accessors.BaseLocalizationSourceAccessor;
using Localization.Accessor.Service.Accessors.CacheAccessor.Contracts;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace Localization.Accessor.Service.Accessors.CacheAccessor.Service
{
    public class CacheAccessorExceptionsHandler : ICacheLocalizationAccessor
    {
        private readonly ICacheLocalizationAccessor _instance;

        private readonly ILogger<CacheAccessorExceptionsHandler> _logger;

        public CacheAccessorExceptionsHandler(ICacheLocalizationAccessor instance, ILogger<CacheAccessorExceptionsHandler> logger)
        {
            _instance = instance;
            _logger = logger;
        }
        public async Task<Response<LocalizedString>> GetLocalizedString(GetLocalizationQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return await _instance.GetLocalizedString(request: request, cancellationToken: cancellationToken);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return Response.Fail<LocalizedString>(e.Message);
            }
        }
        public async Task<Response> UpsertLocalization(UpsertLocalizationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return await _instance.UpsertLocalization(request: request, cancellationToken: cancellationToken);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return Response.Fail(e.Message);
            }
        }
        public async Task<Response<bool>> IsResourceAvailable(CancellationToken cancellationToken)
        {
            try
            {
                return await _instance.IsResourceAvailable(cancellationToken);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return Response.Fail<bool>(e.Message);
            }
        }
        public async Task<Response> RemoveLocalization(RemoveLocalizationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return await _instance.RemoveLocalization(request: request, cancellationToken: cancellationToken);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return Response.Fail(e.Message);
            }
        }
        public async Task<Response<string>> GetLanguagePreference(CancellationToken cancellationToken)
        {
            try
            {
                return await _instance.GetLanguagePreference(cancellationToken);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return Response.Fail<string>(e.Message);
            }
        }
    }
}