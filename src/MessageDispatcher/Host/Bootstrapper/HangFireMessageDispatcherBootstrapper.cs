using Hangfire;
using Hangfire.PostgreSql;
using MessageDispatcher.Contracts;
using MessageDispatcher.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MessageDispatcher.Host.Bootstrapper;

public static class HangFireMessageDispatcherBootstrapper
{
    public static IServiceCollection AddMessageDispatcher(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddTransient<IMessageDispatcher, HangFireDispatcher>();
        services.AddHangfire(hangFireConfiguration =>
        {
            hangFireConfiguration.UseSqlServerStorage(configuration.GetConnectionString("DefaultConnection"));
            hangFireConfiguration.UseMediatR();
        });
        return services;
    }

    public static IApplicationBuilder UseMessageDispatcher(this IApplicationBuilder app)
    {
        MediatrQueueExtension.Configure(app.ApplicationServices.GetService<IMessageDispatcher>()); 
            
        app.UseHangfireDashboard();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapHangfireDashboard();
        });
            
        return app;
    }
}