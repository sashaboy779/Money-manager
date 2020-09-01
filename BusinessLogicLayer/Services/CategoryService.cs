using AutoMapper;
using AutoMapper.Internal;
using BusinessLogicLayer.Dto.CategoryDtos;
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
    public class CategoryService : Service, ICategoryService
    {
        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
            : base(unitOfWork, mapper)
        {
        }

        public async Task<MainCategoryDto> CreateCategoryAsync(int userId, MainCategoryDto categoryDto)
        {
            var categories = GetUserCategories(userId);

            ThrowIfCategorySameName(categories, categoryDto.Name);

            categoryDto.UserId = userId;
            var categoryEntity = Mapper.Map<MainCategory>(categoryDto);

            UnitOfWork.CategoryRepository.Create(categoryEntity);
            await UnitOfWork.CommitAsync();

            return Mapper.Map<MainCategoryDto>(categoryEntity);
        }

        public async Task<MainCategoryDto> CreateSubcategoryAsync(int userId, int parentCategoryId, 
            SubcategoryDto subcategoryDto)
        {
            var categories = GetUserCategories(userId).ToList();

            ThrowIfParentNotContains(categories, parentCategoryId);
            ThrowIfCategorySameName(categories, subcategoryDto.Name);

            var parentCategory = categories.Single(x => x.CategoryId == parentCategoryId);

            var subcategoryEntity = Mapper.Map<Subcategory>(subcategoryDto);
            parentCategory.Subcategories.Add(subcategoryEntity);

            UnitOfWork.CategoryRepository.Update(parentCategory);
            await UnitOfWork.CommitAsync();

            return Mapper.Map<MainCategoryDto>(parentCategory);
        }

        public async Task DeleteCategoryAsync(int userId, int categoryId)
        {
            var categories = GetUserCategories(userId);
            var category = FindCategory(categories, categoryId);

            if (category is MainCategory mainCategory)
            {
                UnitOfWork.CategoryRepository.Remove(mainCategory);
            }
            else
            {
                var subcategoryToDelete = category as Subcategory;
                var parentCategory = subcategoryToDelete.Parent;
                parentCategory.Subcategories.Remove(subcategoryToDelete);

                UnitOfWork.CategoryRepository.Update(parentCategory);
            }
                       
            await UnitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<MainCategoryDto>> GetAllCategoriesAsync(int userId, PaginationFilter filter = null)
        {
            var user = await GetUser(userId);
            var categoriesEntity =
                IsPagingSpecified(filter) ? ConvertToPaged(user.Categories, filter) : user.Categories;
            
            return Mapper.Map<IEnumerable<MainCategoryDto>>(categoriesEntity);
        }

        public async Task<MainCategoryDto> GetCategoryAsync(int userId, int categoryId)
        {
            var user = await GetUser(userId);
            var foundCategory = FindCategory(user.Categories, categoryId);

            if (foundCategory is Subcategory subcategory)
            {
                foundCategory = subcategory.Parent;
            }

            return Mapper.Map<MainCategoryDto>(foundCategory);
        }

        public async Task UpdateCategoryAsync(int userId, UpdateCategoryDto categoryDto)
        {
            var categories = GetUserCategories(userId);
            var categoryToUpdate = FindCategory(categories, categoryDto.CategoryId);

            if (categoryToUpdate is MainCategory mainCategory)
            {
                Mapper.Map(categoryDto, mainCategory);
                categoryToUpdate = mainCategory;
            }
            else
            {
                Mapper.Map(categoryDto, categoryToUpdate);
                var parentCategory = (categoryToUpdate as Subcategory).Parent;
                categoryToUpdate = parentCategory;
            }

            UnitOfWork.CategoryRepository.Update((MainCategory) categoryToUpdate);
            await UnitOfWork.CommitAsync();
        }

        private void ThrowIfParentNotContains(IEnumerable<MainCategory> categories, int parentCategoryId)
        {
            if (categories.All(x => x.CategoryId != parentCategoryId))
            {
                throw new CategoryNotFoundException(String
                    .Format(ServiceMessages.CategoryNotFound, parentCategoryId));
            }
        }

        private Category FindCategory(IEnumerable<MainCategory> categories, int categoryId)
        {
            foreach (var category in categories)
            {
                if (category.CategoryId == categoryId)
                {
                    return category;
                }

                var foundCategory = category.Subcategories.SingleOrDefault(s =>
                    s.CategoryId == categoryId);

                if (foundCategory != null)
                {
                    return foundCategory;
                }
            }

            throw new CategoryNotFoundException(String
                    .Format(ServiceMessages.CategoryNotFound, categoryId));
        }

        private void ThrowIfCategorySameName(IEnumerable<MainCategory> categories, string categoryName)
        {
            categories.ForAll(mainCategory =>
            {
                if (mainCategory.Name == categoryName || mainCategory.Subcategories.Any(s => s.Name == categoryName))
                {
                    throw new CategoryNameException(ServiceMessages.CategorySameName);
                }
            });
        }
    }
}
