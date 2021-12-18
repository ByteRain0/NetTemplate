using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BlobStorage.Accessor.Contracts;
using BlobStorage.Accessor.Contracts.Commands;
using BlobStorage.Accessor.Contracts.Commands.DeleteItem;
using BlobStorage.Accessor.Contracts.Commands.UploadContent;
using BlobStorage.Accessor.Contracts.Queries;
using BlobStorage.Accessor.Contracts.Queries.DownloadContent;
using BlobStorage.Accessor.Contracts.Queries.ListItems;
using ExecutionPipeline.MediatRPipeline.ExceptionHandling;
using FluentValidation;

namespace BlobStorage.Accessor.Service.Service;

public class AzureStorageAccessorValidator : IStorageAccessor
{
    private readonly IStorageAccessor _instance;

    public AzureStorageAccessorValidator(IStorageAccessor instance)
    {
        _instance = instance;
    }

    public async Task<Response> Upload(UploadItemCommand request, CancellationToken cancellationToken)
    {
        var validator = new UploadItemCommandValidator();
        await validator.ValidateAndThrowAsync(instance:request,cancellationToken:cancellationToken);
        return await _instance.Upload(request:request,cancellationToken:cancellationToken);
    }

    public async Task<Response<string>> Download(DownloadItemQuery request, CancellationToken cancellationToken)
    {
        var validator = new DownloadItemQueryValidator();
        await validator.ValidateAndThrowAsync(instance:request,cancellationToken:cancellationToken);
        return await _instance.Download(request:request,cancellationToken:cancellationToken);
    }

    public async Task<Response> DeleteFile(DeleteItemCommand request, CancellationToken cancellationToken)
    {
        var validator = new DeleteItemCommandCommandValidator();
        await validator.ValidateAndThrowAsync(instance:request,cancellationToken:cancellationToken);
        return await _instance.DeleteFile(request:request,cancellationToken:cancellationToken);
    }

    public async Task<Response<List<string>>> ListItems(ListFilesQuery request, CancellationToken cancellationToken)
    {
        var validator = new ListFilesOnPathQueryValidator();
        await validator.ValidateAndThrowAsync(instance:request,cancellationToken:cancellationToken);
        return await _instance.ListItems(request:request,cancellationToken:cancellationToken);
    }
}