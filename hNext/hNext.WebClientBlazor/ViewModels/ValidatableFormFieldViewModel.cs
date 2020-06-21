using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hNext.WebClientBlazor.ViewModels
{
    public class ValidatableFormFieldViewModel:ComponentBase
    {
        [Parameter]
        public bool Wide { get; set; } = true;

        [Parameter]
        public string Label { get; set; }

        [Parameter]
        public string Id { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public string Class { get; set; }
    }
}
