using hNext.IRepository;
using hNext.Model;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace hNext.WebApiRepository
{
    public class DistrictsRepository : Repository<District>, IDistrictsRepository
    {
        public DistrictsRepository(HttpClient client) : base(client)
        {
        }

        public async Task<IEnumerable<City>> GetCities(int id) => await ReadResponse<IEnumerable<City>>(await _httpClient.GetAsync($"districts/{id}/cities"));
    }
}
