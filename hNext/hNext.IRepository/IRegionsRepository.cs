using hNext.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace hNext.IRepository
{
    public interface IRegionsRepository : IRepository<Region>
    {
        Task<IEnumerable<District>> GetDistricts(int id);
        Task<IEnumerable<City>> GetCities(int id);
    }
}
