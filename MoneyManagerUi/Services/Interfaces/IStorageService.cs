using MoneyManagerUi.Data.User;
using System.Threading.Tasks;

namespace MoneyManagerUi.Services.Interfaces
{
    public interface IStorageService
    {
        Task StoreItemAsync<TItem>(string key, TItem item);
        Task<TItem> GetItemAsync<TItem>(string key);
        Task RemoveItemAsync(string key);
    }
}
