namespace InfrastructureLayer.Constants
{
    public static class ExceptionConfiguration
    {
        public static string ResponseContentType => "application/json";
        public static string LogMessage => "Something went wrong: {0}";
        public static string UserMessage => "Internal Server Error.";
    }
}
