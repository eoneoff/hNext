﻿using hNext.IRepository;
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
    public class PatientsPageViewModel : ComponentBase
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
                    Districts = null;
                    CityId = null;
                    cancelDistrictsLoading?.Cancel();
                    cancelCitiesLoading?.Cancel();
                    cancelDistrictsLoading = new CancellationTokenSource();
                    cancelCitiesLoading = new CancellationTokenSource();
                    Task.Run(async () => Districts = await (RegionsRepository as IRegionsRepository).GetDistricts(id), cancelDistrictsLoading.Token).ContinueWith(t => StateHasChanged(), cancelDistrictsLoading.Token);
                    Task.Run(async () => Cities = await (RegionsRepository as IRegionsRepository).GetCities(id), cancelCitiesLoading.Token).ContinueWith(t => StateHasChanged(), cancelCitiesLoading.Token);
                }
            }
        }
        protected int? DistrictId
        {
            get => SearchModel.DistrictId;
            set
            {
                SearchModel.DistrictId = value;
                if(value is int id)
                {
                    Cities = new List<City>();
                    CityName = string.Empty;
                    CityId = null;
                    cancelCitiesLoading?.Cancel();
                    cancelCitiesLoading = new CancellationTokenSource();
                    Task.Run(async () => Cities = await (RegionsRepository as IRegionsRepository).GetCities(id), cancelCitiesLoading.Token).ContinueWith(t => StateHasChanged(), cancelCitiesLoading.Token);
                }
            }
        }
        protected int? CityId
        {
            get => SearchModel.CityId;
            set
            {
                if (value == -1)
                {
                    SearchModel.CityId = null;
                    CityName = string.Empty;
                }
                else SearchModel.CityId = value;
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
            get => State.GetData<Patient>();
            set => State.SetData(value);
        }

        protected Person PersonToEdit { get; set; }

        protected bool loading = false;
        protected bool showError = false;
        protected string errorText;

        protected ModalDialog PatientEditorDialog;
        protected PersonEditor PersonEditor;

        protected override async Task OnInitializedAsync()
        {
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

        protected void CreateNewPatientClicked()
        {
            PersonToEdit = new Person { Address = new Address() };
            PatientEditorDialog.Show();
        }

        protected void SavePersonClicked()
        {
            if(PersonEditor.Valid)
            {

            }
        }
    }
}
