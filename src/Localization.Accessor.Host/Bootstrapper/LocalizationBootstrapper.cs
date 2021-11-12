using Localization.Accessor.Contracts.Contracts;
using Localization.Accessor.Service.Accessors.CacheAccessor.Host;
using Localization.Accessor.Service.Accessors.FileAccessor.Host;
using Localization.Accessor.Service.Service;
using Localization.Accessor.Service.Service.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;

namespace Localization.Accessor.Infrastructure.Bootstrapper
{
    public static class LocalizationBootstrapper
    {
        public static void AddCustomLocalization(this IServiceCollection services, IConfiguration config)
        {
            services.AddDistributedLocalizationResourceAccessor(config);
            services.AddFileLocalization(config);

            services.AddTransient<IStringLocalizer, LocalizationEngine>();
            services.AddTransient<ILocalizationConfigurationsAccessor, LocalizationEngine>();
            
            services.Configure<LocalizationStoreInformation>(config.GetSection("LocalizationConfig"));

        }

        public static void UseLocalization(this IApplicationBuilder app)
        {
            app.UseSession();
        }
    }
}