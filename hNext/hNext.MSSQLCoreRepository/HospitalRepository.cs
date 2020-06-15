using hNext.DbAccessMSSQLCore;
using hNext.IRepository;
using hNext.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace hNext.MSSQLCoreRepository
{
    public class HospitalRepository : Repository<Hospital>, IHospitalRepository
    {
        public HospitalRepository(hNextDbContext db) : base(db)
        {
        }

        public async Task<bool> Exists(string name)
        {
            return await dbSet.CountAsync(h => h.Name == name) > 0;
        }

        public override async Task<IEnumerable<Hospital>> Get()
        {
            return await dbSet
                .Include(h => h.Address).ThenInclude(a => a.Country)
                .Include(h => h.Address).ThenInclude(a => a.Region)
                .Include(h => h.Address).ThenInclude(a => a.District)
                .Include(h => h.Address).ThenInclude(a => a.City).ThenInclude(c => c.CityType)
                .Include(h => h.Address).ThenInclude(a => a.Street).ThenInclude(a => a.StreetType)
                .Include(h => h.HospitalType).Include(h => h.PropertyType)
                .Include(h => h.Phones).ThenInclude(p => p.Phone)
                .Include(h => h.Emails).ThenInclude(e => e.Email)
                .Include(h => h.Licenses)
                .AsNoTracking().ToListAsync();
        }

        public override async Task<Hospital> Get(object[] key)
        {
            if (key[0] is int id)
            {
                return await dbSet
                    .Include(h => h.Address).ThenInclude(a => a.Country)
                    .Include(h => h.Address).ThenInclude(a => a.Region)
                    .Include(h => h.Address).ThenInclude(a => a.District)
                    .Include(h => h.Address).ThenInclude(a => a.City).ThenInclude(c => c.CityType)
                    .Include(h => h.Address).ThenInclude(a => a.Street).ThenInclude(a => a.StreetType)
                    .Include(h => h.HospitalType).Include(h => h.PropertyType)
                    .Include(h => h.Phones).ThenInclude(p => p.Phone)
                    .Include(h => h.Emails).ThenInclude(e => e.Email)
                    .Include(h => h.Licenses)
                    .AsNoTracking().SingleOrDefaultAsync(h => h.Id == id);
            }
            else
                throw new ArgumentException("Hospital Get requires one argument of type int");
        }
        

        public override async Task<Hospital> Post(Hospital hospital)
        {
            dbSet.Update(hospital);
            await db.SaveChangesAsync();
            return await dbSet
                .Include(h => h.Address).ThenInclude(a => a.Country)
                .Include(h => h.Address).ThenInclude(a => a.Region)
                .Include(h => h.Address).ThenInclude(a => a.District)
                .Include(h => h.Address).ThenInclude(a => a.City).ThenInclude(c => c.CityType)
                .Include(h => h.Address).ThenInclude(a => a.Street).ThenInclude(a => a.StreetType)
                .Include(h => h.HospitalType).Include(h => h.PropertyType)
                .Include(h => h.Phones).ThenInclude(p => p.Phone)
                .Include(h => h.Emails).ThenInclude(e => e.Email)
                .Include(h => h.Licenses)
                .AsNoTracking().SingleOrDefaultAsync(h => h.Id == hospital.Id);
        }

        public override async Task<Hospital> Put(Hospital hospital, params object[] key) => await Post(hospital);
    }
}
