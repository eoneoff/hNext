using hNext.Model;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hNext.WebClientBlazor.ViewModels
{
    public class PatientDetailsTabComponentViewModel : ComponentBase
    {
        [Inject]
        protected AppStateViewModel State { get; set; }

        protected Patient Patient
        {
            get => State.State["Patient"] as Patient;
            set => State.State["Patient"] = value;
        }

        protected override void OnInitialized()
        {
            if (!State.State.ContainsKey(nameof(Patient))) State.State[nameof(Patient)] = new Patient();
        }
    }
}
