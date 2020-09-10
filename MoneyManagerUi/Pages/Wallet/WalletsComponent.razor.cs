using AutoMapper;
using MoneyManagerUi.Data.Opration;
using MoneyManagerUi.Infrastructure.Constants;
using MoneyManagerUi.Services.Interfaces;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MoneyManagerUi.Pages.Operation;
using MoneyManagerUi.Shared.Classes;
using WalletModel = MoneyManagerUi.Data.Wallet.Wallet;
using OperationModel = MoneyManagerUi.Data.Opration.Operation;
using MoneyManagerUi.Resources;
using Microsoft.AspNetCore.Authorization;
using MoneyManagerUi.Data.Report;
using System;
using MoneyManagerUi.Data;

namespace MoneyManagerUi.Pages.Wallet
{
    [Authorize]
    public class WalletsComponent : ComponentWithModals
    {
        [Inject] public IMapper Mapper { get; set; }
        [Inject] public IWalletService WalletService { get; set; }
        [Inject] public IOperationService OperationService { get; set; }
        [Inject] public IReportService ReportService { get; set; }
        [Inject] public IStorageService StorageService { get; set; }

        protected WalletModel selectedWallet;
        protected IEnumerable<WalletModel> userWallets;
        protected PagedResponse<WalletOperation> walletOperations;
        protected ReportWithLinks monthlyReport;

        protected override async Task OnInitializedAsync()
        {
            userWallets = await WalletService.GetWalletsAsync();

            await SetInitialWalletAsync();
            await TryUpdateWalletInfoAsync();

            await base.OnInitializedAsync();
            IsPageLoaded = true;
        }

        protected async Task ChangeSelectedWallet(WalletModel walletToSelect)
        {
            IsPageLoaded = false;
            walletOperations = null;
            await SetSelectedWalletAsync(walletToSelect);
            await TryUpdateWalletInfoAsync();
            IsPageLoaded = true;
        }

        protected async Task ShowCreateModal()
        {
            SetModalParameters(new WalletModel { Currency = Data.Wallet.Currency.USD }, async (w) =>
            {
                await WalletService.CreateWalletAsync(w);
            });
            Parameters.Add(nameof(WalletForm.SubmitText), Resource.Create);

            var title = Resource.CreateWallet;
            await ShowModalWindowAsync<WalletForm, WalletModel>(title, async () =>
            {
                userWallets = await WalletService.GetWalletsAsync();
                await SetSelectedWalletAsync(userWallets.OrderBy(x => x.WalletId).Last());
                await TryUpdateWalletInfoAsync();

                IsPageLoaded = true;
                StateHasChanged();
            });
        }

        protected async Task ShowEditModal()
        {
            WalletModel copyWallet = new WalletModel();
            Mapper.Map(selectedWallet, copyWallet);

            SetModalParameters(copyWallet, async (w) =>
            {
                await WalletService.UpdateWalletAsync(w);
            });
            Parameters.Add(nameof(WalletForm.SubmitText), Resource.Save);

            var title = string.Format(Resource.EditTitle, selectedWallet.Name);
            await ShowModalWindowAsync<WalletForm, WalletModel>(title, async () =>
            {
                userWallets = await WalletService.GetWalletsAsync();
                await SetSelectedWalletAsync(userWallets.Single(x => x.WalletId == selectedWallet.WalletId));
                await TryUpdateWalletInfoAsync();
            });
        }

        protected async Task ShowDeleteModal()
        {
            SetModalParameters(selectedWallet, async (w) =>
            {
                await WalletService.DeleteWalletAsync(w.WalletId);
            });

            var title = string.Format(Resource.DeleteTitle, selectedWallet.Name);
            await ShowModalWindowAsync<DeleteWallet, WalletModel>(title, async () =>
            {
                userWallets = await WalletService.GetWalletsAsync();
                await SetSelectedWalletAsync(userWallets.FirstOrDefault());
                await TryUpdateWalletInfoAsync();
            });
        }

        protected async Task ShowCreateOperationModal()
        {
            SetModalParameters(new CreateOperation(), async (o) =>
            {
                await OperationService.CreateOperationAsync(o);
            });
            Parameters.Add(nameof(CreateOperationModal.WalletId), selectedWallet.WalletId);

            var title = Resource.CreateOperatioinTitle;
            await ShowModalWindowAsync<CreateOperationModal, CreateOperation>(title, async () =>
            {
                await TryUpdateWalletInfoAsync();
                await SetSelectedWalletAsync(await WalletService.GetWalletAsync(selectedWallet.WalletId));
                StateHasChanged();
            });
        }

        protected async Task ShowEditOperationModal(WalletOperation operation)
        {
            var operationToEdit = Mapper.Map<UpdateOperation>(operation);
            SetModalParameters(operationToEdit, async (o) =>
            {
                await OperationService.UpdateOperationAsync(o.OperationId, o);
            });

            var title = Resource.EditOperationTitle;
            await ShowModalWindowAsync<EditOperationModal, UpdateOperation>(title, async () =>
            {
                await TryUpdateWalletInfoAsync();
                StateHasChanged();
            });
        }

        protected async Task ShowDeleteOperationModal(WalletOperation operation)
        {
            var operationToDelete = Mapper.Map<OperationModel>(operation);
            SetModalParameters(operationToDelete, async (o) =>
            {
                await OperationService.DeleteOperationAsync(o.OperationId);
            });

            var title = Resource.DeleteOperationTitle;
            await ShowModalWindowAsync<DeleteOperationModal, OperationModel>(title, async () =>
            {
                await SetSelectedWalletAsync(await WalletService.GetWalletAsync(selectedWallet.WalletId));
                await TryUpdateWalletInfoAsync();
                StateHasChanged();
            });
        }

        protected async Task SetWalletOperations(int pageNumber)
        {
            walletOperations = await OperationService.GetWalletOperationsAsync(selectedWallet.WalletId, pageNumber);
            
            walletOperations.Data = walletOperations.Data == null
                ? new List<WalletOperation>()
                :walletOperations.Data.OrderBy(x => x.OperationDate).ToList();

            if (pageNumber != 1 && (walletOperations.Data == null || !walletOperations.Data.Any()))
            {
                await SetWalletOperations(pageNumber - 1);
            }
        }

        private async Task SetInitialWalletAsync()
        {
            var walletId = await StorageService.GetItemAsync<int>(Configuration.WalletIdKey);
            if (walletId != 0)
            {
                var wallet = userWallets.Single(x => x.WalletId == walletId);
                await SetSelectedWalletAsync(wallet);
            }
            else
            {
                await SetSelectedWalletAsync(userWallets.FirstOrDefault());
            }
        }

        private async Task SetSelectedWalletAsync(WalletModel wallet)
        {
            selectedWallet = wallet;

            if (selectedWallet == null)
            {
                await StorageService.RemoveItemAsync(Configuration.WalletIdKey);
            }
            else
            {
                await StorageService.StoreItemAsync(Configuration.WalletIdKey, selectedWallet.WalletId);
            }
        }

        private async Task TryUpdateWalletInfoAsync()
        {
            if (selectedWallet != null)
            {
                var operationsPage = walletOperations == null || walletOperations.PageNumber == null 
                    ? 1 : walletOperations.PageNumber.Value;
                await SetWalletOperations(operationsPage);

                var reportRequest = new ReportRequest
                {
                    ReportType = ReportType.Monthly,
                    WalletId = selectedWallet.WalletId,
                    DateInRange = DateTime.Today
                };
                monthlyReport = await ReportService.GetReportAsync(reportRequest);
            }
        }
    }
}

