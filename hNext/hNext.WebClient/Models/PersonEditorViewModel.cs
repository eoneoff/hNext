using hNext.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hNext.WebClient.Models
{
    public class PersonEditorViewModel
    {
        public Person Person { get; set; }

        public IEnumerable<Gender> Genders { get; set; } = new List<Gender>();
        public IEnumerable<Country> Countries { get; set; } = new List<Country>();
        public IEnumerable<Region> Regions { get; set; } = new List<Region>();
        public IEnumerable<District> Districts { get; set; } = new List<District>();
        public IEnumerable<City> PlacesOfBirth { get; set; } = new List<City>();
        public IEnumerable<CityType> CityTypes { get; set; } = new List<CityType>();
        public IEnumerable<City> Cities { get; set; } = new List<City>();
        public IEnumerable<StreetType> StreetTypes { get; set; } = new List<StreetType>();
        public IEnumerable<Street> Streets { get; set; } = new List<Street>();
    }
}
