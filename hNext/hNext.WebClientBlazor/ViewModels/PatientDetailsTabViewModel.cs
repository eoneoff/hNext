using hNext.Model;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hNext.WebClientBlazor.ViewModels
{
    public class PatientDetailsTabViewModel : ComponentBase
    {
        [Inject]
        protected AppStateViewModel State { get; set; }

        protected Patient Patient => State.GetData<Patient>();

        protected override void OnInitialized()
        {
            State.Subscribe<Patient>(StateHasChanged);
        }
    }
}
