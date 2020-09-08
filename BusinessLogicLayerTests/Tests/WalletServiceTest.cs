using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogicLayer.Dto.WalletDtos;
using BusinessLogicLayer.Exceptions;
using BusinessLogicLayer.Services;
using BusinessLogicLayer.Services.Interfaces;
using BusinessLogicLayerTest.Fixtures;
using DataAccessLayer.Entities;
using Moq;
using Xunit;

namespace BusinessLogicLayerTest.Tests
{
    public class WalletServiceTest : ServiceTest, IClassFixture<WalletServiceFixture>
    {
        private readonly IWalletSevice service;
        private readonly WalletServiceFixture fixture;

        public WalletServiceTest(WalletServiceFixture walletFixture)
        {
            fixture = walletFixture;
            service = new WalletService(MockUnitOfWork.Object, MockMapper.Object);
        }

        [Fact]
        public async Task CreateWalletAsync_ValidWallet_WalletCreated()
        {
            SetupMocksCreate(fixture.DefaultUser);

            var createdWallet = await service.CreateWalletAsync(fixture.DefaultUser.UserId, fixture.DefaultWalletDto);

            Assert.Equal(createdWallet, fixture.DefaultWalletDto);
            VerifyMocksCreate(fixture.DefaultUser.UserId, Times.Once());
        }

        [Fact]
        public async Task CreateWalletAsync_DuplicateName_ExceptionThrown()
        {
            SetupMocksCreate(fixture.UserWithDuplicateWallet);

            await Assert.ThrowsAsync<WalletNameException>(() =>
                service.CreateWalletAsync(fixture.UserWithDuplicateWallet.UserId, fixture.DefaultWalletDto));

            VerifyMocksCreate(fixture.UserWithDuplicateWallet.UserId, Times.Never());
        }

        [Fact]
        public async Task GetAllWalletsAsync_GetWallets_AllWalletsReturned()
        {
            SetupMocksGet<IEnumerable<Wallet>, IEnumerable<WalletDto>>(fixture.UserWithWallets,
                fixture.UserWalletsDtos);

            var wallets = await service.GetAllWalletsAsync(fixture.UserWithWallets.UserId, fixture.PaginationFilter);

            Assert.Equal(wallets, fixture.UserWalletsDtos);
            VerifyMockGetUser(fixture.UserWithWallets.UserId, Times.Once());
            MockMapper.Verify(x => x.Map<IEnumerable<WalletDto>>(fixture.UserWithWallets.Wallets), Times.Once);
        }
        
        [Fact]
        public async Task GetWalletAsync_GetWallet_WalletReturned()
        {
            SetupMocksGet<Wallet, WalletDto>(fixture.DefaultUser, fixture.DefaultWalletDto, fixture.DefaultWallet);

            var wallet = await service.GetWalletAsync(fixture.DefaultUser.UserId, fixture.DefaultWalletDto.WalletId);

            Assert.Equal(wallet, fixture.DefaultWalletDto);
            VerifyMockGetWallet(fixture.DefaultWalletDto.WalletId, Times.Once());
            MockMapper.Verify(x => x.Map<WalletDto>(fixture.DefaultWallet), Times.Once);
        }

        [Fact]
        public async Task UpdateWalletAsync_ValidWallet_WalletUpdated()
        {
            var updatedWallet = fixture.UserWithWallets.Wallets.First();
            SetupMocksUpdate(updatedWallet);

            await service.UpdateWalletAsync(fixture.UserWithWallets.UserId, updatedWallet.WalletId,
                fixture.UpdateWalletDto);

            VerifyMocksUpdate(fixture.UserWithWallets.UserId, updatedWallet.WalletId, updatedWallet, Times.Once());
        }
        
        [Fact]
        public async Task UpdateWalletAsync_InvalidWalletId_ExceptionThrown()
        {
            SetupMocksUpdate(fixture.NullWallet);

            await Assert.ThrowsAsync<WalletNotFoundException>(() =>
                service.UpdateWalletAsync(fixture.UserWithWallets.UserId, fixture.IncorrectWalletId,
                    fixture.UpdateWalletDto));

            VerifyMocksUpdate(fixture.UserWithWallets.UserId, fixture.IncorrectWalletId, fixture.NullWallet, Times.Never());
        }

        [Fact]
        public async Task DeleteWalletAsync_ValidArguments_WalletDeleted()
        {
            var walletToDelete = fixture.UserWithWallets.Wallets.First();
            SetupMocksDelete(walletToDelete);

            await service.DeleteWalletAsync(fixture.UserWithWallets.UserId, walletToDelete.WalletId);

            VerifyMocksDelete(walletToDelete.WalletId, walletToDelete, Times.Once());
        }

        [Fact]
        public async Task DeleteWalletAsync_InvalidArguments_ExceptionThrown()
        {
            SetupMocksDelete(fixture.NullWallet);

            await Assert.ThrowsAsync<WalletNotFoundException>(() =>
                service.DeleteWalletAsync(fixture.UserWithWallets.UserId, fixture.IncorrectWalletId));

            VerifyMocksDelete(fixture.IncorrectWalletId, fixture.NullWallet, Times.Never());
        }

        private void SetupMockGetUser(User userToReturn)
        {
            MockUnitOfWork.Setup(x => x.UserRepository.GetAsync(It.IsAny<int>())).ReturnsAsync(userToReturn);
            MockUnitOfWork.Setup(x => x.CommitAsync());
        }

        private void VerifyMockGetUser(int userId, Times times)
        {
            MockUnitOfWork.Verify(x => x.UserRepository.GetAsync(userId), times);
        }
        
        private void VerifyMockGetWallet(int walletId, Times times)
        {            
            MockUnitOfWork.Verify(x => x.WalletRepository.GetAsync(walletId), times);
        }
        
        private void VerifyMockCommit(Times times)
        {           
            MockUnitOfWork.Verify(x => x.CommitAsync(), times);
        }

        private void SetupMockGetWallet(Wallet walletToReturn)
        {
            MockUnitOfWork.Setup(x => x.WalletRepository.GetAsync(It.IsAny<int>()))
                .ReturnsAsync(walletToReturn);
            MockUnitOfWork.Setup(x => x.CommitAsync());
        }

        private void SetupMocksCreate(User userToReturn)
        {
            SetupMockGetUser(userToReturn);
            MockUnitOfWork.Setup(x => x.WalletRepository.Create(It.IsAny<Wallet>()));
            MockMapper.Setup(x => x.Map<Wallet>(It.IsAny<WalletDto>())).Returns(fixture.DefaultWallet);
            MockMapper.Setup(x => x.Map<WalletDto>(It.IsAny<Wallet>())).Returns(fixture.DefaultWalletDto);
        }

        private void VerifyMocksCreate(int userIdToReturn, Times mapperCalls)
        {
            VerifyMockGetUser(userIdToReturn, Times.Once());
            VerifyMockCommit(mapperCalls);
            MockUnitOfWork.Verify(x => x.WalletRepository.Create(fixture.DefaultWallet), mapperCalls);
            MockMapper.Verify(x => x.Map<Wallet>(fixture.DefaultWalletDto), mapperCalls);
            MockMapper.Verify(x => x.Map<WalletDto>(fixture.DefaultWallet), mapperCalls);
        }

        private void SetupMocksGet<TSource, TDestination>(User userToReturn, TDestination mappedInstance, 
            Wallet walletToReturn = null)
        {
            MockUnitOfWork.Setup(x => x.UserRepository.GetAsync(It.IsAny<int>()))
                .ReturnsAsync(userToReturn);
            MockMapper.Setup(x => x.Map<TDestination>(It.IsAny<TSource>()))
                .Returns(mappedInstance);

            if (walletToReturn != null)
            {
                MockUnitOfWork.Setup(x => x.WalletRepository.GetAsync(fixture.DefaultWalletDto.WalletId))
                    .ReturnsAsync(walletToReturn);

            }
        }

        private void SetupMocksUpdate(Wallet walletToReturn)
        {
            SetupMockGetUser(fixture.UserWithWallets);
            SetupMockGetWallet(walletToReturn);
            MockUnitOfWork.Setup(x => x.WalletRepository.Update(It.IsAny<Wallet>()));
            MockMapper.Setup(x => x.Map(It.IsAny<UpdateWalletDto>(), It.IsAny<Wallet>()));
        }

        private void VerifyMocksUpdate(int userId, int walletId, Wallet walletToUpdate, Times times)
        {
            VerifyMockGetWallet(walletId, Times.Once());
            VerifyMockGetUser(userId, times);
            VerifyMockCommit(times);
            MockUnitOfWork.Verify(x => x.WalletRepository.Update(walletToUpdate), times);
            MockMapper.Verify(x => x.Map(fixture.UpdateWalletDto, walletToUpdate), times);
        }

        private void SetupMocksDelete(Wallet walletToDelete)
        {
            SetupMockGetWallet(walletToDelete);
            MockUnitOfWork.Setup(x => x.WalletRepository.Remove(It.IsAny<Wallet>()));
        }

        private void VerifyMocksDelete(int walletId, Wallet walletToDelete, Times times)
        {
            MockUnitOfWork.Verify(x => x.WalletRepository.GetAsync(walletId), Times.Once);
            MockUnitOfWork.Verify(x => x.WalletRepository.Remove(walletToDelete), times);
            MockUnitOfWork.Verify(x => x.CommitAsync(), times);
        }
    }
}