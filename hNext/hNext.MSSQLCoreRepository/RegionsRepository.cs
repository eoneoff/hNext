using hNext.DbAccessMSSQLCore;
using hNext.IRepository;
using hNext.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hNext.MSSQLCoreRepository
{
    public class RegionsRepository : Repository<Region>, IRegionsRepository
    {
        public RegionsRepository(hNextDbContext db) : base(db) { }

        public async Task<IEnumerable<District>> GetDistricts(int id)
        {
            return await db.Districts.Where(d => d.RegionId == id).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<City>> GetCities(int id)
        {
            return await db.Cities.Where(c => c.RegionId == id)
                .Include(c => c.CityType)
                .AsNoTracking().ToListAsync();
        }
    }
}
