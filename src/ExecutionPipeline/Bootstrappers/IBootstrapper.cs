using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ExecutionPipeline.Bootstrappers;

public interface IBootstrapper
{
    void BootstrapServices(IServiceCollection services, IConfiguration configuration);
}