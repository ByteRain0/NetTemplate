using AutoMapper;
using BlobStorage.Accessor.Contracts.Commands;

namespace Manager.Service.Services.Blobs.Commands.UploadBlob
{
    public class UploadBlobProfile : Profile
    {
        public UploadBlobProfile()
        {
            CreateMap<UploadBlob, UploadContentCommand>()
                .ReverseMap();
        }
    }
}