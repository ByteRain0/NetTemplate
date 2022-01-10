using Microsoft.Extensions.DependencyInjection;

namespace LoggerInfrastructure.Bootstrapper;

public static class ApplicationInsightsInfrastructure
{
    public static IServiceCollection AddAILogging(this IServiceCollection services, string instrumentationKey)
    {
        services.AddApplicationInsightsTelemetry(options =>
        {
            options.InstrumentationKey = instrumentationKey;
        });

        return services;
    }
}