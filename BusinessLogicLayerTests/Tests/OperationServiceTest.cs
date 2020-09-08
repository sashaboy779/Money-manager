using System;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogicLayer.Dto.OperationDtos;
using BusinessLogicLayer.Exceptions;
using BusinessLogicLayer.Services;
using BusinessLogicLayer.Services.Interfaces;
using BusinessLogicLayerTest.Fixtures;
using DataAccessLayer.Entities;
using Moq;
using Xunit;

namespace BusinessLogicLayerTest.Tests
{
    public class OperationServiceTest : ServiceTest, IClassFixture<OperationFixture>
    {
        private readonly IOperationService service;
        private readonly OperationFixture fixture;

        public OperationServiceTest(OperationFixture operationFixture)
        {
            fixture = operationFixture;
            service = new OperationService(MockUnitOfWork.Object, MockMapper.Object);
        }

        [Fact]
        public async Task CreateOperationAsync_ValidIncomeOperation_BalanceEdited()
        {
            var userWallet = GetUserWallet();
            var expectedBalance = userWallet.Balance + fixture.IncomeOperation.Amount;
            SetupMocks(fixture.IncomeOperation, fixture.IncomeOperationDto, fixture.User);

            await service.CreateOperationAsync(userWallet.UserId, fixture.IncomeOperationDto);

            Assert.Equal(expectedBalance, userWallet.Balance);
            VerifyMocks(userWallet, fixture.User.UserId, fixture.IncomeOperation,
                fixture.IncomeOperationDto, Times.Once());
        }

        [Fact]
        public async Task CreateOperationAsync_ValidExpanseOperation_BalanceEdited()
        {
            var userWallet = GetUserWallet();
            var expectedBalance = userWallet.Balance + fixture.ExpanseOperationDto.Amount;
            SetupMocks(fixture.ExpanseOperation, fixture.ExpanseOperationDto, fixture.User);
            
            await service.CreateOperationAsync(userWallet.UserId, fixture.ExpanseOperationDto);

            Assert.Equal(expectedBalance, userWallet.Balance);
            VerifyMocks(userWallet, userWallet.UserId, fixture.ExpanseOperation, 
                fixture.ExpanseOperationDto, Times.Once());
        }
       
        [Fact]
        public async Task CreateOperationAsync_OperationWithoutDate_CurrentDateProvided()
        {
            var userWallet = GetUserWallet();
            SetupMocks(fixture.OperationNoDate, fixture.OperationNoDateDto, fixture.User);

            var result = 
                await service.CreateOperationAsync(userWallet.UserId, fixture.OperationNoDateDto);

            Assert.Equal(DateTime.Now.Date, result.OperationDate.Date);
            VerifyMocks(userWallet, userWallet.UserId, fixture.OperationNoDate, 
                fixture.OperationNoDateDto, Times.Once());
        }

        [Theory]
        [MemberData(nameof(OperationFixture.GetOperationsForException), MemberType= typeof(OperationFixture))]
        public async Task CreateOperationAsync_UserWithoutWallets_ExceptionThrown(OperationDto operationDto)
        {
            SetupMocks(fixture.ExpanseOperation, fixture.ExpanseOperationDto, fixture.UserWithoutWallets);

            await Assert.ThrowsAsync<WalletNotFoundException>(() => 
                service.CreateOperationAsync(fixture.UserWithoutWallets.UserId, operationDto));
            await Assert.ThrowsAsync<WalletNotFoundException>(() => 
                service.CreateOperationAsync(fixture.UserWithoutWallets.UserId, fixture.OperationNoWalletIdDto));

            VerifyMocks(null, fixture.UserWithoutWallets.UserId, fixture.ExpanseOperation, 
                operationDto, Times.Never());
        }

        [Fact]
        public async Task CreateOperationAsync_UserWithManyWallets_ExceptionThrown()
        {
            SetupMocks(fixture.OperationNoWalletId, fixture.OperationNoWalletIdDto, fixture.UserWithManyWallets);

            await Assert.ThrowsAsync<WalletNotSpecifiedException>(() => 
                service.CreateOperationAsync(fixture.UserWithManyWallets.UserId, fixture.OperationNoWalletIdDto));

            VerifyMocks(null, fixture.UserWithManyWallets.UserId, fixture.OperationNoWalletId, 
                fixture.OperationNoWalletIdDto, Times.Never());
        }
        
        [Fact]
        public async Task CreateOperationAsync_InvalidOperationWalletId_ExceptionThrown()
        {
            var userWallet = GetUserWallet();
            SetupMocks(fixture.OperationInvalidWalletId, fixture.OperationInvalidWalletIdDto, fixture.User);
            
            await Assert.ThrowsAsync<WalletNotFoundException>(() => 
                service.CreateOperationAsync(userWallet.UserId, fixture.OperationInvalidWalletIdDto));

            VerifyMocks(userWallet, userWallet.UserId, fixture.ExpanseOperation, 
                fixture.ExpanseOperationDto, Times.Never());
        }
        
        private Wallet GetUserWallet()
        {
            return fixture.User.Wallets.First();
        }

        private void SetupMocks(Operation createOperation, OperationDto createOperationDto, User user)
        {
            MockUnitOfWork.Setup(x => x.UserRepository.GetAsync(It.IsAny<int>()))
                .ReturnsAsync(user);
            MockUnitOfWork.Setup(x => x.OperationRepository.Create(It.IsAny<Operation>()));
            MockUnitOfWork.Setup(x => x.WalletRepository.Update(It.IsAny<Wallet>()));
            MockUnitOfWork.Setup(x => x.CommitAsync());
            MockMapper.Setup(x => x.Map<Operation>(It.IsAny<OperationDto>())).Returns(createOperation);
            MockMapper.Setup(x => x.Map<OperationDto>(It.IsAny<Operation>())).Returns(createOperationDto);
        }
        
        private void VerifyMocks(Wallet userWallet, int userId, Operation createOperation, 
            OperationDto createOperationDto, Times methodCalls)
        {
            MockUnitOfWork.Verify(x => x.UserRepository.GetAsync(userId));
            MockUnitOfWork.Verify(x => x.OperationRepository.Create(createOperation), methodCalls);
            MockUnitOfWork.Verify(x => x.WalletRepository.Update(userWallet), methodCalls);
            MockUnitOfWork.Verify(x => x.CommitAsync(), methodCalls);
            MockMapper.Verify(x => x.Map<Operation>(createOperationDto), methodCalls);
            MockMapper.Verify(x => x.Map<OperationDto>(createOperation), methodCalls);
        }
    }
}