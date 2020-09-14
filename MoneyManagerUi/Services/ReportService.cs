using MoneyManagerUi.Data.Report;
using MoneyManagerUi.Infrastructure;
using MoneyManagerUi.Infrastructure.Constants;
using MoneyManagerUi.Infrastructure.Extensions;
using MoneyManagerUi.Services.Interfaces;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace MoneyManagerUi.Services
{
    public class ReportService : Service, IReportService
    {
        public ReportService(IStorageService storageService, IExpanseManagerClient apiClient) 
            : base(storageService, apiClient)
        {
        }

        public async Task<Report> GetCustomReportAsync(CustomReportRequest request)
        {
            return await PostRequestAsync<Report, CustomReportRequest>(ApiRoutes.Reports, request);
        }

        public async Task<ReportWithLinks> GetReportAsync(ReportRequest request)
        {
            var uri = string.Format(ApiRoutes.ReportsParameter, 
                Enum.GetName(typeof(ReportType), request.ReportType));

            return await PostRequestAsync<ReportWithLinks, ReportRequest>(uri, request);
        }

        public async Task<ReportWithLinks> GetReportByLinkAsync(string link)
        {
            return await GetRequestAsync<ReportWithLinks>(link);
        }

        private async Task<TResponse> PostRequestAsync<TResponse, TContent>(string uri, TContent content)
        {
            ApiClient
               .CreateRequestMessage(HttpMethod.Get, uri)
               .AddAuthorization(await GetTokenAsync())
               .AddJsonContent(content);

            var response = await ApiClient.SendRequestAsync();
            return await response.TryFetchContentAsync<TResponse>();
        }
    }
}