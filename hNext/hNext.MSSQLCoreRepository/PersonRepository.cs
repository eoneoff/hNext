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
            .AsNoTracking().ToListAsync();

        public override async Task<Person> Get(long id) => await dbSet
            .Include(p => p.Address).ThenInclude(a => a.Region)
            .Include(p => p.Address).ThenInclude(a => a.District)
            .Include(p => p.Address).ThenInclude(a => a.City).ThenInclude(c => c.CityType)
            .Include(p => p.Address).ThenInclude(a => a.Street).ThenInclude(s => s.StreetType)
            .Include(p => p.CountryOfBirth)
            .Include(p => p.PlaceOfBirth)
            .Include(p => p.Gender)
            .AsNoTracking().SingleOrDefaultAsync(p => p.Id == id);

        public override async Task<Person> Post(Person item)
        {
            return await PersonChange(item, (person) => dbSet.Add(person),
                async (address) => await new AddressRepository(db).Post(address));
        }

        public override async Task<Person> Put(Person item)
        {
            return await PersonChange(item, (person) => db.Entry(person).State = EntityState.Modified,
                async (address) => await new AddressRepository(db).Put(address));
        }

        private async Task<Person> PersonChange(Person person, Action<Person> action, Func<Address, Task<Address>> addressAction)
        {
            Person propertyStore = new Person();
            StoreDependencyProperties(person, propertyStore);
            Address address = person.Address;
            if (address != null)
            {
                address = await addressAction(address);
                person.Address = null;
                person.AddressId = address.Id;
            }

            action(person);
            await db.SaveChangesAsync();

            return await dbSet
            .Include(p => p.Address).ThenInclude(a => a.Region)
            .Include(p => p.Address).ThenInclude(a => a.District)
            .Include(p => p.Address).ThenInclude(a => a.City).ThenInclude(c => c.CityType)
            .Include(p => p.Address).ThenInclude(a => a.Street).ThenInclude(s => s.StreetType)
            .Include(p => p.CountryOfBirth)
            .Include(p => p.PlaceOfBirth)
            .Include(p => p.Gender)
            .AsNoTracking().SingleOrDefaultAsync(p => p.Id == person.Id);
        }

        private void StoreDependencyProperties(Person source, Person destination)
        {
            destination.CountryOfBirth = source.CountryOfBirth;
            source.CountryOfBirth = null;
            destination.PlaceOfBirth = source.PlaceOfBirth;
            source.PlaceOfBirth = null;
            destination.Gender = source.Gender;
            source.Gender = null;
            destination.Phones = source.Phones;
            source.Phones = null;
            destination.Emails = source.Emails;
            source.Emails = null;
            destination.Patient = source.Patient;
            source.Patient = null;
            destination.Guardians = source.Guardians;
            source.Guardians = null;
            destination.Wards = source.Wards;
            source.Wards = null;
            destination.Documents = source.Documents;
            source.Documents = null;
        }
    }
}
