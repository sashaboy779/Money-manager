namespace MoneyManagerApi.Models.ReportModels
{
    public class ReportWithLinks : Report
    {
        public string NextReport { get; set; }
        public string PreviousReport { get; set; }
    }
}
