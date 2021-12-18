using System;
using System.Dynamic;
using System.Globalization;
using AutoMapper;
using Azure.Storage.Blobs;
using BlobStorage.Accessor.Contracts;
using BlobStorage.Accessor.Host.Bootstrappers;
using BlobStorage.Accessor.Service.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BlobStorage.Accessor.Tests.Infrastructure;

public class TestBase
{
    public TestContext TestContext { get; set; }

    protected static IServiceProvider ServiceProvider { get; set; }

    protected static IMapper Mapper { get; set; }


    public static void InitialSetup(TestContext context)
    {
        var cultureInfo = new CultureInfo("en-US");
        CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
        CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
        ServiceProvider = ConfigureServices();
        Mapper = ServiceProvider.GetService<IMapper>();
    }

    public static IServiceProvider ConfigureServices()
    {
        var services = new ServiceCollection();
        var configuration = InitConfiguration();

        services.AddEntityFrameworkInMemoryDatabase()
            .BuildServiceProvider();

        services.Configure<BlobStorageTestConfigurations>(configuration.GetSection("BlobStorageTestConfigurations"));
        services.AddBlobStorage(configuration);
            
        return services
            .AddLogging()
            .BuildServiceProvider();
    }

    private static IConfiguration InitConfiguration()
    {
        var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.test.json")
            .Build();

        return config;
    }

    protected T GetService<T>()
    {
        return ServiceProvider.GetService<T>();
    }


    [TestCleanup]
    public void Cleanup()
    {
        IOptions<AzureStorageConfigs> options = GetService<IOptions<AzureStorageConfigs>>();
        var blobContainer = new BlobServiceClient(options.Value.ConnectionString)
            .GetBlobContainerClient(options.Value.ContainerName.ToLowerInvariant());

            
        var exists = blobContainer.Exists();
        if (exists.Value)
        {
            var blobList = blobContainer.GetBlobs();

            foreach (var blobItem in blobList)
            {
                blobContainer.DeleteBlob(blobItem.Name);
            }
                
            //Deleting a container takes more time than simply cleaning up all the blobs from the folders
            //And impacts tests realiability.
                
            //blobContainer.Delete();
        }
    }
}