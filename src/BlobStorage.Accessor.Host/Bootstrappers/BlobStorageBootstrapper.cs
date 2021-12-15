using BlobStorage.Accessor.Contracts;
using BlobStorage.Accessor.Service.Host.Configurations;
using BlobStorage.Accessor.Service.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BlobStorage.Accessor.Host.Bootstrappers;

public static class BlobStorageBootstrapper
{
    public static void AddBlobStorage(this IServiceCollection services, IConfiguration config)
    {
        services.AddTransient<IStorageAccessor, AzureStorageAccessor>();
        //services.Decorate<IStorageAccessor, AzureStorageAccessorExceptionHandler>();
        //services.Decorate<IStorageAccessor, AzureStorageAccessorValidator>();
        services.Configure<AzureStorageConfigs>(config.GetSection("AzureStorage"));
    }
}