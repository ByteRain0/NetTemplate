using BlobStorage.Accessor.Contracts;
using BlobStorage.Accessor.Service.Infrastructure;
using BlobStorage.Accessor.Service.Service;
using ExecutionPipeline.Bootstrappers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BlobStorage.Accessor.Host.Bootstrappers;

public class BlobStorageBootstrapper : IBootstrapper
{
    public void BootstrapServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IStorageAccessor, AzureStorageAccessor>();
        services.Decorate<IStorageAccessor, AzureStorageAccessorExceptionHandler>();
        services.Decorate<IStorageAccessor, AzureStorageAccessorValidator>();
        services.Configure<AzureStorageConfigs>(configuration.GetSection("AzureStorage"));
    }
}