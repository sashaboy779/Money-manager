using MoneyManagerUi.Infrastructure;
using MoneyManagerUi.Infrastructure.Constants;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Threading.Tasks;

namespace MoneyManagerUi.Shared
{
    public class NavMenuComponent : ComponentBase
    {
        [Inject] public NavigationManager NavigationManager { get; set; }
        [Inject] public AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        private bool collapseNavMenu = true;
        protected string NavMenuCssClass => collapseNavMenu ? CssConstants.Collapse : null;

        protected void ToggleNavMenu()
        {
            collapseNavMenu = !collapseNavMenu;
        }

        protected string TryAddActive(string relativePath)
        {
            if ($"/{NavigationManager.ToBaseRelativePath(NavigationManager.Uri)}" == relativePath)
            {
                return CssConstants.Active;
            }

            return string.Empty;
        }

        protected async Task Logout()
        {
            await ((CustomAuthenticationStateProvider)AuthenticationStateProvider).MarkUserAsLoggedOut();
            NavigationManager.NavigateTo(Routes.Login);
        }
    }
}
