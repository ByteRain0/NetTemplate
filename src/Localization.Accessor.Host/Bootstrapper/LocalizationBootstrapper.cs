using ExecutionPipeline.Bootstrappers;
using Localization.Accessor.Contracts.Contracts;
using Localization.Accessor.Service.Accessors.CacheAccessor.Host;
using Localization.Accessor.Service.Accessors.FileAccessor.Host;
using Localization.Accessor.Service.Service;
using Localization.Accessor.Service.Service.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;

namespace Localization.Accessor.Infrastructure.Bootstrapper;

public class LocalizationBootstrapper : IBootstrapper
{
    public void BootstrapServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddDistributedLocalizationResourceAccessor(configuration);
        services.AddFileLocalization(configuration);

        services.AddTransient<IStringLocalizer, LocalizationEngine>();
        services.AddTransient<ILocalizationConfigurationsAccessor, LocalizationEngine>();
            
        services.Configure<LocalizationStoreInformation>(configuration.GetSection("LocalizationConfig"));
    }
}

public static class LocalizationMiddleware
{   
    public static void UseLocalization(this IApplicationBuilder app)
    {
        app.UseSession();
    }
}