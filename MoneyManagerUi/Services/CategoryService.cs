using MoneyManagerUi.Data.Category;
using MoneyManagerUi.Infrastructure;
using MoneyManagerUi.Infrastructure.Constants;
using MoneyManagerUi.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoneyManagerUi.Services
{
    public class CategoryService : Service, ICategoryService
    {
        public CategoryService(IStorageService storageService, IExpanseManagerClient apiClient)
           : base(storageService, apiClient)
        {
        }

        public async Task CreateCategoryAsync(Category category)
        {
            await PostRequestAsync(ApiRoutes.Categories, category);
        }

        public async Task CreateSubcategoryAsync(int parentCategoryId, Category subcategory)
        {
            var uri = string.Format(ApiRoutes.CategoriesParameter, parentCategoryId);
            await PostRequestAsync(uri, subcategory);
        }

        public async Task DeleteCategoryAsync(int id)
        {
            var uri = string.Format(ApiRoutes.CategoriesParameter, id);
            await DeleteRequestAsync(uri);
        }

        public async Task<MainCategory> GetCategoryAsync(int categoryId)
        {
            var uri = string.Format(ApiRoutes.CategoriesParameter, categoryId);
            return await GetRequestAsync<MainCategory>(uri);
        }

        public async Task<List<MainCategory>> GetUserCategoriesAsync()
        {
            return await GetRequestAsync<List<MainCategory>>(ApiRoutes.Categories);
        }

        public async Task UpdateCategoryAsync(Category category)
        {
            var uri = string.Format(ApiRoutes.CategoriesParameter, category.CategoryId);
            await PutRequestAsync(uri, category);
        }
    }
}
