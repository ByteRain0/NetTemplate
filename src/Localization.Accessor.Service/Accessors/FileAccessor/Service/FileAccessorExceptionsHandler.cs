using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ExecutionPipeline.MediatRPipeline.ExceptionHandling;
using Localization.Accessor.Service.Accessors.BaseLocalizationSourceAccessor;
using Localization.Accessor.Service.Accessors.FileAccessor.Contracts;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace Localization.Accessor.Service.Accessors.FileAccessor.Service
{
    public class FileAccessorExceptionsHandler : IFileAccessor
    {
        private readonly IFileAccessor _instance;

        private readonly ILogger<FileAccessorExceptionsHandler> _logger;

        public FileAccessorExceptionsHandler(ILogger<FileAccessorExceptionsHandler> logger, IFileAccessor instance)
        {
            _logger = logger;
            _instance = instance;
        }

        public async Task<Response<LocalizedString>> GetLocalizedString(GetLocalizationQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return await _instance.GetLocalizedString(request, cancellationToken);
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
                return await _instance.UpsertLocalization(request, cancellationToken);
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
                return await _instance.RemoveLocalization(request, cancellationToken);
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

        public async Task<Response<List<LocalizedString>>> GetLocalizations(GetLocalizationsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return await _instance.GetLocalizations(request, cancellationToken);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return Response.Fail<List<LocalizedString>>(e.Message);
            }
        }

        public async Task<Response<bool>> IsResourceAvailable(IsAvailableQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return await _instance.IsResourceAvailable(request, cancellationToken);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return Response.Fail<bool>(e.Message);
            }
        }
    }
}