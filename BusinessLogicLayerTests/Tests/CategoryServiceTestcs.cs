using System.Threading.Tasks;
using BusinessLogicLayer.Dto.CategoryDtos;
using BusinessLogicLayer.Exceptions;
using BusinessLogicLayer.Services;
using BusinessLogicLayer.Services.Interfaces;
using BusinessLogicLayerTest.Fixtures;
using DataAccessLayer.Entities;
using Moq;
using Xunit;

namespace BusinessLogicLayerTest.Tests
{
    public class CategoryServiceTests : ServiceTest, IClassFixture<CategoryServiceFixture>
    {
        private readonly ICategoryService service;
        private readonly CategoryServiceFixture fixture;

        public CategoryServiceTests(CategoryServiceFixture categoryServiceFixture)
        {
            fixture = categoryServiceFixture;
            service = new CategoryService(MockUnitOfWork.Object, MockMapper.Object);
        }

        [Fact]
        public async Task CreateCategoryAsync_ValidArguments_CategoryCreated()
        {
            SetupMocksCrate();
            
            var result = await service.CreateCategoryAsync(fixture.UserWithCategories.UserId, fixture.CreateCategoryDto);

            VerifyMocksCreate(Times.Once());      
            Assert.Equal(result.UserId, fixture.UserWithCategories.UserId);
        }

        [Fact]
        public async Task CreateCategoryAsync_InvalidName_ExceptionThrown()
        {
            SetupMocksCrate();
        
            await Assert.ThrowsAsync<CategoryNameException>(() => 
                service.CreateCategoryAsync(fixture.UserWithCategories.UserId, fixture.CreateCategorySameName));

            VerifyMocksCreate(Times.Never());
        }

        [Fact]
        public async Task UpdateCategoryAsync_ValidMainCategory_CategoryUpdated()
        {
            SetupMocksUpdate();

            await service.UpdateCategoryAsync(fixture.UserWithCategories.UserId, fixture.UpdateCategory);

            VerifyMocksUpdate<MainCategory>();
        }
        
        [Fact]
        public async Task UpdateCategoryAsync_ValidSubcategory_SubcategoryUpdated()
        {
            SetupMocksUpdate();
            
            await service.UpdateCategoryAsync(fixture.UserWithCategories.UserId, fixture.UpdateSubCategory);

            VerifyMocksUpdate<Category>();
        }

        private void SetupMocksGeneral()
        {
            MockUnitOfWork.Setup(x => x.UserRepository.GetAsync(It.IsAny<int>()))
                .ReturnsAsync(fixture.UserWithCategories);
            MockUnitOfWork.Setup(x => x.CommitAsync());
        }

        private void SetupMocksCrate()
        {
            SetupMocksGeneral();
            MockUnitOfWork.Setup(x => x.CategoryRepository.Create(It.IsAny<MainCategory>()));
            MockMapper.Setup(x => x.Map<MainCategoryDto>(It.IsAny<MainCategory>()))
                .Returns(fixture.CreateCategoryDto);
            MockMapper.Setup(x => x.Map<MainCategory>(It.IsAny<MainCategoryDto>())).Returns(fixture.CreateCategory);
        }
        
        private void SetupMocksUpdate()
        {
            SetupMocksGeneral();
            MockUnitOfWork.Setup(x => x.CategoryRepository.Update(It.IsAny<MainCategory>()));
            MockMapper.Setup(x => x.Map(It.IsAny<UpdateCategoryDto>(), It.IsAny<MainCategory>()));
        }

        private void VerifyMocksGeneral(Times methodCalls)
        {
            MockUnitOfWork.Verify(x => x.UserRepository.GetAsync(fixture.UserWithCategories.UserId), Times.Once);
            MockUnitOfWork.Verify(x => x.CommitAsync(), methodCalls);
        }
        
        private void VerifyMocksCreate(Times methodCalls)
        {
            VerifyMocksGeneral(methodCalls);
            MockUnitOfWork.Verify(x => x.CategoryRepository.Create(fixture.CreateCategory), methodCalls);
            MockMapper.Verify(x => x.Map<MainCategoryDto>(fixture.CreateCategory), methodCalls);
            MockMapper.Verify(x => x.Map<MainCategory>(fixture.CreateCategoryDto), methodCalls);
        }
        
        private void VerifyMocksUpdate<TUpdatedCategory>()
        {
            VerifyMocksGeneral(Times.Once());
            MockUnitOfWork.Verify(x => x.CategoryRepository.Update(It.IsAny<MainCategory>()), Times.Once);
            MockMapper.Verify(x => x.Map(It.IsAny<UpdateCategoryDto>(), It.IsAny<TUpdatedCategory>()), Times.Once);
        }
    }
}