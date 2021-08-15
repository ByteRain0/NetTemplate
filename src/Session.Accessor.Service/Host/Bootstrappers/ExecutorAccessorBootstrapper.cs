using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using SessionAccessor.Contracts;
using SessionAccessor.Host.HealthChecks;
using SessionAccessor.Service.Infrastructure;

namespace SessionAccessor.Host.Bootstrappers
{
    public static class ExecutorAccessorBootstrapper
    {
        public static void ConfigureSessionAccessors(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddTransient<IExecutorAccessor, Service.ExecutorAccessor>();
            services.Decorate<IExecutorAccessor,ExecutorAccessorLogger>();
            
            var hcBuilder = services.AddHealthChecks();
            
            hcBuilder.AddCheck<ExecutorAccessorHealthCheck>(
                "executor_service_health_check",
                failureStatus: HealthStatus.Degraded,
                tags: new[] { "ExecutorAccessor" });
        }
    }
}