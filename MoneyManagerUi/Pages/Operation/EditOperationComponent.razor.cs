using MoneyManagerUi.Data.Category;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UpdateOperationModel = MoneyManagerUi.Data.Opration.UpdateOperation;

namespace MoneyManagerUi.Pages.Operation
{
    public class EditOperationComponent : BaseOperationComponent<UpdateOperationModel>
    {
        protected List<MainCategory> userCategories;

        protected async override Task OnInitializedAsync()
        {
            userCategories = await CategoryService.GetUserCategoriesAsync();
            userCategories = userCategories.OrderBy(x => x.Name).ToList();

            await base.OnInitializedAsync();
            isPageLoaded = true;
        }
    }
}
