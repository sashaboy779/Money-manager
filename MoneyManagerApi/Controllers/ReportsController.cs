using AutoMapper;
using BusinessLogicLayer.Dto.ReportDtos;
using BusinessLogicLayer.Services.Interfaces;
using MoneyManagerApi.Infrastructure.Constants;
using MoneyManagerApi.Models.ReportModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace MoneyManagerApi.Controllers
{
    [Route(Routes.Controller)]
    [ApiController]
    [Authorize]
    public class ReportsController : BaseController
    {
        private readonly IReportService reportService;

        public ReportsController(IReportService reportService, IMapper mapper, IUriService uriService)
            : base(mapper, uriService)
        {
            this.reportService = reportService;
        }

        [HttpGet(Routes.Daily)]
        public IActionResult ShowDailyReport(ReportRequest request)
        {
            var report = GetReport<ReportWithLinks>(TimeRange.Day, request);
            AddReportLinks(request, report, Routes.ShowDailyReport, (date, days) => date.AddDays(days));

            return Ok(report);
        }

        [HttpGet]
        [Route(Routes.QueryDaily, Name = Routes.ShowDailyReport)]
        public IActionResult ShowDailyReportWrapper([FromQuery] ReportRequest request)
        {
            return ShowDailyReport(request);
        }

        [HttpGet(Routes.Monthly)]
        public IActionResult ShowMonthlyReport(ReportRequest request)
        {
            var report = GetReport<ReportWithLinks>(TimeRange.Month, request);
            AddReportLinks(request, report, Routes.ShowMonthlyReport,
                          (date, months) => date.AddMonths(months));

            return Ok(report);
        }

        [HttpGet]
        [Route(Routes.QueryMonthly, Name = Routes.ShowMonthlyReport)]
        public IActionResult ShowMonthlyReportWrapper([FromQuery] ReportRequest request)
        {
            return ShowMonthlyReport(request);
        }

        [HttpGet(Routes.Yearly)]
        public IActionResult ShowYearlyReport(ReportRequest request)
        {
            var report = GetReport<ReportWithLinks>(TimeRange.Year, request);
            AddReportLinks(request, report, Routes.ShowYearlyReport,
                          (date, years) => date.AddYears(years));

            return Ok(report);
        }

        [HttpGet]
        [Route(Routes.QueryYearly, Name = Routes.ShowYearlyReport)]
        public IActionResult ShowYearlyReportWrapper([FromQuery] ReportRequest request)
        {
            return ShowYearlyReport(request);
        }

        [HttpGet()]
        public IActionResult ShowCustomReport(CustomReportRequest request)
        {
            var report = GetReport<Report>(TimeRange.Custom, request);

            return Ok(report);
        }

        private TReport GetReport<TReport>(TimeRange timeRange, BaseReportRequest request)
        {
            request.TimeRange = timeRange;
            var test = new ReportRequestDto();
            Mapper.Map(request, test);
            var requestDto = Mapper.Map<ReportRequestDto>(request);

            var reportDto = reportService.CreateReport(GetUserId(), requestDto);
            return Mapper.Map<TReport>(reportDto);
        }

        private void AddReportLinks(ReportRequest request, ReportWithLinks report, string routeName,
                                    Func<DateTime, int, DateTime> addRemoveDateStep)
        {
            request.DateInRange = addRemoveDateStep(request.DateInRange.Value, 1);
            report.NextReport = Url.Link(routeName, request);

            request.DateInRange = addRemoveDateStep(request.DateInRange.Value, -2);
            report.PreviousReport = Url.Link(routeName, request);
        }
    }
}
