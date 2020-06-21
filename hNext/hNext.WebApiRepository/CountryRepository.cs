using hNext.IRepository;
using hNext.Model;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace hNext.WebApiRepository
{
    public class CountryRepository : Repository<Country>, ICountryRepository
    {
        public CountryRepository(HttpClient client) : base(client)
        {
        }

        public async Task<IEnumerable<City>> GetCities(int id) => await ReadResponse<IEnumerable<City>>(await _httpClient.GetAsync($"countries/{id}/cities"));

        public async Task<IEnumerable<City>> GetCitiesByName(int id, string name) => await ReadResponse<IEnumerable<City>>(await _httpClient.GetAsync($"countries/{id}/byname/{name}"));

        public async Task<IEnumerable<Region>> GetRegions(int id) => await ReadResponse<IEnumerable<Region>>(await _httpClient.GetAsync($"countries/{id}/regions"));
    }
}
