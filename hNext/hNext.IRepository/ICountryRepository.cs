using hNext.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace hNext.IRepository
{
    public interface ICountryRepository:IRepository<Country>
    {
        Task<IEnumerable<Region>> GetRegions(int id);
        Task<IEnumerable<City>> GetCities(int id);
        Task<IEnumerable<City>> GetCitiesByName(int id, string name);
    }
}
