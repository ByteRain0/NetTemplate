using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Logger.Bootstrapper;

public static class LoggerBootstrapper
{
    public static IHostBuilder AddApplicationInsightsLogging(this IHostBuilder host)
    {
        host.UseSerilog((context, services, loggerConfiguration) =>
            loggerConfiguration
                .WriteTo
                .ApplicationInsights(services.GetRequiredService<TelemetryConfiguration>(), TelemetryConverter.Traces));
        
        return host;
    }
    
    public static IServiceCollection AddAILogging(this IServiceCollection services, string instrumentationKey)
    {
        services.AddApplicationInsightsTelemetry(options =>
        {
            options.InstrumentationKey = instrumentationKey;
        });

        return services;
    }
}