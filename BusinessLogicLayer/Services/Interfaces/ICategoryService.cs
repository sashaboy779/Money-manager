using BusinessLogicLayer.Dto.CategoryDtos;
using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLogicLayer.Dto;

namespace BusinessLogicLayer.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<MainCategoryDto> GetCategoryAsync(int userId, int categoryId);
        Task<IEnumerable<MainCategoryDto>> GetAllCategoriesAsync(int userId, PaginationFilter filter = null);
        Task<MainCategoryDto> CreateCategoryAsync(int userId, MainCategoryDto categoryDto);
        Task<MainCategoryDto> CreateSubcategoryAsync(int userId, int parentCategoryId, SubcategoryDto categoryDto);
        Task UpdateCategoryAsync(int userId, UpdateCategoryDto categoryDto);
        Task DeleteCategoryAsync(int userId, int categoryId);
    }
}
