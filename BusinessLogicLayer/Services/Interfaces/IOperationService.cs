using BusinessLogicLayer.Dto.OperationDtos;
using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLogicLayer.Dto;

namespace BusinessLogicLayer.Services.Interfaces
{
    public interface IOperationService
    {
        Task<OperationDto> GetOperationAsync(int userId, int operationId);
        Task<IEnumerable<OperationDto>> GetWalletOperationsAsync(int userId, int walletId, 
            PaginationFilter filter = null);
        Task<OperationDto> CreateOperationAsync(int userId, OperationDto operationDto);
        Task UpdateOperationAsync(int userId, UpdateOperationDto operationDto);
        Task DeleteOperationAsync(int userId, int operationId);
    }
}
