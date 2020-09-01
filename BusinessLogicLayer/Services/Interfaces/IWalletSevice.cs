using BusinessLogicLayer.Dto.WalletDtos;
using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLogicLayer.Dto;

namespace BusinessLogicLayer.Services.Interfaces
{
    public interface IWalletSevice
    {
        Task<WalletDto> CreateWalletAsync(int userId, WalletDto walletDto);
        Task<WalletDto> GetWalletAsync(int userId, int walletId);
        Task<IEnumerable<WalletDto>> GetAllWalletsAsync(int userId, PaginationFilter filter = null);
        Task UpdateWalletAsync(int userId, int walletId, UpdateWalletDto walletDto);
        Task DeleteWalletAsync(int userId, int walletId);
    }
}
