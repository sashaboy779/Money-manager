using AutoMapper;
using BusinessLogicLayer.Dto.OperationDtos;
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
    public class OperationService : Service, IOperationService
    {
        public OperationService(IUnitOfWork unitOfWork, IMapper mapper)
            : base(unitOfWork, mapper)
        {
        }

        public async Task<OperationDto> CreateOperationAsync(int userId, OperationDto operationDto)
        {
            var user = await GetUser(userId);

            ValidateOperationDate(operationDto);
            ValidateOperationWallet(operationDto, user.Wallets.ToList());
            ValidateOperationCategory(operationDto.CategoryId, user);

            var userWallet = user.Wallets.Single(x => x.WalletId == operationDto.WalletId);
            
            operationDto.CurrentBalance = userWallet.Balance;
            userWallet.Balance = userWallet.Balance += operationDto.Amount;

            var operationEntity = Mapper.Map<Operation>(operationDto);
            UnitOfWork.OperationRepository.Create(operationEntity);
            UnitOfWork.WalletRepository.Update(userWallet);
            await UnitOfWork.CommitAsync();

            return Mapper.Map<OperationDto>(operationEntity);
        }

        public async Task DeleteOperationAsync(int userId, int operationId)
        {
            var operationToRemove = await UnitOfWork.OperationRepository.GetAsync(operationId);
            var userWallets = GetUserWallets(userId).ToList();

            ThrowIfNotUserOperation(operationToRemove, userWallets);
            
            var walletToUpdate = userWallets.Single(x => x.WalletId == operationToRemove.WalletId);
            walletToUpdate.Balance = walletToUpdate.Balance -= operationToRemove.Amount;

            UnitOfWork.OperationRepository.Remove(operationToRemove);
            UnitOfWork.WalletRepository.Update(walletToUpdate);
            await UnitOfWork.CommitAsync();
        }

        public async Task<OperationDto> GetOperationAsync(int userId, int operationId)
        {
            var user = await GetUser(userId);
            var operationEntity = await UnitOfWork.OperationRepository.GetAsync(operationId);

            ThrowIfNotUserOperation(operationEntity, user.Wallets);
         
            return Mapper.Map<OperationDto>(operationEntity);
        }

        public async Task<IEnumerable<OperationDto>> GetWalletOperationsAsync(int userId, int walletId,
            PaginationFilter filter = null)
        {
            var wallet = await UnitOfWork.WalletRepository.GetAsync(walletId);

            if (wallet == null || wallet.Owner.UserId != userId)
            {
                throw new WalletNotFoundException(String.Format(ServiceMessages.WalletNotFound, walletId));
            }

            var operationsEntity = IsPagingSpecified(filter)
                ? ConvertToPaged(wallet.Operations, filter)
                : wallet.Operations;
            return Mapper.Map<IEnumerable<OperationDto>>(operationsEntity);
        }

        public async Task UpdateOperationAsync(int userId, UpdateOperationDto updateOperation)
        {
            ValidateOperationCategory(updateOperation.CategoryId, await GetUser(userId));

            var operationToUpdate = await UnitOfWork.OperationRepository.GetAsync(updateOperation.OperationId);
            ThrowIfNotUserOperation(operationToUpdate, GetUserWallets(userId));
            
            updateOperation.OperationDate = updateOperation.OperationDate.ToUniversalTime();
            Mapper.Map(updateOperation, operationToUpdate);

            UnitOfWork.OperationRepository.Update(operationToUpdate);
            await UnitOfWork.CommitAsync();
        }

        private void ValidateOperationCategory(int categoryId, User user)
        {
            if (!user.Categories.Any(c => c.CategoryId == categoryId 
                                 || c.Subcategories.Any(s => s.CategoryId == categoryId)))
            {
                throw new CategoryNotFoundException(String.Format(ServiceMessages.CategoryNotFound, categoryId));
            }
        }

        private void ValidateOperationWallet(OperationDto operationDto, List<Wallet> userWallets)
        {
            if (operationDto.WalletId == 0)
            {
                var walletCount = userWallets.Count;
                if (walletCount == 0)
                {
                    throw new WalletNotFoundException(ServiceMessages.CreateWalletFirst);
                }
                if (walletCount > 1)
                {
                    throw new WalletNotSpecifiedException(ServiceMessages.SpecifyWallet);
                }

                operationDto.WalletId = userWallets.First().WalletId;
            }
            else
            {
                if (userWallets.All(x => x.WalletId != operationDto.WalletId))
                {
                    throw new WalletNotFoundException(String
                        .Format(ServiceMessages.WalletNotFound, operationDto.WalletId));
                }
            }
        }

        private void ValidateOperationDate(OperationDto operationDto)
        {
            if (operationDto.OperationDate == default)
            {
                operationDto.OperationDate = DateTime.Now;
            }
        }

        private void ThrowIfNotUserOperation(Operation operation, IEnumerable<Wallet> wallets)
        {
            if (operation == null || wallets.All(x => x.WalletId != operation.WalletId))
            {
                throw new OperationNotFoundException(ServiceMessages.OperationNotFound);
            }
        }
    }
}
