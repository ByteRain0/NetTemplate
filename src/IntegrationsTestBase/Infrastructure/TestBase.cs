using System;
using System.Globalization;
using AutoMapper;
using ExecutionPipeline.Bootstrapper;
using History.Accessor.Host.Bootstrappers;
using History.Accessor.Service.Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace History.Accessor.Tests.Infrastructure
{
    public class TestBase
    {
        public TestContext TestContext { get; set; }

        protected static HistoryContext DatabaseContext { get; set; }

        protected static IServiceProvider ServiceProvider { get; set; }

        protected static IMapper Mapper { get; set; }
        
        public static void InitialSetup(TestContext context)
        {
            var cultureInfo = new CultureInfo("en-US");
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
            ServiceProvider = ConfigureServices();
            Mapper = ServiceProvider.GetService<IMapper>();
            InitializeDatabase();
        }
        
        public static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();
            var configuration = InitConfiguration();

            services.ConfigureEventHistory(configuration);
            services.ConfigureExecutionPipeline();
            
            var serviceProvider = services
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            services.AddDbContext<HistoryContext>(options =>
            {
                options.UseInMemoryDatabase("InMemoryHistoryDb");
                options.UseInternalServiceProvider(serviceProvider);
            });
            
            return services
                .AddLogging()
                .BuildServiceProvider();
        }
        
        private static void InitializeDatabase()
        {
            DatabaseContext = ServiceProvider.GetRequiredService<HistoryContext>();
            DatabaseContext.Database.EnsureCreated();
        }
        
        private static IConfiguration InitConfiguration()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.test.json")
                .Build();

            return config;
        }
        
        protected T GetInstance<T>(string instanceKey = null)
        {
            if (string.IsNullOrWhiteSpace(instanceKey) == false)
            {
                return ServiceProvider.GetService<T>();
            }

            return ServiceProvider.GetService<T>();
        }
        
        [TestCleanup]
        public void Cleanup()
        {
            DatabaseContext.Database.EnsureDeleted();
        }
    }
}
