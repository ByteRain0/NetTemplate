using Localization.Accessor.Service.Accessors.FileAccessor.Contracts;
using Localization.Accessor.Service.Accessors.FileAccessor.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Localization.Accessor.Service.Accessors.FileAccessor.Host
{
    public static class FileLocalizationBootstrapper
    {
        public static void AddFileLocalization(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<FileLocalizationConfig>(config.GetSection("LocalizationConfig"));
            services.AddTransient<IFileAccessor, Service.FileAccessor>();
            services.Decorate<IFileAccessor, FileAccessorExceptionsHandler>();
        }
    }
}