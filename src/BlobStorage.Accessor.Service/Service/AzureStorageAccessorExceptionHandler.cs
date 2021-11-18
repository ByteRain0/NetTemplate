using System;
using System.Threading;
using System.Threading.Tasks;
using BlobStorage.Accessor.Contracts;
using BlobStorage.Accessor.Contracts.Commands;
using BlobStorage.Accessor.Contracts.Queries;
using ExecutionPipeline.MediatRPipeline.ExceptionHandling;
using Microsoft.Extensions.Logging;

namespace BlobStorage.Accessor.Service.Service
{
    public class AzureStorageAccessorExceptionHandler : IStorageAccessor
    {
        private readonly IStorageAccessor _instance;
        private readonly ILogger<AzureStorageAccessorExceptionHandler> _logger;
        
        public AzureStorageAccessorExceptionHandler(IStorageAccessor instance, ILogger<AzureStorageAccessorExceptionHandler> logger)
        {
            _instance = instance;
            _logger = logger;
        }

        public async Task<Response> Upload(UploadContentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return await _instance.Upload(request: request, cancellationToken: cancellationToken);
            }
            catch (Exception e)
            {
                _logger.LogCritical(e,e.Message);
                return Response.Fail(e.Message);
            }
        }

        public async Task<Response<string>> Download(DownloadContentQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return await _instance.Download(request: request, cancellationToken: cancellationToken);
            }
            catch (Exception e)
            {
                _logger.LogCritical(e,e.Message);
                return Response.Fail<string>(e.Message);
            }
        }
    }
}