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
    public class GuardianRepository : Repository<GuardianWard>, IGuardianRepository
    {
        public GuardianRepository(hNextDbContext db) : base(db){}

        public async Task<GuardianWard> Exists(GuardianWard guardian) => await dbSet
            .Include(g => g.Guardian).ThenInclude(p => p.Address).ThenInclude(a => a.Region)
                .Include(g => g.Guardian).ThenInclude(p => p.Address).ThenInclude(a => a.District)
                .Include(g => g.Guardian).ThenInclude(p => p.Address).ThenInclude(a => a.City).ThenInclude(c => c.CityType)
                .Include(g => g.Guardian).ThenInclude(p => p.Address).ThenInclude(a => a.Street).ThenInclude(s => s.StreetType)
                .Include(g => g.Guardian).ThenInclude(p => p.CountryOfBirth)
                .Include(g => g.Guardian).ThenInclude(p => p.PlaceOfBirth)
                .Include(g => g.Guardian).ThenInclude(p => p.Gender)
                .AsNoTracking()
                .SingleOrDefaultAsync(g => g.GuardianId == guardian.GuardianId
                    && g.WardId == guardian.WardId && g.Relation == guardian.Relation);

        public override async Task<IEnumerable<GuardianWard>> Get() => await dbSet
                .Include(g => g.Guardian).ThenInclude(p => p.Address).ThenInclude(a => a.Region)
                .Include(g => g.Guardian).ThenInclude(p => p.Address).ThenInclude(a => a.District)
                .Include(g => g.Guardian).ThenInclude(p => p.Address).ThenInclude(a => a.City).ThenInclude(c => c.CityType)
                .Include(g => g.Guardian).ThenInclude(p => p.Address).ThenInclude(a => a.Street).ThenInclude(s => s.StreetType)
                .Include(g => g.Guardian).ThenInclude(p => p.CountryOfBirth)
                .Include(g => g.Guardian).ThenInclude(p => p.PlaceOfBirth)
                .Include(g => g.Guardian).ThenInclude(p => p.Gender)
                .AsNoTracking().ToListAsync();

        public override async Task<GuardianWard> Get(params object[] key)
        {
            if (key.Count() > 1 && key[0] is long guardianId && key[1] is long wardId)
            {
                return await dbSet
                .Include(g => g.Guardian).ThenInclude(p => p.Address).ThenInclude(a => a.Region)
                .Include(g => g.Guardian).ThenInclude(p => p.Address).ThenInclude(a => a.District)
                .Include(g => g.Guardian).ThenInclude(p => p.Address).ThenInclude(a => a.City).ThenInclude(c => c.CityType)
                .Include(g => g.Guardian).ThenInclude(p => p.Address).ThenInclude(a => a.Street).ThenInclude(s => s.StreetType)
                .Include(g => g.Guardian).ThenInclude(p => p.CountryOfBirth)
                .Include(g => g.Guardian).ThenInclude(p => p.PlaceOfBirth)
                .Include(g => g.Guardian).ThenInclude(p => p.Gender)
                .AsNoTracking().SingleOrDefaultAsync(g => g.GuardianId == guardianId && g.WardId == wardId);
            }
            else
                throw new ArgumentException("Guardian Getter needs two arguments of type long");
        }

        public override async Task<GuardianWard> Post(GuardianWard guardian)
        {
            dbSet.Update(guardian);
            await db.SaveChangesAsync();
            db.Entry(guardian).State = EntityState.Detached;
            guardian.Guardian = await db.People
                .Include(p => p.Address).ThenInclude(a => a.Region)
                .Include(p => p.Address).ThenInclude(a => a.District)
                .Include(p => p.Address).ThenInclude(a => a.City).ThenInclude(c => c.CityType)
                .Include(p => p.Address).ThenInclude(a => a.Street).ThenInclude(s => s.StreetType)
                .Include(p => p.CountryOfBirth)
                .Include(p => p.PlaceOfBirth)
                .Include(p => p.Gender)
                .AsNoTracking().SingleOrDefaultAsync(p => p.Id == guardian.GuardianId);
            return guardian;
        }

        public override async Task<GuardianWard> Put(GuardianWard guardian) => await Post(guardian);
    }
}
