using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using BlobStorage.Accessor.Contracts;
using BlobStorage.Accessor.Contracts.Commands;
using BlobStorage.Accessor.Contracts.Commands.DeleteItem;
using BlobStorage.Accessor.Contracts.Commands.UploadContent;
using BlobStorage.Accessor.Contracts.Queries;
using BlobStorage.Accessor.Contracts.Queries.DownloadContent;
using BlobStorage.Accessor.Contracts.Queries.ListItems;
using BlobStorage.Accessor.Service.Infrastructure;
using ExecutionPipeline.MediatRPipeline.ExceptionHandling;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BlobStorage.Accessor.Service.Service;

public class AzureStorageAccessor : IStorageAccessor
{
    private BlobContainerClient _blobContainer;

        
    public AzureStorageAccessor(IOptions<AzureStorageConfigs> options, ILogger<AzureStorageAccessor> logger)
    {
        _blobContainer = new BlobServiceClient(options.Value.ConnectionString)
            .GetBlobContainerClient(options.Value.ContainerName.ToLowerInvariant());
        _blobContainer.CreateIfNotExists();
    }

    public async Task<Response> Upload(UploadItemCommand request, CancellationToken cancellationToken)
    {
        if (!request.Stream.IsBase64String())
        {
            request.Stream = request.Stream.EncodeToBase64();
        }

        BlobClient blobClient = _blobContainer.GetBlobClient(request.GetFilePath);

        byte[] data = Encoding.ASCII.GetBytes(request.Stream);

        await using (var stream = new MemoryStream(data, writable: false))
        {
            var operation = await blobClient.UploadAsync(stream,
                new BlobHttpHeaders()
                {
                    ContentType = request.ContentType
                },
                request.Tags, cancellationToken: cancellationToken);
        }
        return Response.Ok();
    }

    public async Task<ExecutionPipeline.MediatRPipeline.ExceptionHandling.Response<string>> Download(DownloadItemQuery request, CancellationToken cancellationToken)
    {
        BlobClient blob = _blobContainer.GetBlobClient(request.GetSingleFilePath);
            
        BlobDownloadInfo blobData = await blob.DownloadAsync(cancellationToken:cancellationToken);

        StreamReader reader = new StreamReader(blobData.Content);
        string result = await reader.ReadToEndAsync();

        if (result.IsBase64String())
        {
            return Response.Ok(value:result);
        }

        return Response.Ok(value:result.EncodeToBase64());
    }

    public async Task<ExecutionPipeline.MediatRPipeline.ExceptionHandling.Response<List<string>>> ListItems(ListFilesQuery request, CancellationToken cancellationToken)
    {
        var blobs =  _blobContainer.GetBlobs();
        var list = blobs.Select(x => x.Name).ToList();
        return Response.Ok(value:list);
    }

    public async Task<Response> DeleteFile(DeleteItemCommand request, CancellationToken cancellationToken)
    {
        var operation = await _blobContainer.DeleteBlobAsync(request.GetFilePath, cancellationToken: cancellationToken);

        if (operation.Status.IsSuccessStatusCode())
        {
            return Response.Ok();
        }

        return Response.Fail(
            message: $"Error deleting file on path : '{request.GetFilePath}'. Error : '{operation.ReasonPhrase}'.",
            code: (HttpStatusCode)operation.Status);
    }
}