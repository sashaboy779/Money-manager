using System.Threading.Tasks;
using MoneyManagerUi.Data.Report;

namespace MoneyManagerUi.Services.Interfaces
{
    public interface IReportService
    {
        Task<ReportWithLinks> GetReportAsync(ReportRequest request);
        Task<Report> GetCustomReportAsync(CustomReportRequest request);
        Task<ReportWithLinks> GetReportByLinkAsync(string link);
    }
}
