namespace MoneyManagerApi.Infrastructure.Constants
{
    public static class Routes
    {
        public const string Controller = "[controller]";
        public const string Id = "{id}";
        public const string ShowCategory = "ShowCategory";
        public const string SignIn = "signin";
        public const string Register = "register";
        public const string Daily = "daily";
        public const string QueryDaily = "queryDaily";
        public const string Monthly = "monthly";
        public const string QueryMonthly = "queryMonthly";
        public const string Yearly = "yearly";
        public const string QueryYearly = "queryYearly";
        public const string WalletOperations = "wallets/{walletId}";

        public const string ShowWallet = "ShowWallet";
        public const string ShowDailyReport = "ShowDailyReport";
        public const string ShowMonthlyReport = "ShowMonthlyReport";
        public const string ShowYearlyReport = "ShowYearlyReport";
        public const string ShowOperation = "ShowOperation";
    }
}
