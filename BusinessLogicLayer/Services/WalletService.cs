using AutoMapper;
using BusinessLogicLayer.Dto.WalletDtos;
using BusinessLogicLayer.Exceptions;
using BusinessLogicLayer.Resources;
using BusinessLogicLayer.Services.Interfaces;
using DataAccessLayer.Entities;
using DataAccessLayer.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogicLayer.Dto;

namespace BusinessLogicLayer.Services
{
    public class WalletService : Service, IWalletSevice
    {
        public WalletService(IUnitOfWork unitOfWork, IMapper mapper)
            : base(unitOfWork, mapper)
        {
        }

        public async Task<WalletDto> CreateWalletAsync(int userId, WalletDto walletDto)
        {
            await ThrowIfNameExists(userId, walletDto.Name);

            walletDto.UserId = userId;
            var walletEntity = Mapper.Map<Wallet>(walletDto);
            UnitOfWork.WalletRepository.Create(walletEntity);
            await UnitOfWork.CommitAsync();

            return Mapper.Map<WalletDto>(walletEntity);
        }

        public async Task<IEnumerable<WalletDto>> GetAllWalletsAsync(int userId, PaginationFilter filter = null)
        {
            var user = await GetUser(userId);
            var walletsEntity = IsPagingSpecified(filter) ? ConvertToPaged(user.Wallets, filter) : user.Wallets;
            
            return Mapper.Map<IEnumerable<WalletDto>>(walletsEntity);
        }

        public async Task<WalletDto> GetWalletAsync(int userId, int walletId)
        {
            var walletEntity = await ThrowIfNotExists(userId, walletId);
            return Mapper.Map<WalletDto>(walletEntity);
        }

        public async Task UpdateWalletAsync(int userId, int walletId, UpdateWalletDto updateWalletDto)
        {
            var walletToUpdate = await ThrowIfNotExists(userId, walletId);
            await ThrowIfNameExists(userId, updateWalletDto.Name, walletToUpdate.WalletId);

            Mapper.Map(updateWalletDto, walletToUpdate);

            UnitOfWork.WalletRepository.Update(walletToUpdate);
            await UnitOfWork.CommitAsync();
        }

        public async Task DeleteWalletAsync(int userId, int walletId)
        {
            var wallet = await ThrowIfNotExists(userId, walletId);

            UnitOfWork.WalletRepository.Remove(wallet);
            await UnitOfWork.CommitAsync();
        }

        private async Task<Wallet> ThrowIfNotExists(int userId, int walletId)
        {
            var wallet = await UnitOfWork.WalletRepository.GetAsync(walletId);

            if (wallet == null || wallet.UserId != userId)
            {
                throw new WalletNotFoundException(String.Format(ServiceMessages.WalletNotFound, walletId));
            }

            return wallet;
        }

        private async Task ThrowIfNameExists(int userId, string walletName, int walletId = 0)
        {
            var user = await GetUser(userId);

            if (user.Wallets.Any(x => x.Name == walletName && x.WalletId != walletId))
            {
                throw new WalletNameException(ServiceMessages.WalletSameName);
            }
        }
    }
}
