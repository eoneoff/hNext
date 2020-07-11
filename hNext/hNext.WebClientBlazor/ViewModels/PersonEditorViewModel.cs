using hNext.IRepository;
using hNext.Model;
using hNext.WebClientBlazor.Shared;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace hNext.WebClientBlazor.ViewModels
{
    public class PersonEditorViewModel : ComponentBase
    {
        [Inject]
        public IRepository<Gender> _gendersRepository { get; set; }

        [Inject]
        public IRepository<Country> _countriesRepository { get; set; }

        [Inject]
        public IRepository<Region> _regionsRepository {get; set;}

        [Inject]
        public IRepository<District> _districtsRepository { get; set; }

        [Inject]
        public IRepository<City> _citiesRepository { get; set; }

        [Parameter]
        public Person Person { get; set; } = new Person { Address = new Address()};

        [Parameter]
        public bool Wide { get; set; } = false;
        [Parameter]
        public bool HideButtons { get; set; } = true;

        protected IEnumerable<Gender> Genders { get; set; } = new List<Gender>();
        protected IEnumerable<Country> Countries { get; set; } = new List<Country>();
        protected IEnumerable<City> PlacesOfBirth { get; set; } = new List<City>();
        protected IEnumerable<City> FilteredPlacesOfBirth => PlacesOfBirth.Where(c => c.Name.ToLower().StartsWith(PlaceOfBirthName.ToLower()));
        protected string PlaceOfBirthName { get; set; } = string.Empty;
        protected IEnumerable<Region> Regions { get; set; } = new List<Region>();
        protected IEnumerable<District> Districts { get; set; } = new List<District>();
        protected IEnumerable<City> Cities { get; set; } = new List<City>();
        protected string CityName { get; set; } = string.Empty;
        protected IEnumerable<City> FilteredCities => Cities.Where(c => c.Name.ToLower().StartsWith(CityName.ToLower()));
        protected IEnumerable<Street> Streets { get; set; } = new List<Street>();
        protected string StreetName { get; set; } = string.Empty;
        protected IEnumerable<Street> FilteredStreets => Streets.Where(s => s.Name.ToLower().StartsWith(StreetName.ToLower()));


        private CancellationTokenSource cancelPlaceOfBirthLoading;
        private CancellationTokenSource cancelRegionsLoading;
        private CancellationTokenSource cancelDistrictsLoading;
        private CancellationTokenSource cancelCitiesLoading;
        private CancellationTokenSource cancelStreetsLoading;
        protected int? CountryOfBirthId
        {
            get => Person?.CountryOfBirthId ?? 0;
            set
            {
                Person.CountryOfBirthId = value;
                if(value is int id)
                {
                    PlacesOfBirth = new List<City>();
                    PlaceOfBirthName = string.Empty;
                    PlaceOfBirthId = null;
                    cancelPlaceOfBirthLoading?.Cancel();
                    cancelPlaceOfBirthLoading = new CancellationTokenSource();
                    Task.Run(async () => PlacesOfBirth = await (_countriesRepository as ICountryRepository).GetCities(id), cancelPlaceOfBirthLoading.Token).ContinueWith(t => StateHasChanged(), cancelPlaceOfBirthLoading.Token);
                }
            }
        }
        protected int? PlaceOfBirthId
        {
            get => Person?.PlaceOfBirthId;
            set
            {
                if (value == -1)
                {
                    Person.PlaceOfBirthId = null;
                    PlaceOfBirthName = string.Empty;
                }
                else Person.PlaceOfBirthId = value;
            }
        }
        protected int CountryId
        {
            get => Person?.Address?.CountryId??0;
            set
            {
                Person.Address.CountryId = value;
                Regions = new List<Region>();
                Cities = new List<City>();
                Districts = new List<District>();
                Cities = new List<City>();
                Streets = new List<Street>();
                CityName = string.Empty;
                StreetName = string.Empty;
                RegionId = null;
                DistrictId = null;
                CityId = 0;
                StreetId = null;
                cancelRegionsLoading?.Cancel();
                cancelDistrictsLoading?.Cancel();
                cancelCitiesLoading?.Cancel();
                cancelStreetsLoading?.Cancel();
                cancelRegionsLoading = new CancellationTokenSource();
                cancelCitiesLoading = new CancellationTokenSource();
                Task.Run(async () => Regions = await (_countriesRepository as ICountryRepository).GetRegions(value), cancelRegionsLoading.Token).ContinueWith(t => StateHasChanged(), cancelRegionsLoading.Token);
                Task.Run(async () => Cities = await (_countriesRepository as ICountryRepository).GetCities(value), cancelCitiesLoading.Token).ContinueWith(t => StateHasChanged(), cancelCitiesLoading.Token);
            }
        }
        protected int? RegionId
        {
            get => Person?.Address?.RegionId;
            set
            {
                Person.Address.RegionId = value;
                if(value is int id)
                {
                    Districts = new List<District>();
                    Cities = new List<City>();
                    Streets = new List<Street>();
                    CityName = string.Empty;
                    StreetName = string.Empty;
                    DistrictId = null;
                    CityId = 0;
                    StreetId = null;
                    cancelDistrictsLoading?.Cancel();
                    cancelCitiesLoading?.Cancel();
                    cancelStreetsLoading?.Cancel();
                    cancelDistrictsLoading = new CancellationTokenSource();
                    cancelCitiesLoading = new CancellationTokenSource();
                    Task.Run(async () => Districts = await (_regionsRepository as IRegionsRepository).GetDistricts(id), cancelDistrictsLoading.Token).ContinueWith(t => StateHasChanged(), cancelDistrictsLoading.Token);
                    Task.Run(async () => Cities = await (_regionsRepository as IRegionsRepository).GetCities(id), cancelCitiesLoading.Token).ContinueWith(t => StateHasChanged(), cancelCitiesLoading.Token);
                }
            }
        }
        protected int? DistrictId
        {
            get => Person?.Address?.DistrictId;
            set
            {
                Person.Address.DistrictId = value;
                if(value is int id)
                {
                    Cities = new List<City>();
                    Streets = new List<Street>();
                    CityName = string.Empty;
                    StreetName = string.Empty;
                    CityId = 0;
                    StreetId = null;
                    cancelCitiesLoading?.Cancel();
                    cancelStreetsLoading?.Cancel();
                    cancelCitiesLoading = new CancellationTokenSource();
                    Task.Run(async () => Cities = await (_districtsRepository as IDistrictsRepository).GetCities(id), cancelCitiesLoading.Token).ContinueWith(t => StateHasChanged(), cancelCitiesLoading.Token);
                }
            }
        }
        protected int CityId
        {
            get => Person?.Address?.CityId ?? 0;
            set
            {
                Streets = new List<Street>();
                StreetName = string.Empty;
                StreetId = null;
                if(value == -1)
                {
                    Person.Address.CityId = 0;
                    CityName = string.Empty;
                }
                else
                {
                    Person.Address.CityId = value;
                    if(value > 0)
                    {
                        cancelStreetsLoading?.Cancel();
                        cancelStreetsLoading = new CancellationTokenSource();
                        Task.Run(async () => Streets = await (_citiesRepository as ICityRepository).GetStreets(value), cancelStreetsLoading.Token).ContinueWith(t => StateHasChanged(), cancelStreetsLoading.Token);
                    }
                }
            }
        }
        protected int? StreetId
        {
            get => Person?.Address?.StreetId;
            set
            {
                if(value == -1)
                {
                    Person.Address.StreetId = null;
                    StreetName = string.Empty;
                }
                else
                {
                    Person.Address.StreetId = value;
                }
            }
        }

        protected ValidatableForm<Person> Form;
        public bool Valid => Form.Valid;

        protected async override Task OnInitializedAsync()
        {
            Genders = await _gendersRepository.Get();
            Countries = await _countriesRepository.Get();
            if (CountryId == 0) CountryId = 71;
        }
    }
}
