using Localization.Accessor.Service.Accessors.CacheAccessor.Contracts;
using Localization.Accessor.Service.Accessors.CacheAccessor.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Localization.Accessor.Service.Accessors.CacheAccessor.Host;

public static class CacheLocalizationBootstrapper
{
    public static void AddDistributedLocalizationResourceAccessor(this IServiceCollection services, IConfiguration config)
    {
        services.Configure<CacheLocalizationConfig>(config.GetSection("LocalizationConfig"));
        services.AddTransient<ICacheLocalizationAccessor, Service.CacheAccessor>();
        services.Decorate<ICacheLocalizationAccessor, CacheAccessorExceptionsHandler>();
        services.Decorate<ICacheLocalizationAccessor, CacheLocalizationValidator>();
        services.AddHttpContextAccessor();
        services.AddDistributedMemoryCache().AddSession();
    }
}