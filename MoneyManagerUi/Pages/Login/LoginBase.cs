using System;
using System.Threading.Tasks;
using MoneyManagerUi.Data;
using MoneyManagerUi.Services.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;

namespace MoneyManagerUi.Pages.Login
{
    public class LoginBase : ComponentBase
    {
        [Inject] protected AuthenticationStateProvider AuthenticationStateProvider { get; set; }
        [Inject] protected NavigationManager NavigationManager { get; set; }
        [Inject] protected IUserService UserService { get; set; }

        [Parameter]
        public bool IsSpinnerLoading { get; set; }
        protected Errors ValidationErrors;
        protected EditContext EditContext;
        protected ValidationMessageStore MessageStore;

        protected void ClearErrors()
        {
            ValidationErrors.ModelErrors.Clear();
        }

        protected async Task ValidateUser(Func<Task> actionAfterValidation)
        {
            if (EditContext.Validate())
            {
                await actionAfterValidation.Invoke();
            }
            else
            {
                ValidationErrors = new Errors(EditContext.GetValidationMessages());
            }

            MessageStore.Clear();
        }
    }
}