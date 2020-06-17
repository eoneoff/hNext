using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hNext.WebClientBlazor.ViewModels
{
    public class ModalDialogViewModel:ComponentBase
    {
        protected bool isOpen = false;

        [Parameter]
        public RenderFragment Header { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public string OkButtonText { get; set; } = "OK";

        [Parameter]
        public string CancelButtonText { get; set; }

        [Parameter]
        public EventCallback OnConfirm { get; set; }

        [Parameter]
        public int Level { get; set; } = 1;

        [Parameter]
        public string Class { get; set; }

        [Parameter]
        public string Style { get; set; }

        [Parameter]
        public bool CloseOnOk { get; set; } = true;

        [Parameter]
        public bool ShowOkButton { get; set; } = true;

        protected int DialogZIndex => Level * 1000;
        protected int BackdropZIndex => Level * 1000 - 500;

        public void Show()
        {
            isOpen = true;
            StateHasChanged();
        }

        public void Hide()
        {
            isOpen = false;
            StateHasChanged();
        }

        protected void OkClicked()
        {
            if (CloseOnOk) isOpen = false;
            OnConfirm.InvokeAsync(null);
        }
    }
}
