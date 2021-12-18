using AzureKeyVault.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace AzureKeyVault.Host.Bootstrapper;

public static class KeyVaultBootstrapper
{
    public static IHostBuilder ConfigureHostForKeyVault(this IHostBuilder host)
    {
        host.ConfigureAppConfiguration(builder =>
        {
            var root = builder.Build();
            var vaultName = root["KeyVault:Vault"];
            var clientId = root["KeyVault:ClientId"];
            var clientSecret = root["KeyVault:ClientSecret"];

            builder.AddAzureKeyVault(vault: $"https://{vaultName}.vault.azure.net/",
                clientId: clientId,
                clientSecret: clientSecret,
                new PrefixKeyVaultSecretManager(root["KeyVault:AppPrefix"]));
        });

        return host;
    }
}