using System.Collections.Generic;
using System.Threading.Tasks;
using ExecutionPipeline.MediatRPipeline.ExceptionHandling;

namespace BlobStorage.Accessor.Contracts
{
    public interface IStorageService
    {
        Task<Response> Upload();

        Task<Response<byte[]>> Download();

        Task<Response<List<string>>> ListContents();
    }
}