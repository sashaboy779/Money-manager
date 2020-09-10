using MoneyManagerUi.Data.Category;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoneyManagerUi.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<List<MainCategory>> GetUserCategoriesAsync();
        Task<MainCategory> GetCategoryAsync(int id);
        Task CreateCategoryAsync(Category category);
        Task CreateSubcategoryAsync(int parentCategoryId, Category subcategory);
        Task UpdateCategoryAsync(Category category);
        Task DeleteCategoryAsync(int id);
    }
}
