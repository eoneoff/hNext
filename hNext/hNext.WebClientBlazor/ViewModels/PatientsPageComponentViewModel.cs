using hNext.IRepository;
using hNext.Model;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace hNext.WebClientBlazor.ViewModels
{
    public class PatientsPageComponentViewModel:ComponentBase
    {
        [Inject]
        public AppStateViewModel State { get; set; }

        [Inject]
        public IRepository<Patient> Repository { get; set; }

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
        protected bool loading = false;
        protected bool showError = false;
        protected string errorText = "Error";

        protected override void OnInitialized()
        {
            if (!State.State.ContainsKey(nameof(Patient))) State.State[nameof(Patient)] = new Patient();
        }

        protected async Task SearchPatients()
        {
            loading = true;
            try
            {
                FoundPatients = await (Repository as IPatientsRepository).SearchPatients(SearchModel);         
            }
            catch(Exception ex)
            {
                errorText = ex.Message;
                showError = true;
            }
            finally
            {
                loading = false;
            }
        }

        protected async void CreateNewPatient()
        {

        }
    }
}
