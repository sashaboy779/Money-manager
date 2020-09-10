namespace MoneyManagerUi.Infrastructure.Constants
{
    public class ApiRoutes
    {
        public static string SignIn => "accounts/signin";
        public static string Register => "accounts/register";
        public static string UserAccount => "accounts";
        public static string Wallets => "wallets";
        public static string WalletsParameter => "wallets/{0}";
        public static string Operations => "operations";
        public static string OperationsParameter => "operations/{0}";
        public static string Categories => "categories";
        public const string CreateSubcategory = "categories/sub";
        public static string CategoriesParameter => "categories/{0}";
        public static string WalletOperations => "/operations/wallets/{0}/?pageNumber={1}&pageSize={2}";
        public static string ReportsParameter => "/reports/{0}";
        public static string Reports => "/reports";
    }
}
