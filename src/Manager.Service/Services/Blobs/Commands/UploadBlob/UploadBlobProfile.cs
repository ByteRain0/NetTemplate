using AutoMapper;
using BlobStorage.Accessor.Contracts.Commands;
using BlobStorage.Accessor.Contracts.Commands.UploadContent;

namespace Manager.Service.Services.Blobs.Commands.UploadBlob;

public class UploadBlobProfile : Profile
{
    public UploadBlobProfile()
    {
        CreateMap<UploadBlob, UploadItemCommand>()
            .ReverseMap();
    }
}