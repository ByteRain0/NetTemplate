using System.Threading;
using System.Threading.Tasks;
using BlobStorage.Accessor.Contracts;
using BlobStorage.Accessor.Contracts.Commands;
using BlobStorage.Accessor.Contracts.Queries;
using BlobStorage.Accessor.Service.Service.Validators;
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

    public async Task<Response> Upload(UploadContentCommand request, CancellationToken cancellationToken)
    {
        var validator = new UploadContentCommandValidator();
        await validator.ValidateAndThrowAsync(instance:request,cancellationToken:cancellationToken);
        return await _instance.Upload(request:request,cancellationToken:cancellationToken);
    }

    public async Task<Response<string>> Download(DownloadContentQuery request, CancellationToken cancellationToken)
    {
        var validator = new DownloadContentQueryValidator();
        await validator.ValidateAndThrowAsync(instance:request,cancellationToken:cancellationToken);
        return await _instance.Download(request:request,cancellationToken:cancellationToken);
    }
}