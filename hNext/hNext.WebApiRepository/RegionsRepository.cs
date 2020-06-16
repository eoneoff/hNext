using hNext.IRepository;
using hNext.Model;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace hNext.WebApiRepository
{
    public class RegionsRepository : Repository<Region>, IRegionsRepository
    {
        public RegionsRepository(HttpClient client) : base(client)
        {
        }

        public async Task<IEnumerable<City>> GetCities(int id)
        {
            return await ReadResponse<IEnumerable<City>>(await _httpClient.GetAsync($"regions/{id}/cities"));
        }

        public async Task<IEnumerable<District>> GetDistricts(int id)
        {
            return await ReadResponse<IEnumerable<District>>(await _httpClient.GetAsync($"regions/{id}/districts"));
        }
    }
}
