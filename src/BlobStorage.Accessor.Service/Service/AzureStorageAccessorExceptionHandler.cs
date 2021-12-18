using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BlobStorage.Accessor.Contracts;
using BlobStorage.Accessor.Contracts.Commands.DeleteItem;
using BlobStorage.Accessor.Contracts.Commands.UploadContent;
using BlobStorage.Accessor.Contracts.Queries.DownloadContent;
using BlobStorage.Accessor.Contracts.Queries.ListItems;
using ExecutionPipeline.MediatRPipeline.ExceptionHandling;
using Microsoft.Extensions.Logging;

namespace BlobStorage.Accessor.Service.Service;

public class AzureStorageAccessorExceptionHandler : IStorageAccessor
{
    private readonly IStorageAccessor _instance;
    private readonly ILogger<AzureStorageAccessorExceptionHandler> _logger;
        
    public AzureStorageAccessorExceptionHandler(IStorageAccessor instance, ILogger<AzureStorageAccessorExceptionHandler> logger)
    {
        _instance = instance;
        _logger = logger;
    }

    public async Task<Response> Upload(UploadItemCommand request, CancellationToken cancellationToken)
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

    public async Task<Response<string>> Download(DownloadItemQuery request, CancellationToken cancellationToken)
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

    public async Task<Response> DeleteFile(DeleteItemCommand request, CancellationToken cancellationToken)
    {
        try
        {
            return await _instance.DeleteFile(request: request, cancellationToken: cancellationToken);
        }
        catch (Exception e)
        {
            _logger.LogCritical(e,e.Message);
            return Response.Fail<string>(e.Message);
        }
    }

    public async Task<Response<List<string>>> ListItems(ListFilesQuery request, CancellationToken cancellationToken)
    {
        try
        {
            return await _instance.ListItems(request: request, cancellationToken: cancellationToken);
        }
        catch (Exception e)
        {
            _logger.LogCritical(e,e.Message);
            return Response.Fail<List<string>>(e.Message);
        }
    }
}