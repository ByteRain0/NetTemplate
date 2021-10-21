using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BlobStorage.Accessor.Contracts;
using BlobStorage.Accessor.Service.Infrastructure.Configurations;
using ExecutionPipeline.MediatRPipeline.ExceptionHandling;
using Microsoft.Extensions.Options;

namespace BlobStorage.Accessor.Service.Service
{
    public class AzureStorageAccessor : IStorageService
    {
        private readonly AzureStorageConfigs _options;

        public AzureStorageAccessor(IOptions<AzureStorageConfigs> options)
        {
            _options = options.Value;
        }

        public Task<Response> Upload()
        {
            throw new NotImplementedException();
        }

        public Task<Response<byte[]>> Download()
        {
            throw new NotImplementedException();
        }

        public Task<Response<List<string>>> ListContents()
        {
            throw new NotImplementedException();
        }
    }
}