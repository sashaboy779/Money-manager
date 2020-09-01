using BusinessLogicLayer.Dto.ReportDtos;

namespace BusinessLogicLayer.Services.Interfaces
{
    public interface IReportService
    {
        ReportDto CreateReport(int userId, ReportRequestDto request);
    }
}
