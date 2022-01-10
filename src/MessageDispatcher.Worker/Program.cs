using ExecutionPipeline.Bootstrapper;
using Hangfire;
using Hangfire.PostgreSql;
using History.Accessor.Host.Bootstrappers;
using Manager.Host.Bootstrappers;
using Manager.Service.Bootstrappers;
using MessageDispatcher.Contracts;
using MessageDispatcher.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Session.Accessor.Service.Host.Bootstrappers;

namespace MessageDispatcher.Worker;

/// <summary>
/// Worker that can be used to Enqueue jobs starting with the Manager Layer downwards.
/// </summary>
public static class Program
{
    static void Main(string[] args)
    {
        CreateHostBuilder(args)
            .Build()
            .Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration(app => { app.AddJsonFile("appsettings.json"); })
            .ConfigureServices(services =>
            {
                var serviceConfiguration = services.BuildServiceProvider().GetService<IConfiguration>();
                    
                services.AddManagerServices();
                services.AddExecutionPipeline();
                services.AddEventHistory(serviceConfiguration);
                services.AddSessionAccessors();
                services.AddTransient<IMessageDispatcher, HangFireDispatcher>();
                    
                services.AddHangfire(configuration =>
                {
                    configuration.UseSqlServerStorage(serviceConfiguration.GetConnectionString("DefaultConnection"));
                    configuration.UseMediatR();
                });
                services.AddHangfireServer();
            });
}