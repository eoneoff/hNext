using hNext.Model;
using MoreLinq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace hNext.DataBaseDataFiller
{
     public class ItemCreator
    {
        Dictionary<string, int> streetTypes = new Dictionary<string, int>
        {
            ["вул."] = 1,
            ["вулиця"] = 1,
            ["пл."] = 6,
            ["пров."] = 2,
            ["просп."] = 3,
            ["проїзд"] = 7,
            ["хутір"] = 8,
            ["тупік"] = 9,
            ["узвіз"] = 10,
            ["бульв."] = 4,
            ["парк"] = 11,
            ["селище"] = 12,
            ["шосе"] = 13,
            ["жилий масив"] = 14,
            ["м-р"] = 15,
            ["майдан"] = 16,
            ["спуск"] = 17,
            ["в'їзд"] = 18,
            ["острів"] = 19,
            ["містечко"] = 20,
            ["кв-л"] = 5,
            ["лінія"] = 21,
            ["шлях"] = 22,
            ["набережна"] = 23,
            ["алея"] = 24,
            ["урочище"] = 25,
            ["дорога"] = 26,
            ["завулок"] = 27
        };
        public IEnumerable<Country> Countries { get; set; }
        public IEnumerable<Region> Regions { get; set; }
        public IEnumerable<District> Districts { get; set; }
        public IEnumerable<City> Cities { get; set; }
        public IEnumerable<Street> Streets { get; set; }
        public int CountryId { get; set; }

        public void CreateCities(IEnumerable<CsvModel> records)
        {
            Cities = records.GroupBy(r => r.Region).SelectMany(rg => rg.GroupBy(g => g.District).SelectMany(dg => dg.DistinctBy(d => d.City)
            .Select(s => new City
            {
                Name = s.City,
                CountryId = CountryId,
                RegionId = Regions.First(reg => reg.Name == s.Region).Id,
                DistrictId = Districts.FirstOrDefault(dist => dist.Name == s.District && dist.Region.Name == s.Region)?.Id
            }))).ToList();

            Cities.ForEach(c =>
            {
                (c.CityTypeId, c.Name) = GetCityTypeAndName(c.Name);
                Console.Write($"\r{c.Name}{new string(' ', 20)}");
            });
        }

        public void CreateStreets(IEnumerable<CsvModel> records)
        {
            var streetsWidData = records.GroupBy(r => r.Region)
                        .SelectMany(rg => rg.GroupBy(rgr => rgr.District))
                        .SelectMany(dg => dg.GroupBy(dgr => dgr.City))
                        .SelectMany(cg => cg.DistinctBy(c => c.Street))
                        .Select(s => new
                        {
                            Street = new Street
                            {
                                Name = s.Street
                            },
                            Data = s
                        }).ToList();
            foreach(var s in streetsWidData)
            {
                var cities = Cities.Where(c => c.Name == GetCityTypeAndName(s.Data.City).Item2).ToList();
                if (cities.Count() == 1)
                    s.Street.CityId = cities.First().Id;
                else
                {
                    cities = cities.Where(c => c.Region.Name == s.Data.Region).ToList();
                    if (cities.Count() == 1)
                        s.Street.CityId = cities.First().Id;
                    else
                    {
                        cities = cities.Where(c => (c.District?.Name ?? "") == s.Data.District).ToList();
                        if (cities.Count() == 1)
                            s.Street.CityId = cities.First().Id;
                        else
                        {
                            cities = cities.Where(c => c.CityTypeId == GetCityTypeAndName(s.Data.City).Item1).ToList();
                            if (cities.Count() == 1)
                                s.Street.CityId = cities.First().Id;
                            else
                                s.Street.CityId = cities.First().Id;
                        }
                    }
                }
                Console.Write($"\r{s.Data.Region} {s.Data.District} {s.Data.City} {s.Data.Street}{new string(' ', 20)}");
            }

            Streets = streetsWidData.Select(sd =>
            {
                foreach (var i in streetTypes)
                {
                    if (sd.Street.Name.StartsWith(i.Key))
                    {
                        sd.Street.Name = sd.Street.Name.Remove(0, i.Key.Length + 1);
                        sd.Street.StreetTypeId = i.Value;
                        break;
                    }
                    if (sd.Street.StreetTypeId == 0)
                    {
                        sd.Street.StreetTypeId = 1;
                        Console.WriteLine(sd.Street.Name);
                    }
                }

                return sd.Street;
            }).ToList();
        }

        private (int, string) GetCityTypeAndName(string nameWithType)
        {
            int typeId = 0;
            string name = nameWithType;
            switch(nameWithType.Split(' ')[0])
            {
                case "м.":
                    name = nameWithType.Remove(0, 3);
                    typeId = 1;
                    break;
                case "с.":
                    name = nameWithType.Remove(0, 3);
                    typeId = 2;
                    break;
                case "смт":
                    name = nameWithType.Remove(0, 4);
                    typeId = 3;
                    break;
                case "с-ще":
                    name = nameWithType.Remove(0, 5);
                    typeId = 4;
                    break;
            }

            return (typeId, name);
        }
    }
}
