using MoneyManagerUi.Infrastructure.Constants;
using Microsoft.AspNetCore.Components;

namespace MoneyManagerUi.Shared
{
    public partial class SpinnerComponent
    {
        [Parameter]
        public bool IsSpinnerLoading { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public bool IsInModal { get; set; }

        [Parameter]
        public string Height { get; set; }

        private string SetHeight()
        {
            if (!string.IsNullOrEmpty(Height))
            {
                return Height;
            }

            return IsInModal ? CssConstants.Initial : string.Empty;
        }
    }
}
