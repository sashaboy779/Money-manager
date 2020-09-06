using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExpenseManagerApi.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MoneyManagerApi.Infrastructure.Constants;
using MoneyManagerApi.Infrastructure.Helpers;

namespace MoneyManagerApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(builder =>
                {
                    var root = builder.Build();
                    var vaultName = root[AppConfiguration.KeyVaultNameKey];
                    if (!String.IsNullOrEmpty(vaultName))
                    {
                        // use Azure key vault
                        builder.AddAzureKeyVault(
                        String.Format(AppConfiguration.KeyVaultAddress, vaultName),
                        root[AppConfiguration.KeyVaultClientIdKey],
                        CertificateHelper.GetCertificate(root[AppConfiguration.KeyVaultThumbprintKey]),
                        new PrefixKeyVaultSecretManager(AppConfiguration.WebApiProject));
                    }
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
