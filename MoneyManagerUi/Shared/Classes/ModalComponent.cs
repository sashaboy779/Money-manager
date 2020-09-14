using Blazored.Modal;
using Blazored.Modal.Services;
using MoneyManagerUi.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Components;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MoneyManagerUi.Shared.Classes
{
    public class ModalComponent<TModel> : ComponentBase
    {
        [Parameter]
        public Func<TModel, Task> ModelFunctionAsync { get; set; }

        [Parameter]
        public TModel Model { get; set; }

        [CascadingParameter]
        protected BlazoredModalInstance ModalInstance { get; set; }
        protected string ErrorMessage = string.Empty;

        protected async Task InvokeModelFunctionAsync()
        {
            try
            {
                await ModelFunctionAsync(Model);
                await ModalInstance.Close(ModalResult.Ok(0));
            }
            catch (ModelErrorException e)
            {
                ErrorMessage = e.Errors.ModelErrors.FirstOrDefault();
            }
        }

        protected async Task Cancel()
        {
            await ModalInstance.Cancel();
        }

        protected void ClearError()
        {
            ErrorMessage = string.Empty;
            StateHasChanged();
        }
    }
}
