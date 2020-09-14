using System.Security.Claims;
using System.Threading.Tasks;
using MoneyManagerUi.Data;
using MoneyManagerUi.Data.User;
using MoneyManagerUi.Infrastructure;
using MoneyManagerUi.Infrastructure.Constants;
using MoneyManagerUi.Infrastructure.Exceptions;
using MoneyManagerUi.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;

namespace MoneyManagerUi.Pages.Login
{
    public partial class Login
    {
        private AuthenticateModel user;
        private ClaimsPrincipal claimsPrincipal;

        [CascadingParameter]
        private Task<AuthenticationState> AuthenticationStateTask { get; set; }

        protected override async Task OnInitializedAsync()
        {
            user = new AuthenticateModel();

            claimsPrincipal = (await AuthenticationStateTask).User;

            if (claimsPrincipal.Identity.IsAuthenticated)
            {
                NavigationManager.NavigateTo(Routes.Home);
            }

            ValidationErrors = new Errors();
            EditContext = new EditContext(user);
            MessageStore = new ValidationMessageStore(EditContext);
        }

        private async Task ValidateUser()
        {
            await base.ValidateUser(async () => await LoginUser());
        }

        private async Task LoginUser()
        {
            IsSpinnerLoading = true;
            try
            {
                var accessToken = await UserService.LoginAsync(user);
                await ((CustomAuthenticationStateProvider)AuthenticationStateProvider).MarkUserAsAuthenticated(
                   accessToken);
                NavigationManager.NavigateTo(Routes.Home);
            }
            catch (ModelErrorException)
            {
                ValidationErrors = new Errors(Resource.LoginError);
            }

            IsSpinnerLoading = false;
        }
    }
}