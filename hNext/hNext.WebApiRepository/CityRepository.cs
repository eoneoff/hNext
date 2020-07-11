using hNext.IRepository;
using hNext.Model;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace hNext.WebApiRepository
{
    public class CityRepository : Repository<City>, ICityRepository
    {
        public CityRepository(HttpClient client) : base(client)
        {
        }

        public async Task<IEnumerable<Street>> GetStreets(int id) => await ReadResponse<IEnumerable<Street>>(await _httpClient.GetAsync($"cities/{id}/streets"));
    }
}
