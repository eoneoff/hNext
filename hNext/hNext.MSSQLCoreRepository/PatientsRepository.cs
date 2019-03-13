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
    public class PatientsRepository  :Repository<Patient>, IPatientsRepository
    {
        public PatientsRepository(hNextDbContext db) : base(db) { }

        public async override Task<IEnumerable<Patient>> Get() => await dbSet
            .Include(p => p.Person).ThenInclude(p => p.Address).ThenInclude(a => a.Country)
            .Include(p => p.Person).ThenInclude(p => p.Address).ThenInclude(a => a.Region)
            .Include(p => p.Person).ThenInclude(p => p.Address).ThenInclude(a => a.District)
            .Include(p => p.Person).ThenInclude(p => p.Address).ThenInclude(a => a.City)
            .Include(p => p.Person).ThenInclude(p => p.Address).ThenInclude(a => a.Street)
            .Include(p=>p.Person).ThenInclude(p=>p.CountryOfBirth)
            .Include(p=>p.Person).ThenInclude(p=>p.PlaceOfBirth)
            .Include(p=>p.Person).ThenInclude(p=>p.Gender)
            .AsNoTracking().ToListAsync();

        public async override Task<Patient> Get(long id) => await dbSet
            .Include(p => p.Person).ThenInclude(p => p.Address).ThenInclude(a => a.Country)
            .Include(p => p.Person).ThenInclude(p => p.Address).ThenInclude(a => a.Region)
            .Include(p => p.Person).ThenInclude(p => p.Address).ThenInclude(a => a.District)
            .Include(p => p.Person).ThenInclude(p => p.Address).ThenInclude(a => a.City)
            .Include(p => p.Person).ThenInclude(p => p.Address).ThenInclude(a => a.Street)
            .Include(p => p.Person).ThenInclude(p => p.CountryOfBirth)
            .Include(p => p.Person).ThenInclude(p => p.PlaceOfBirth)
            .Include(p => p.Person).ThenInclude(p => p.Gender)
            .AsNoTracking().SingleOrDefaultAsync(p => p.Id == id);

        public async Task<IEnumerable<Patient>> SearchPatients(PatientSearchModel model) => await db.SearchPatients(model)
            .Include(p => p.Person).ThenInclude(p => p.Address).ThenInclude(a => a.Country)
            .Include(p => p.Person).ThenInclude(p => p.Address).ThenInclude(a => a.Region)
            .Include(p => p.Person).ThenInclude(p => p.Address).ThenInclude(a => a.District)
            .Include(p => p.Person).ThenInclude(p => p.Address).ThenInclude(a => a.City).ThenInclude(c => c.CityType)
            .Include(p => p.Person).ThenInclude(p => p.Address).ThenInclude(a => a.Street).ThenInclude(s => s.StreetType)
            .Include(p => p.Person).ThenInclude(p => p.CountryOfBirth)
            .Include(p => p.Person).ThenInclude(p => p.PlaceOfBirth)
            .Include(p => p.Person).ThenInclude(p => p.Gender)
            .AsNoTracking().ToListAsync();

        public override async Task<Patient> Post(Patient item)
        {
            return await ChangePatient(item, (patient) => dbSet.Add(patient),
                async (person) => await new PersonRepository(db).Post(person));
        }

        public override async Task<Patient> Put(Patient item)
        {
            return await ChangePatient(item, (patient) => db.Entry(patient).State = EntityState.Modified,
                async (person) => await new PersonRepository(db).Put(person));
        }

        private async Task<Patient> ChangePatient(Patient patient, Action<Patient> action, Func<Person, Task<Person>> personAction)
        {
            Person person = patient.Person;
            if (person != null)
            {
                person = await personAction(person);
                patient.Person = null;
                patient.PersonId = person.Id;
            }

            action(patient);
            await db.SaveChangesAsync();

            patient.Person = person ?? await db.People
            .Include(p => p.Address).ThenInclude(a => a.Region)
            .Include(p => p.Address).ThenInclude(a => a.District)
            .Include(p => p.Address).ThenInclude(a => a.City).ThenInclude(c => c.CityType)
            .Include(p => p.Address).ThenInclude(a => a.Street).ThenInclude(s => s.StreetType)
            .Include(p => p.CountryOfBirth)
            .Include(p => p.PlaceOfBirth)
            .Include(p => p.Gender)
            .AsNoTracking().SingleOrDefaultAsync(p => p.Id == patient.PersonId);

            return patient;
        }
    }
}
