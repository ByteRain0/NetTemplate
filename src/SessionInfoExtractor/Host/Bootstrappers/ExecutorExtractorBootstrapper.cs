using ExecutionPipeline.Bootstrappers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Session.Accessor.Service.Contracts;
using Session.Accessor.Service.Service.Infrastructure;

namespace Session.Accessor.Service.Host.Bootstrappers;

public class ExecutorExtractorBootstrapper : IBootstrapper
{
    public void BootstrapServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpContextAccessor();
        services.AddTransient<ISessionInfoExtractor, Service.SessionInfoExtractor>();
        services.Decorate<ISessionInfoExtractor,SessionInfoExtractorLogger>();
    }
}