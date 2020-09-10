using AutoMapper;
using MoneyManagerUi.Data;
using MoneyManagerUi.Data.User;
using MoneyManagerUi.Infrastructure;
using MoneyManagerUi.Infrastructure.Constants;
using MoneyManagerUi.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Threading.Tasks;

namespace MoneyManagerUi.Pages.Login
{
    public partial class SignUp
    {
        [Inject] public IMapper Mapper { get; set; }

        private RegisterUserModel user;

        protected override Task OnInitializedAsync()
        {
            user = new RegisterUserModel();
            ValidationErrors = new Errors();
            EditContext = new EditContext(user);
            MessageStore = new ValidationMessageStore(EditContext);

            return base.OnInitializedAsync();
        }

        private async Task ValidateUser()
        {
            await base.ValidateUser(async () => await RegisterUser());
        }

        private async Task RegisterUser()
        {
            IsSpinnerLoading = true;

            try
            {
                await UserService.RegisterUserAsync(user);

                var authenticateModel = Mapper.Map<AuthenticateModel>(user);
                var token = await UserService.LoginAsync(authenticateModel);

                await ((CustomAuthenticationStateProvider)AuthenticationStateProvider).MarkUserAsAuthenticated(token);
                NavigationManager.NavigateTo(Routes.Home);
            }
            catch (ModelErrorException e)
            {
                ValidationErrors = new Errors(e.Errors.ModelErrors);
            }

            IsSpinnerLoading = false;
        }
    }
}