using MoneyManagerUi.Infrastructure.Constants;
using Microsoft.AspNetCore.Components;

namespace MoneyManagerUi.Pages.Operation
{
    public class NoCategoriesComponent : ComponentBase
    {
        [Inject] protected NavigationManager NavigationManager { get; set; }

        protected void NavigateToCategories()
        {
            NavigationManager.NavigateTo(Routes.Categories);
        }
    }
}
