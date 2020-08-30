using Microsoft.Extensions.Configuration;
using System.IO;

namespace DataAccessLayer.Migrations
{
    internal static class MigrationHelper
    {
        private const string DirectoryUpLevel = "..";
        private const string StartupProjectName = "MoneyManagerApi";
        private const string AppSettings = "appsettings.json";
        private const string AppSettingsDevelopment= "appsettings.Development.json";
        public static string ConnectionStringName => "WebApiDb";

        public static string GetConnectionString() 
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), 
                    DirectoryUpLevel, 
                    StartupProjectName))
                .AddJsonFile(AppSettings, optional: false, reloadOnChange: true)
                .AddJsonFile(AppSettingsDevelopment, optional: true)
                .AddEnvironmentVariables()
                .Build();

            return configuration.GetConnectionString(ConnectionStringName);
        }
    }
}
