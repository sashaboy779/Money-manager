using MoneyManagerUi.Services.Interfaces;
using MoneyManagerUi.Shared.Classes;
using Microsoft.AspNetCore.Components;

namespace MoneyManagerUi.Pages.Operation
{
    public class BaseOperationComponent<TModel> : ModalComponent<TModel>
    {
        [Inject] protected ICategoryService CategoryService { get; set; }
        protected bool isPageLoaded;
    }
}
