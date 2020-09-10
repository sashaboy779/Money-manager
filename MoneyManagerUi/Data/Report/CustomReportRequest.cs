using System;
using System.ComponentModel.DataAnnotations;

namespace MoneyManagerUi.Data.Report
{
    public class CustomReportRequest : ReportRequest
    {
        public DateTime StartingDate { get; set; }

        public DateTime EndingDate { get; set; }
    }
}
