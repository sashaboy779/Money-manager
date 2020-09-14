using System;

namespace MoneyManagerUi.Data.Report
{
    public class ReportRequest
    {
        public int WalletId { get; set; }
        public ReportType ReportType { get; set; }
        public DateTime DateInRange { get; set; }
    }
}
