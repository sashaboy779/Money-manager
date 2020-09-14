using MoneyManagerUi.Infrastructure.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using System;

namespace MoneyManagerUi.Pages
{
    [Authorize]
    public class SettingsComponent : ComponentBase
    {
        [Inject] public NavigationManager NavigationManager { get; set; }

        protected void OnSelected(ChangeEventArgs e)
        {
            var culture = (string)e.Value;
            var uri = new Uri(NavigationManager.Uri)
                .GetComponents(UriComponents.PathAndQuery, UriFormat.Unescaped);
            var query = $"?{Configuration.Culture}{Uri.EscapeDataString(culture)}&" +
                $"{Configuration.Redirect}{Uri.EscapeDataString(uri)}";

            NavigationManager.NavigateTo(Routes.SetCulture + query, forceLoad: true);
        }
    }
}
