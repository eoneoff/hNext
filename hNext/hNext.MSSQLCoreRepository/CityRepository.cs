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
    public class CityRepository : Repository<City>, ICityRepository
    {
        public CityRepository(hNextDbContext db) : base(db) { }

        public override async Task<IEnumerable<City>> Get() =>
            await dbSet.Include(c => c.CityType).OrderBy(c => c.Name).AsNoTracking().ToListAsync();

        public override async Task<City> Get(long id) =>
            await dbSet.Include(c => c.CityType).SingleOrDefaultAsync(c => c.Id == id);

        public async Task<IEnumerable<Street>> GetStreets(int id)
        {
            return await db.Streets.Where(s => s.CityId == id)
                .Include(s => s.StreetType)
                .OrderBy(s => s.Name)
                .AsNoTracking().ToListAsync();
        }
    }
}
