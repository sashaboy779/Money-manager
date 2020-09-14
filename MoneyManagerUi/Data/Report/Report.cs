using MoneyManagerUi.Data.Wallet;

namespace MoneyManagerUi.Data.Report
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
