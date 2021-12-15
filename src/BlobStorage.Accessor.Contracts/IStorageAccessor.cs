using System.Threading;
using System.Threading.Tasks;
using BlobStorage.Accessor.Contracts.Commands;
using BlobStorage.Accessor.Contracts.Queries;
using ExecutionPipeline.MediatRPipeline.ExceptionHandling;

namespace BlobStorage.Accessor.Contracts;

public interface IStorageAccessor
{
    Task<Response> Upload(UploadContentCommand request, CancellationToken cancellationToken);

    Task<Response<string>> Download(DownloadContentQuery request, CancellationToken cancellationToken);
}