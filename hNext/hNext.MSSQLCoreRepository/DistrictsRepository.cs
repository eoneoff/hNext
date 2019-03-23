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
    public class DistrictsRepository : Repository<District>, IDistrictsRepository
    {
        public DistrictsRepository(hNextDbContext db) : base(db) { }

        public override async Task<IEnumerable<District>> Get()
        {
            return await dbSet.OrderBy(d => d.Name).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<City>> GetCities(int id)
        {
            return await db.Cities.Where(c => c.DistrictId == id)
                .Include(c => c.CityType)
                .OrderBy(c => c.Name)
                .AsNoTracking().ToListAsync();
        }
    }
}
