using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hNext.WebClientBlazor.ViewModels
{
    public class ContextWatcher:ComponentBase
    {
        [CascadingParameter]
        protected EditContext EditContext { get; set; }

        public bool Valid => EditContext?.Validate() ?? false;
    }
}
