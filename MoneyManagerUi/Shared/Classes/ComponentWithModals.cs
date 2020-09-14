using Blazored.Modal;
using Blazored.Modal.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace MoneyManagerUi.Shared.Classes
{
    public class ComponentWithModals : ComponentBase
    {
        [CascadingParameter]
        protected IModalService Modal { get; set; }
        protected ModalOptions Options { get; set; } = new ModalOptions
        {
            HideCloseButton = true,
            DisableBackgroundCancel = true
        };

        protected ModalParameters Parameters { get; set; }
        protected bool IsPageLoaded { get; set; }

        protected void SetModalParameters<TModel>(TModel model, Func<TModel, Task> modelFunction) 
        {
            Parameters = new ModalParameters();
            Parameters.Add(nameof(ModalComponent<TModel>.Model), model);
            Parameters.Add(nameof(ModalComponent<TModel>.ModelFunctionAsync), modelFunction);
        }

        protected async Task ShowModalWindowAsync<TModalComponent, TModel>(string modalTitle, Func<Task> updatePageContent) 
            where TModalComponent : ModalComponent<TModel>
        {
            var modal = Modal.Show<TModalComponent>(modalTitle, Parameters, Options);
            var result = await modal.Result;

            if (!result.Cancelled)
            {
                IsPageLoaded = false;
                await updatePageContent.Invoke();
                IsPageLoaded = true;
            }
        }
    }
}
