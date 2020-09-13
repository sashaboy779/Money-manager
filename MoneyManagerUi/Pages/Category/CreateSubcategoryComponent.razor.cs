using MoneyManagerUi.Shared.Classes;
using Microsoft.AspNetCore.Components;
using CategoryModel = MoneyManagerUi.Data.Category.Category;

namespace MoneyManagerUi.Pages.Category
{
    public class CreateSubcategoryComponent : ModalComponent<CategoryModel>
    {
        [Parameter]
        public CategoryModel ParentCategory { get; set; }
    }
}
