using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BlobStorage.Accessor.Contracts;
using BlobStorage.Accessor.Contracts.Commands;
using BlobStorage.Accessor.Contracts.Commands.UploadContent;
using ExecutionPipeline.MediatRPipeline.ExceptionHandling;
using MediatR;

namespace Manager.Service.Services.Blobs.Commands.UploadBlob;

public class UploadBlobHandler : IRequestHandler<UploadBlob, Response>
{
    // private readonly IStorageAccessor _storageAccessor;
    //
    // private readonly IMapper _mapper;
        
    // public UploadBlobHandler(IStorageAccessor storageAccessor, IMapper mapper)
    // {
    //     _storageAccessor = storageAccessor;
    //     _mapper = mapper;
    // }

    public async Task<Response> Handle(UploadBlob request, CancellationToken cancellationToken)
    {
        throw new InvalidOperationException("Expected exception");
        return Response.Fail("Test");
        //return await _storageAccessor.Upload(_mapper.Map<UploadItemCommand>(request), cancellationToken);
    }
}