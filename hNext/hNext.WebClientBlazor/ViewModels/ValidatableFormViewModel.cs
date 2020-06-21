using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hNext.WebClientBlazor.ViewModels
{
    public class ValidatableFormViewModel<TItem>:ComponentBase
    {
        protected ContextWatcher contextWatcher;

        [Parameter]
        public TItem Model { get; set; }

        [Parameter]
        public RenderFragment<TItem> ChildContent { get; set; }

        [Parameter]
        public bool HideButtons { get; set; } = false;

        [Parameter]
        public EventCallback OnSubmit { get; set; }

        [Parameter]
        public EventCallback OnCancel { get; set; }

        [Parameter]
        public bool Disabled { get; set; } = false;

        [Parameter]
        public bool Unchanged { get; set; } = true;

        [Parameter]
        public string OkButtonText { get; set; }

        [Parameter]
        public string CancelButtonText { get; set; }

        public bool Validate() => contextWatcher.Validate();
    }
}
