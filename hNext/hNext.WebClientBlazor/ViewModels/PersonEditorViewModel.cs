using hNext.IRepository;
using hNext.Model;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
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

        [Parameter]
        public Person Person { get; set; } = new Person { Address = new Address()};

        [Parameter]
        public bool Wide { get; set; } = false;
        [Parameter]
        public bool ShowButtons { get; set; } = false;

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

        protected async override Task OnInitializedAsync()
        {
            Genders = await _gendersRepository.Get();
            Countries = await _countriesRepository.Get();
        }
    }
}
