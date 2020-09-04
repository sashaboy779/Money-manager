namespace MoneyManagerApi.Infrastructure.Constants
{
    public static class AppConfiguration
    {
        public static string LogFilePath => "{0}\\Logs\\Log.txt";
        public static string ConnectionStringName => "WebApiDb";
        public static string BusinessLogicProject => "BusinessLogicLayer";
        public static string WebApiProject => "MoneyManagerApi";
        public static string KeyVaultNameKey => "KeyVault:Vault";
        public static string KeyVaultClientIdKey => "KeyVault:ClientId";
        public static string KeyVaultThumbprintKey => "KeyVault:Thumbprint";
        public static string KeyVaultAddress => "https://{0}.vault.azure.net/";
    }
}
