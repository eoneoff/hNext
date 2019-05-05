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
            .Include(p => p.Documents).ThenInclude(d => d.DocumentType)
            .Include(p => p.Guardians).ThenInclude(g => g.Guardian).ThenInclude(g => g.Address).ThenInclude(a => a.Region)
            .Include(p => p.Guardians).ThenInclude(g => g.Guardian).ThenInclude(g => g.Address).ThenInclude(a => a.District)
            .Include(p => p.Guardians).ThenInclude(g => g.Guardian).ThenInclude(g => g.Address).ThenInclude(a => a.City).ThenInclude(c => c.CityType)
            .Include(p => p.Guardians).ThenInclude(g => g.Guardian).ThenInclude(g => g.Address).ThenInclude(a => a.Street).ThenInclude(s => s.StreetType)
            .AsNoTracking().ToListAsync();

        public override async Task<Person> Get(params object[] key)
        {
            if (key.Count() > 0 && key[0] is long id)
            {
                return await dbSet
                .Include(p => p.Address).ThenInclude(a => a.Region)
                .Include(p => p.Address).ThenInclude(a => a.District)
                .Include(p => p.Address).ThenInclude(a => a.City).ThenInclude(c => c.CityType)
                .Include(p => p.Address).ThenInclude(a => a.Street).ThenInclude(s => s.StreetType)
                .Include(p => p.CountryOfBirth)
                .Include(p => p.PlaceOfBirth)
                .Include(p => p.Gender)
                .Include(p => p.Phones).ThenInclude(p => p.Phone)
                .Include(p => p.Emails).ThenInclude(e => e.Email)
                .Include(p => p.Documents).ThenInclude(d => d.DocumentType)
                .Include(p => p.Guardians).ThenInclude(g => g.Guardian).ThenInclude(g => g.Address).ThenInclude(a => a.Region)
                .Include(p => p.Guardians).ThenInclude(g => g.Guardian).ThenInclude(g => g.Address).ThenInclude(a => a.District)
                .Include(p => p.Guardians).ThenInclude(g => g.Guardian).ThenInclude(g => g.Address).ThenInclude(a => a.City).ThenInclude(c => c.CityType)
                .Include(p => p.Guardians).ThenInclude(g => g.Guardian).ThenInclude(g => g.Address).ThenInclude(a => a.Street).ThenInclude(s => s.StreetType)
                .AsNoTracking().SingleOrDefaultAsync(p => p.Id == id);
            }
            else
                throw new ArgumentException("Person Getter needs argument of type long");
        }

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
                .Include(p => p.Documents).ThenInclude(d => d.DocumentType)
                .Include(p => p.Guardians).ThenInclude(g => g.Guardian).ThenInclude(g => g.Address).ThenInclude(a => a.Region)
                .Include(p => p.Guardians).ThenInclude(g => g.Guardian).ThenInclude(g => g.Address).ThenInclude(a => a.District)
                .Include(p => p.Guardians).ThenInclude(g => g.Guardian).ThenInclude(g => g.Address).ThenInclude(a => a.City).ThenInclude(c => c.CityType)
                .Include(p => p.Guardians).ThenInclude(g => g.Guardian).ThenInclude(g => g.Address).ThenInclude(a => a.Street).ThenInclude(s => s.StreetType)
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
                .Include(p => p.Documents).ThenInclude(d => d.DocumentType)
                .Include(p => p.Guardians).ThenInclude(g => g.Guardian).ThenInclude(g => g.Address).ThenInclude(a => a.Region)
                .Include(p => p.Guardians).ThenInclude(g => g.Guardian).ThenInclude(g => g.Address).ThenInclude(a => a.District)
                .Include(p => p.Guardians).ThenInclude(g => g.Guardian).ThenInclude(g => g.Address).ThenInclude(a => a.City).ThenInclude(c => c.CityType)
                .Include(p => p.Guardians).ThenInclude(g => g.Guardian).ThenInclude(g => g.Address).ThenInclude(a => a.Street).ThenInclude(s => s.StreetType)
                .AsNoTracking().SingleOrDefaultAsync(p => p.Id == item.Id);
        }

        public async Task<IEnumerable<Person>> Exists(Person person)
        {
            return await dbSet.AsNoTracking().Where(p => p.Id != person.Id
                                                    && p.FirstName.ToLower() == person.FirstName.ToLower()
                                                    && p.FamilyName.ToLower() == person.FamilyName.ToLower()
                                                    && p.Patronimic.ToLower() == person.Patronimic.ToLower()
                                                    && p.DateOfBirth.GetValueOrDefault().Date == person.DateOfBirth.GetValueOrDefault().Date)
                                                    .ToListAsync();
        }

        public async Task<IEnumerable<Person>> Search(params string[] name)
        {
            var query = dbSet
                .Include(p => p.Address).ThenInclude(a => a.Country)
                .Include(p => p.Address).ThenInclude(a => a.Region)
                .Include(p => p.Address).ThenInclude(a => a.District)
                .Include(p => p.Address).ThenInclude(a => a.City).ThenInclude(c => c.CityType)
                .Include(p => p.Address).ThenInclude(a => a.Street).ThenInclude(s => s.StreetType)
                .Include(p => p.CountryOfBirth)
                .Include(p => p.PlaceOfBirth)
                .Include(p => p.Gender).AsQueryable();
            
            if(name.Count() > 0)
            {
                query = query.Where(p => p.FamilyName.ToLower().StartsWith(name[0].ToLower()));
            }

            if(name.Count() > 1)
            {
                query = query.Where(p => p.FirstName.ToLower().StartsWith(name[1].ToLower()));
            }

            if(name.Count() > 2)
            {
                query = query.Where(p => p.Patronimic != null ? p.Patronimic.ToLower().StartsWith(name[1].ToLower()) : false);
            }

            return await query.AsNoTracking().ToListAsync();
        }
    }
}
