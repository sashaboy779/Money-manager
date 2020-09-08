using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BusinessLogicLayer.Dto;
using BusinessLogicLayer.Dto.UserDtos;
using BusinessLogicLayer.Exceptions;
using BusinessLogicLayer.Services;
using BusinessLogicLayer.Services.Interfaces;
using BusinessLogicLayerTest.Fixtures;
using DataAccessLayer.Entities;
using Moq;
using Xunit;

namespace BusinessLogicLayerTest.Tests
{
    public class UserServiceTest : ServiceTest, IClassFixture<UserFixture>
    {
        private readonly IUserService service;
        private readonly UserFixture fixture;
        private readonly Mock<IPasswordService> mockPasswordService;

        public UserServiceTest(UserFixture userFixture)
        {
            fixture = userFixture;
            mockPasswordService = new Mock<IPasswordService>();
            service = new UserService(MockUnitOfWork.Object, mockPasswordService.Object, MockMapper.Object);
        }

        [Fact]
        public async Task CreateAsync_ValidUser_UserCreated()
        {
            SetupMocksCreate();

            await service.CreateAsync(fixture.UserDto, fixture.Password);
            
            VerifyMocksCreate(Times.Once());
        }

        [Fact]
        public async Task CreateAsync_InvalidUsername_ExceptionThrown()
        {
            SetupMocksCreate(fixture.User);
            
            await Assert.ThrowsAsync<UserException>(() => service.CreateAsync(fixture.UserDto, fixture.Password));
            
            VerifyMocksCreate(Times.Never());
        }
        
        [Fact]
        public async Task UpdateAsync_ValidUser_UserUpdated()
        {
            SetupMocksUpdate();

            await service.UpdateAsync(fixture.UserForUpdateDto, fixture.Password);
            
            VerifyMocksUpdate(Times.Once());
            Assert.Equal(fixture.UpdatedByteLength, fixture.UserForUpdateDto.PasswordHash.Length);
            Assert.Equal(fixture.UpdatedByteLength, fixture.UserForUpdateDto.PasswordSalt.Length);
        }
        
        [Fact]
        public async Task UpdateAsync_InvalidUsername_ExceptionThrown()
        {
            SetupMocksUpdate(fixture.User);

            await Assert.ThrowsAsync<UserException>(() => 
                service.UpdateAsync(fixture.UserForUpdateDto, fixture.Password));
            
            VerifyMocksUpdate(Times.Never());
        }
        
        private void SetupMocksGeneral(User repositoryUser, User mapperUser, CreatePasswordDto passwordModel)
        {
            MockUnitOfWork.Setup(x =>
                x.UserRepository.SingleOrDefaultAsync(It.IsAny<Expression<Func<User, bool>>>()))
                .ReturnsAsync(repositoryUser);
            MockUnitOfWork.Setup(x => x.CommitAsync());
            MockMapper.Setup(x => x.Map<User>(It.IsAny<UserDto>())).Returns(mapperUser);
            mockPasswordService.Setup(x => x.CreatePasswordHash(It.IsAny<string>())).Returns(passwordModel);
        }

        private void SetupMocksCreate(User repositoryUser = null)
        {
            SetupMocksGeneral(repositoryUser, fixture.User, fixture.PasswordModel);
            MockUnitOfWork.Setup(x => x.UserRepository.Create(It.IsAny<User>()));
        }

        private void SetupMocksUpdate(User repositoryUser = null)
        {
            SetupMocksGeneral(repositoryUser, fixture.UserForUpdate, fixture.PasswordModelForUpdate);
            MockUnitOfWork.Setup(x => x.UserRepository.Update(It.IsAny<User>()));
        }

        private void VerifyMocksGeneral(UserDto mappedUser, Times methodCalls)
        {
            MockUnitOfWork.Verify(x =>
                x.UserRepository.SingleOrDefaultAsync(It.IsAny<Expression<Func<User, bool>>>()), Times.Once());
            MockUnitOfWork.Verify(x => x.CommitAsync(), methodCalls);
            MockMapper.Verify(x => x.Map<User>(mappedUser), methodCalls);
            mockPasswordService.Verify(x => x.CreatePasswordHash(fixture.Password), methodCalls);
        }

        private void VerifyMocksCreate(Times methodCalls)
        {
            VerifyMocksGeneral(fixture.UserDto, methodCalls);
            MockUnitOfWork.Verify(x => x.UserRepository.Create(fixture.User), methodCalls);
        }
        
        private void VerifyMocksUpdate(Times methodCalls)
        {
            VerifyMocksGeneral(fixture.UserForUpdateDto, methodCalls);
            MockUnitOfWork.Verify(x => x.UserRepository.Update(fixture.UserForUpdate), methodCalls);
        }
    }
}