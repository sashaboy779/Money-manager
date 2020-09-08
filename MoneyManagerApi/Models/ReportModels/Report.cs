using MoneyManagerApi.Models.WalletModels;

namespace MoneyManagerApi.Models.ReportModels
{
    public class Report
    {
        public string Name { get; set; }
        public Currency Currency { get; set; }
        public decimal? Income { get; set; }
        public decimal? Expense { get; set; }
        public decimal? OpeningBalance { get; set; }
        public decimal? EndingBalance { get; set; }
    }
}
