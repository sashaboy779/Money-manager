namespace MoneyManagerApi.Infrastructure.Constants
{
    public static class AppConfiguration
    {
        public static string LogFilePath => "{0}\\Logs\\Log.txt";
        public static string ConnectionStringName => "WebApiDb";
        public static string BusinessLogicProject => "BusinessLogicLayer";
        public static string WebApiProject => "MoneyManagerApi";
    }
}
