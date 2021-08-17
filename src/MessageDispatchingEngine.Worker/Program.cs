using ExecutionPipeline.Bootstrapper;
using Hangfire;
using Hangfire.PostgreSql;
using History.Accessor.Host.Bootstrappers;
using MessageDispatchingEngine.Contracts;
using MessageDispatchingEngine.Service.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MessageDispatchingEngine.Worker
{
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
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(app =>
                {
                    app.AddJsonFile("appsettings.json");
                })
                .ConfigureServices(services =>
                {
                    var serviceConfiguration = services.BuildServiceProvider().GetService<IConfiguration>();
                    services.ConfigureEventHistory(serviceConfiguration);
                    services.ConfigureExecutionPipeline();
                    services.AddTransient<IMessageDispatcher, HangFireDispatcher>();
                    services.AddHangfire(configuration =>
                    {
                        configuration.UsePostgreSqlStorage(serviceConfiguration["DatabaseConnectionString"]);
                        configuration.UseMediatR();
                    });
                    services.AddHangfireServer();
                });
    }
}