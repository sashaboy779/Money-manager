using MoneyManagerUi.Infrastructure.Constants;
using Microsoft.AspNetCore.Components;

namespace MoneyManagerUi.Pages.Login
{
    public partial class RedirectToLogin
    {
        [Inject] protected NavigationManager NavigationManager { get; set; }

        protected override void OnInitialized()
        {
            NavigationManager.NavigateTo(Routes.Login);
        }
    }
}