using AzureKeyVaultAccessorContracts.Host.Bootstrapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace CloudOrchestra
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args)
                .Build()
                .Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureHostForKeyVault()
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}