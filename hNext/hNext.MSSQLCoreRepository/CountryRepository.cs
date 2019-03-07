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
    public class CountryRepository : Repository<Country>, ICountryRepository
    {
        public CountryRepository(hNextDbContext db) : base(db) { }

        public async Task<IEnumerable<City>> GetCities(int id)
        {
            return await db.Cities.Where(c => c.CountryId == id).AsNoTracking()
                .Include(c => c.CityType)
                .ToListAsync();
        }

        public async Task<IEnumerable<Region>> GetRegions(int id)
        {
            return await db.Regions.Where(r => r.CountryId == id).AsNoTracking().ToListAsync();
        }
    }
}
