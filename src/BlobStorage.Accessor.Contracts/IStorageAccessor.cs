using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BlobStorage.Accessor.Contracts.Commands;
using BlobStorage.Accessor.Contracts.Commands.DeleteItem;
using BlobStorage.Accessor.Contracts.Commands.UploadContent;
using BlobStorage.Accessor.Contracts.Queries;
using BlobStorage.Accessor.Contracts.Queries.DownloadContent;
using BlobStorage.Accessor.Contracts.Queries.ListItems;
using ExecutionPipeline.MediatRPipeline.ExceptionHandling;

namespace BlobStorage.Accessor.Contracts;

public interface IStorageAccessor
{
    Task<Response> Upload(UploadItemCommand request, CancellationToken cancellationToken);

    Task<Response<string>> Download(DownloadItemQuery request, CancellationToken cancellationToken);
    
    Task<Response> DeleteFile(DeleteItemCommand request, CancellationToken cancellationToken);
    
    Task<Response<List<string>>> ListItems(ListFilesQuery request, CancellationToken cancellationToken);
}

public interface IStorageValidator
{
    
}