using MoneyManagerUi.Infrastructure;
using MoneyManagerUi.Infrastructure.Constants;
using MoneyManagerUi.Infrastructure.Extensions;
using MoneyManagerUi.Services.Interfaces;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace MoneyManagerUi.Services
{
    public abstract class Service
    {
        protected readonly IStorageService StorageService;
        protected readonly IExpanseManagerClient ApiClient;

        public Service(IStorageService storageService, IExpanseManagerClient apiClient)
        {
            StorageService = storageService;
            ApiClient = apiClient;
        }

        protected async Task<string> GetTokenAsync()
        {
            return await StorageService.GetItemAsync<string>(Configuration.TokenKey);
        }

        protected async Task<TResponseContent> GetRequestAsync<TResponseContent>(string uri)
            where TResponseContent : new()
        {
            ApiClient
                .CreateRequestMessage(HttpMethod.Get, uri)
                .AddAuthorization(await GetTokenAsync());

            var response = await ApiClient.SendRequestAsync();

            return (response.StatusCode == HttpStatusCode.NotFound)
                ? new TResponseContent()
                : await response.TryFetchContentAsync<TResponseContent>();
        }

        protected async Task PostRequestAsync<TContent>(string uri, TContent content)
        {
            ApiClient
               .CreateRequestMessage(HttpMethod.Post, uri)
               .AddAuthorization(await GetTokenAsync())
               .AddJsonContent(content);

            var response = await ApiClient.SendRequestAsync();
            await response.TryThrowModelErrorAsync();
        }

        protected async Task PutRequestAsync<TContent>(string uri, TContent content)
        {
            ApiClient
                .CreateRequestMessage(HttpMethod.Put, uri)
                .AddAuthorization(await GetTokenAsync())
                .AddJsonContent(content);

            var response = await ApiClient.SendRequestAsync();
            await response.TryThrowModelErrorAsync();
        }

        protected async Task DeleteRequestAsync(string uri)
        {
            ApiClient
               .CreateRequestMessage(HttpMethod.Delete, uri)
               .AddAuthorization(await GetTokenAsync());

            await ApiClient.SendRequestAsync();
        }
    }
}
