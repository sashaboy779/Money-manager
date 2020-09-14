using AutoMapper;
using MoneyManagerUi.Data.Category;
using MoneyManagerUi.Resources;
using MoneyManagerUi.Services.Interfaces;
using MoneyManagerUi.Shared.Classes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;
using CategoryModel = MoneyManagerUi.Data.Category.Category;

namespace MoneyManagerUi.Pages.Category
{
    [Authorize]
    public class CategoriesComponent : ComponentWithModals
    {
        [Inject] public ICategoryService CategoryService { get; set; }
        [Inject] public IMapper Mapper { get; set; }

        protected List<MainCategory> userCategories;

        protected override async Task OnInitializedAsync()
        {
            userCategories = await CategoryService.GetUserCategoriesAsync();

            await base.OnInitializedAsync();
            IsPageLoaded = true;
        }

        protected async Task ShowCreateModal()
        {
            SetModalParameters(new CategoryModel(), async (c) =>
            {
                await CategoryService.CreateCategoryAsync(c);
            });

            await ShowModalWindow<CreateCategoryModal>(Resource.CreateCategoryTitle);
            StateHasChanged();
        }

        protected async Task ShowCreateSubcategoryModal(CategoryModel parentCategory)
        {
            SetModalParameters(new CategoryModel(), async (c) =>
            {
                await CategoryService.CreateSubcategoryAsync(parentCategory.CategoryId, c);
            });
            Parameters.Add(nameof(CreateSubcategoryModal.ParentCategory), parentCategory);

            await ShowModalWindow<CreateSubcategoryModal>(Resource.CreateSubcategoryTitle);
        }

        protected async Task ShowEditModal(CategoryModel category)
        {
            var categoryCopy = new CategoryModel();
            Mapper.Map(category, categoryCopy);

            SetModalParameters(categoryCopy, async (c) =>
            {
                await CategoryService.UpdateCategoryAsync(c);
            });

            var modalTitle = string.Format(Resource.EditTitle, category.Name);
            await ShowModalWindow<EditCategoryModal>(modalTitle);
        }

        protected async Task ShowDeleteModal(CategoryModel category)
        {

            SetModalParameters(category, async (c) =>
            {
                await CategoryService.DeleteCategoryAsync(c.CategoryId);
            });

            var modalTitle = string.Format(Resource.DeleteTitle, category.Name);
            await ShowModalWindow<DeleteCategoryModal>(modalTitle);
        }

        private async Task ShowModalWindow<TModalComponent>(string modalTitle)
            where TModalComponent : ModalComponent<CategoryModel>
        {
            await ShowModalWindowAsync<TModalComponent, CategoryModel>(modalTitle, async () =>
            {
                userCategories = await CategoryService.GetUserCategoriesAsync();
            });
        }
    }
}
