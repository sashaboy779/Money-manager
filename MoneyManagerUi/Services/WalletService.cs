using MoneyManagerUi.Data.Wallet;
using MoneyManagerUi.Infrastructure;
using MoneyManagerUi.Infrastructure.Constants;
using MoneyManagerUi.Infrastructure.Extensions;
using MoneyManagerUi.Services.Interfaces;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace MoneyManagerUi.Services
{
    public class WalletService : Service, IWalletService
    {
        public WalletService(IStorageService storageService, IExpanseManagerClient apiClient)
            : base(storageService, apiClient)
        {
        }

        public async Task CreateWalletAsync(Wallet wallet)
        {
            ApiClient
                .CreateRequestMessage(HttpMethod.Post, ApiRoutes.Wallets)
                .AddAuthorization(await GetTokenAsync())
                .AddJsonContent(wallet);

            var response = await ApiClient.SendRequestAsync();
            await response.TryThrowModelErrorAsync();
        }

        public async Task DeleteWalletAsync(int id)
        {
            ApiClient
                .CreateRequestMessage(HttpMethod.Delete, string.Format(ApiRoutes.WalletsParameter, id))
                .AddAuthorization(await GetTokenAsync());

            await ApiClient.SendRequestAsync();
        }

        public async Task<Wallet> GetWalletAsync(int id)
        {
            ApiClient
               .CreateRequestMessage(HttpMethod.Get, string.Format(ApiRoutes.WalletsParameter, id))
               .AddAuthorization(await GetTokenAsync());

            var response = await ApiClient.SendRequestAsync();

            return await response.TryFetchContentAsync<Wallet>();
        }

        public async Task<IEnumerable<Wallet>> GetWalletsAsync()
        {
            ApiClient
                .CreateRequestMessage(HttpMethod.Get, ApiRoutes.Wallets)
                .AddAuthorization(await GetTokenAsync());

            var response = await ApiClient.SendRequestAsync();

            return (response.StatusCode == HttpStatusCode.NotFound) 
                ? new List<Wallet>() 
                : await response.TryFetchContentAsync<List<Wallet>>();
        }

        public async Task UpdateWalletAsync(Wallet wallet)
        {
            ApiClient
                .CreateRequestMessage(HttpMethod.Put, string.Format(ApiRoutes.WalletsParameter, wallet.WalletId))
                .AddAuthorization(await GetTokenAsync())
                .AddJsonContent(wallet);

            var response = await ApiClient.SendRequestAsync();
            await response.TryThrowModelErrorAsync();
        }
    }
}
