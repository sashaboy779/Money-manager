using MoneyManagerUi.Data.Wallet;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoneyManagerUi.Services.Interfaces
{
    public interface IWalletService
    {
        Task<IEnumerable<Wallet>> GetWalletsAsync();
        Task<Wallet> GetWalletAsync(int id);
        Task UpdateWalletAsync(Wallet wallet);
        Task CreateWalletAsync(Wallet wallet);
        Task DeleteWalletAsync(int id); 
    }
}
