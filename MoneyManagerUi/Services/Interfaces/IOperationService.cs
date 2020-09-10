using MoneyManagerUi.Data;
using MoneyManagerUi.Data.Opration;
using System.Threading.Tasks;

namespace MoneyManagerUi.Services.Interfaces
{
    public interface IOperationService
    {
        Task<PagedResponse<WalletOperation>> GetWalletOperationsAsync(int walletId,
            int pageNumber = 1, int pageSize = 5);
        Task CreateOperationAsync(CreateOperation operation);
        Task UpdateOperationAsync(int id, UpdateOperation operation);
        Task DeleteOperationAsync(int id);
    }
}
