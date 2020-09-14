using System;
using System.Threading.Tasks;
using MoneyManagerUi.Resources;
using Microsoft.AspNetCore.Components;

namespace MoneyManagerUi.Shared
{
    public partial class NoContent
    {
        [Inject] public NavigationManager NavigationManager { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public Func<Task> CreateContent { get; set; }

        [Parameter]
        public string CreateRoute { get; set; } = string.Empty;

        [Parameter]
        public string Text { get; set; } = Resource.Create;

        private async Task OnClick()
        {
            if (string.IsNullOrEmpty(CreateRoute))
            {
                await CreateContent.Invoke();
            }
            else
            {
                NavigationManager.NavigateTo(CreateRoute);
            }
        }
    }
}
