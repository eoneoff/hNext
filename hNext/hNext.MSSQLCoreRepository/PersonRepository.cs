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
    public class PersonRepository : Repository<Person>, IPersonRepository
    {
        public PersonRepository(hNextDbContext db) : base(db) { }

        public override async Task<IEnumerable<Person>> Get() => await dbSet
            .Include(p => p.Address).ThenInclude(a => a.Region)
            .Include(p => p.Address).ThenInclude(a => a.District)
            .Include(p => p.Address).ThenInclude(a => a.City).ThenInclude(c => c.CityType)
            .Include(p => p.Address).ThenInclude(a => a.Street).ThenInclude(s => s.StreetType)
            .Include(p => p.CountryOfBirth)
            .Include(p => p.PlaceOfBirth)
            .Include(p => p.Gender)
            .Include(p => p.Phones).ThenInclude(p=>p.Phone)
            .Include(p => p.Emails).ThenInclude(e=>e.Email)
            .AsNoTracking().ToListAsync();

        public override async Task<Person> Get(long id) => await dbSet
            .Include(p => p.Address).ThenInclude(a => a.Region)
            .Include(p => p.Address).ThenInclude(a => a.District)
            .Include(p => p.Address).ThenInclude(a => a.City).ThenInclude(c => c.CityType)
            .Include(p => p.Address).ThenInclude(a => a.Street).ThenInclude(s => s.StreetType)
            .Include(p => p.CountryOfBirth)
            .Include(p => p.PlaceOfBirth)
            .Include(p => p.Gender)
            .Include(p => p.Phones).ThenInclude(p => p.Phone)
            .Include(p => p.Emails).ThenInclude(e => e.Email)
            .AsNoTracking().SingleOrDefaultAsync(p => p.Id == id);

        public override async Task<Person> Post(Person item)
        {
            dbSet.Update(item);
            await db.SaveChangesAsync();
            return await dbSet
                .Include(p => p.Address).ThenInclude(a => a.Country)
                .Include(p => p.Address).ThenInclude(a => a.Region)
                .Include(p => p.Address).ThenInclude(a => a.District)
                .Include(p => p.Address).ThenInclude(a => a.City).ThenInclude(c => c.CityType)
                .Include(p => p.Address).ThenInclude(a => a.Street).ThenInclude(s => s.StreetType)
                .Include(p => p.CountryOfBirth)
                .Include(p => p.PlaceOfBirth)
                .Include(p => p.Gender)
                .Include(p => p.Phones).ThenInclude(p => p.Phone)
                .Include(p => p.Emails).ThenInclude(e => e.Email)
                .AsNoTracking().SingleOrDefaultAsync(p => p.Id == item.Id);
        }

        public override async Task<Person> Put(Person item)
        {
            dbSet.Update(item);
            await db.SaveChangesAsync();
            return await dbSet
                .Include(p => p.Address).ThenInclude(a => a.Country)
                .Include(p => p.Address).ThenInclude(a => a.Region)
                .Include(p => p.Address).ThenInclude(a => a.District)
                .Include(p => p.Address).ThenInclude(a => a.City).ThenInclude(c => c.CityType)
                .Include(p => p.Address).ThenInclude(a => a.Street).ThenInclude(s => s.StreetType)
                .Include(p => p.CountryOfBirth)
                .Include(p => p.PlaceOfBirth)
                .Include(p => p.Gender)
                .Include(p => p.Phones).ThenInclude(p => p.Phone)
                .Include(p => p.Emails).ThenInclude(e => e.Email)
                .AsNoTracking().SingleOrDefaultAsync(p => p.Id == item.Id);
        }

        public async Task<Person> Exists(Person person)
        {
            return (await dbSet.AsNoTracking().SingleOrDefaultAsync(p => p.Id != person.Id
                                                    && p.FirstName == person.FirstName
                                                    && p.FamilyName == person.FamilyName
                                                    && p.Patronimic == person.Patronimic
                                                    && p.DateOfBirth == person.DateOfBirth));
        }
    }
}
