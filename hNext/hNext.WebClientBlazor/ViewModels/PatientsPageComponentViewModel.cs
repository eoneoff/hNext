using hNext.Model;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hNext.WebClientBlazor.ViewModels
{
    public class PatientsPageComponentViewModel:ComponentBase
    {
        [Inject]
        protected AppStateViewModel State { get; set; }

        protected PatientSearchModel SearchModel { get; set; } = new PatientSearchModel();
        protected IEnumerable<Region> Regions { get; set; } = new List<Region>();
        protected IEnumerable<District> Districts { get; set; } = new List<District>();
        protected IEnumerable<City> Cities { get; set; } = new List<City>();
        protected string CityName { get; set; }
        protected IEnumerable<City> FilteredCities => Cities.Where(c => c.Name.StartsWith(CityName));
        protected IEnumerable<Patient> FoundPatients { get; set; } = new List<Patient>();
        protected Patient SelectedPatient
        {
            get => State.State[nameof(Patient)] as Patient;
            set => State.State[nameof(Patient)] = value;
        }

        protected override void OnInitialized()
        {
            if (!State.State.ContainsKey(nameof(Patient))) State.State[nameof(Patient)] = new Patient();
        }

        protected async void SearchPatients()
        {

        }

        protected async void CreateNewPatient()
        {

        }
    }
}
