using BlobStorage.Accessor.Contracts;
using BlobStorage.Accessor.Service;
using BlobStorage.Accessor.Service.Infrastructure.Configurations;
using BlobStorage.Accessor.Service.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BlobStorage.Accessor.Host.Bootstrappers
{
    public static class BlobStorageBootstrapper
    {
        public static void AddBlobStorage(this IServiceCollection services, IConfiguration config)
        {
            services.AddTransient<IStorageService, AzureStorageAccessor>();
            services.Configure<AzureStorageConfigs>(config.GetSection("AzureStorage"));
        }
    }
}