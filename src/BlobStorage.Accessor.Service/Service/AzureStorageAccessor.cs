using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using BlobStorage.Accessor.Contracts;
using BlobStorage.Accessor.Contracts.Commands;
using BlobStorage.Accessor.Contracts.Queries;
using BlobStorage.Accessor.Contracts.Utilities;
using BlobStorage.Accessor.Service.Host.Configurations;
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

    public async Task<Response> Upload(UploadContentCommand request, CancellationToken cancellationToken)
    {
        if (!request.Stream.IsBase64String())
        {
            request.Stream = request.Stream.EncodeToBase64();
        }

        BlobClient blobClient = _blobContainer.GetBlobClient($"{request.CustomPath}/{request.FileName}");

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

    public async Task<Response<string>> Download(DownloadContentQuery request, CancellationToken cancellationToken)
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
}