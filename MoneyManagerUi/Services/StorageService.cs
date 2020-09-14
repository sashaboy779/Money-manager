using System.Threading.Tasks;
using Blazored.SessionStorage;
using MoneyManagerUi.Data.User;
using MoneyManagerUi.Infrastructure.Constants;
using MoneyManagerUi.Services.Interfaces;

namespace MoneyManagerUi.Services
{
    public class StorageService : IStorageService
    {
        private readonly ISessionStorageService storageService;

        public StorageService(ISessionStorageService storageService)
        {
            this.storageService = storageService;
        }

        public async Task StoreItemAsync<TItem>(string key, TItem item)
        {
            await storageService.SetItemAsync(key, item);
        }

        public async Task<TItem> GetItemAsync<TItem>(string key)
        {
            return await storageService.GetItemAsync<TItem>(key);
        }

        public async Task RemoveItemAsync(string key)
        {
            await storageService.RemoveItemAsync(key);
        }
    }
}
