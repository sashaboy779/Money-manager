using MoneyManagerUi.Data.Category;
using MoneyManagerUi.Data.Opration;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneyManagerUi.Pages.Operation
{
    public class CreateOperationComponent : BaseOperationComponent<CreateOperation>
    {
        [Parameter]
        public int WalletId { get; set; }

        protected List<MainCategory> userCategories;

        protected async override Task OnInitializedAsync()
        {
            userCategories = await CategoryService.GetUserCategoriesAsync();
            userCategories = userCategories.OrderBy(x => x.Name).ToList();

            Model = new CreateOperation
            {
                WalletId = WalletId,
                CategoryId = userCategories.Count != 0 ? userCategories.First().CategoryId : 0
            };

            await base.OnInitializedAsync();
            isPageLoaded = true;
        }
    }
}
