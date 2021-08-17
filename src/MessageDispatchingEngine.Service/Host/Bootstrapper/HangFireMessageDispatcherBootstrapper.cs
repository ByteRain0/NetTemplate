using Hangfire;
using Hangfire.PostgreSql;
using MessageDispatchingEngine.Contracts;
using MessageDispatchingEngine.Service.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MessageDispatchingEngine.Service.Host.Bootstrapper
{
    public static class HangFireMessageDispatcherBootstrapper
    {
        public static IServiceCollection ConfigureMessageDispatcher(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddTransient<IMessageDispatcher, HangFireDispatcher>();
            services.AddHangfire(hangFireConfiguration =>
            {
                hangFireConfiguration.UsePostgreSqlStorage(configuration["DatabaseConnectionString"]);
                hangFireConfiguration.UseMediatR();
            });
            return services;
        }

        public static IApplicationBuilder ConfigureMessageDispatcher(this IApplicationBuilder app)
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
}