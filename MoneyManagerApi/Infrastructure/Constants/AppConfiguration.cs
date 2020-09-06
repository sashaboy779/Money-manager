namespace MoneyManagerApi.Infrastructure.Constants
{
    public static class AppConfiguration
    {
        public static string ResourcesPath => "Resources";
        public static string AppSettingsSection => "AppSettings";
        public static string LogFilePath => "{0}\\Logs\\Log.txt";
        public static string[] SupportedCultures => new[] { "uk", "en-US" };
        public static string DefaultCulture => "en-US";
        public static string Unauthorized => "Unauthorized";
        public static string KeyVaultNameKey => "KeyVault:Vault";
        public static string KeyVaultClientIdKey => "KeyVault:ClientId";
        public static string KeyVaultThumbprintKey => "KeyVault:Thumbprint";
        public static string KeyVaultAddress => "https://{0}.vault.azure.net/";
        public static string ProjectName => "MoneyManagerApi";
        public static string ConnectionStringName => "WebApiDb";
        public static string BusinessLogicProject => "BusinessLogicLayer";
        public static string WebApiProject => "MoneyManagerApi";
    }
}
