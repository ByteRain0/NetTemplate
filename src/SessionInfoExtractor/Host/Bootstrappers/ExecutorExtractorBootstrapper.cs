using Microsoft.Extensions.DependencyInjection;
using Session.Accessor.Service.Contracts;
using Session.Accessor.Service.Service.Infrastructure;

namespace Session.Accessor.Service.Host.Bootstrappers
{
    public static class ExecutorExtractorBootstrapper
    {
        public static void ConfigureSessionAccessors(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddTransient<ISessionInfoExtractor, Service.SessionInfoExtractor>();
            services.Decorate<ISessionInfoExtractor,SessionInfoExtractorLogger>();
        }
    }
}