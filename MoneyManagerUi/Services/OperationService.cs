using MoneyManagerUi.Data.Opration;
using MoneyManagerUi.Services.Interfaces;
using MoneyManagerUi.Infrastructure;
using System.Threading.Tasks;
using MoneyManagerUi.Infrastructure.Constants;
using MoneyManagerUi.Data;

namespace MoneyManagerUi.Services
{
    public class OperationService : Service, IOperationService
    {
        public OperationService(IStorageService storageService, IExpanseManagerClient apiClient)
            : base(storageService, apiClient)
        {
        }

        public async Task CreateOperationAsync(CreateOperation operation)
        {
            await PostRequestAsync(ApiRoutes.Operations, operation);
        }

        public async Task DeleteOperationAsync(int operationId)
        {
            var uri = string.Format(ApiRoutes.OperationsParameter, operationId);
            await DeleteRequestAsync(uri);
        }

        public async Task<PagedResponse<WalletOperation>> GetWalletOperationsAsync(int walletId, 
            int pageNumber = 1, int pageSize = 5)
        {
            var uri = string.Format(ApiRoutes.WalletOperations, walletId, pageNumber, pageSize);
            return await GetRequestAsync<PagedResponse<WalletOperation>>(uri);
        }

        public async Task UpdateOperationAsync(int operationId, UpdateOperation operation)
        {
            var uri = string.Format(ApiRoutes.OperationsParameter, operationId);
            await PutRequestAsync(uri, operation);
        }
    }
}
