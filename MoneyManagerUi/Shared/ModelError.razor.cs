using Microsoft.AspNetCore.Components;
using MoneyManagerUi.Data;

namespace MoneyManagerUi.Shared
{
    public partial class ModelError
    {
        [Parameter]
        public Errors Errors { get; set; }
    }
}
