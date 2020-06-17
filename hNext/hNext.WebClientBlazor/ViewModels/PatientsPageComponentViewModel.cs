using hNext.IRepository;
using hNext.Model;
using hNext.WebClientBlazor.Shared;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace hNext.WebClientBlazor.ViewModels
{
    public class PatientsPageComponentViewModel : ComponentBase
    {
        [Inject]
        public AppStateViewModel State { get; set; }

        [Inject]
        public IRepository<Patient> Repository { get; set; }

        [Inject]
        public IRepository<Region> RegionsRepository { get; set; }

        protected PatientSearchModel SearchModel { get; set; } = new PatientSearchModel();

        private CancellationTokenSource cancelDistrictsLoading;
        private CancellationTokenSource cancelCitiesLoading;
        protected int? RegionId
        {
            get => SearchModel.RegionId;
            set
            {
                SearchModel.RegionId = value;
                if (value is int id)
                {
                    Districts = new List<District>();
                    Cities = new List<City>();
                    CityName = string.Empty;
                    cancelDistrictsLoading?.Cancel();
                    cancelCitiesLoading?.Cancel();
                    cancelDistrictsLoading = new CancellationTokenSource();
                    cancelCitiesLoading = new CancellationTokenSource();
                    Task.Run(async () => Districts = await (RegionsRepository as IRegionsRepository).GetDistricts(id), cancelDistrictsLoading.Token).ContinueWith(t => StateHasChanged(), cancelDistrictsLoading.Token);
                    Task.Run(async () => Cities = await (RegionsRepository as IRegionsRepository).GetCities(id), cancelCitiesLoading.Token).ContinueWith(t => StateHasChanged(), cancelCitiesLoading.Token);
                }
            }
        }
        protected IEnumerable<Region> Regions { get; set; } = new List<Region>();
        protected IEnumerable<District> Districts { get; set; } = new List<District>();
        protected IEnumerable<City> Cities { get; set; } = new List<City>();
        protected string CityName { get; set; } = string.Empty;
        protected IEnumerable<City> FilteredCities => Cities.Where(c => c.Name.ToLower().StartsWith(CityName.ToLower()));
        protected IEnumerable<Patient> FoundPatients { get; set; } = new List<Patient>();
        protected Patient SelectedPatient
        {
            get => State.State[nameof(Patient)] as Patient;
            set => State.State[nameof(Patient)] = value;
        }
        protected bool loading = false;
        protected bool showError = false;
        protected string errorText = "Error";

        protected ModalDialogComponent PatientEditor;

        protected override async Task OnInitializedAsync()
        {
            if (!State.State.ContainsKey(nameof(Patient))) State.State[nameof(Patient)] = new Patient();
            Regions = await RegionsRepository.Get();
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
                SelectedPatient = new Patient();
            }
        }

        protected async void CreateNewPatient()
        {

        }
    }
}
