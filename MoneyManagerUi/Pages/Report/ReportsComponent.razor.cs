using MoneyManagerUi.Data.Report;
using MoneyManagerUi.Services.Interfaces;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalletModel = MoneyManagerUi.Data.Wallet.Wallet;
using ReportModel = MoneyManagerUi.Data.Report.Report;
using Microsoft.AspNetCore.Authorization;
using MoneyManagerUi.Resources;

namespace MoneyManagerUi.Pages.Report
{
    [Authorize]
    public class ReportsComponent : ComponentBase
    {
        [Inject] public IReportService ReportService { get; set; }
        [Inject] public IStorageService StorageService { get; set; }
        [Inject] public IWalletService WalletService { get; set; }
        [Inject] public NavigationManager NavigationManager { get; set; }

        protected CustomReportRequest reportRequest;
        protected ReportModel reportResult;
        protected IEnumerable<WalletModel> userWallets;
        protected string validationError = string.Empty;
        protected bool isPageLoaded;
        protected bool isReportLoading;

        protected override async Task OnInitializedAsync()
        {
            reportRequest = new CustomReportRequest
            {
                StartingDate = DateTime.Today,
                EndingDate = DateTime.Today,
                DateInRange = DateTime.Today
            };

            userWallets = await WalletService.GetWalletsAsync();
            userWallets = userWallets.OrderBy(x => x.Name);
            if (userWallets.Any())
            {
                reportRequest.WalletId = userWallets.FirstOrDefault().WalletId;
            }

            await base.OnInitializedAsync();
            isPageLoaded = true;
        }

        protected void ClearError()
        {
            validationError = string.Empty;
        }

        protected async Task LoadReport()
        {
            isReportLoading = true;
            if (reportRequest.ReportType == ReportType.Custom)
            {
                if (DateTime.Compare(reportRequest.StartingDate, reportRequest.EndingDate) > -1)
                {
                    validationError = Resource.DateError;
                }
                else
                {
                    reportResult = await ReportService.GetCustomReportAsync(reportRequest);
                }
            }
            else
            {
                reportResult = await ReportService.GetReportAsync(reportRequest);
            }

            isReportLoading = false;
            StateHasChanged();
        }

        protected async Task LoadReportByLink(string link)
        {
            isReportLoading = true;
            reportResult = await ReportService.GetReportByLinkAsync(link);

            isReportLoading = false;
            StateHasChanged();
        }
    }
}
