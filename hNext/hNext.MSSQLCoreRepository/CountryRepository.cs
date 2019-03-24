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

        public override async Task<IEnumerable<Country>> Get()
        {
            return await dbSet.OrderBy(c => c.Name).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<City>> GetCities(int id)
        {
            return await db.Cities.Where(c => c.CountryId == id).AsNoTracking()
                .Include(c => c.CityType)
                .OrderBy(c => c.Name)
                .ToListAsync();
        }

        public async Task<IEnumerable<City>> GetCitiesByName(int id, string start) =>
            await db.Cities.Where(c => c.CountryId == id
            && c.Name.ToLower().StartsWith(start.ToLower())).OrderBy(c => c.Name)
                .AsNoTracking().ToListAsync();

        public async Task<IEnumerable<Region>> GetRegions(int id)
        {
            return await db.Regions.Where(r => r.CountryId == id).OrderBy(r => r.Name).AsNoTracking().ToListAsync();
        }
    }
}
