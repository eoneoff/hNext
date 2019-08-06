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
            .Include(p=>p.Person).ThenInclude(p=>p.Phones).ThenInclude(p=>p.Phone)
            .Include(p => p.Person).ThenInclude(p => p.Emails).ThenInclude(e=>e.Email)
            .Include(p=>p.Person).ThenInclude(p => p.Documents).ThenInclude(d => d.DocumentType)
            .Include(p => p.Person).ThenInclude(p => p.Guardians).ThenInclude(g => g.Guardian).ThenInclude(g => g.Address).ThenInclude(a => a.Region)
            .Include(p => p.Person).ThenInclude(p => p.Guardians).ThenInclude(g => g.Guardian).ThenInclude(g => g.Address).ThenInclude(a => a.District)
            .Include(p => p.Person).ThenInclude(p => p.Guardians).ThenInclude(g => g.Guardian).ThenInclude(g => g.Address).ThenInclude(a => a.City).ThenInclude(c => c.CityType)
            .Include(p => p.Person).ThenInclude(p => p.Guardians).ThenInclude(g => g.Guardian).ThenInclude(g => g.Address).ThenInclude(a => a.Street).ThenInclude(s => s.StreetType)
            .AsNoTracking().ToListAsync();

        public async override Task<Patient> Get(params object[] key)
        {
            if (key.Count() > 1 && key[0] is long id)
            {
                return await dbSet
                    .Include(p => p.Person).ThenInclude(p => p.Address).ThenInclude(a => a.Country)
                    .Include(p => p.Person).ThenInclude(p => p.Address).ThenInclude(a => a.Region)
                    .Include(p => p.Person).ThenInclude(p => p.Address).ThenInclude(a => a.District)
                    .Include(p => p.Person).ThenInclude(p => p.Address).ThenInclude(a => a.City)
                    .Include(p => p.Person).ThenInclude(p => p.Address).ThenInclude(a => a.Street)
                    .Include(p => p.Person).ThenInclude(p => p.CountryOfBirth)
                    .Include(p => p.Person).ThenInclude(p => p.PlaceOfBirth)
                    .Include(p => p.Person).ThenInclude(p => p.Gender)
                    .Include(p => p.Person).ThenInclude(p => p.Phones).ThenInclude(p => p.Phone)
                    .Include(p => p.Person).ThenInclude(p => p.Emails).ThenInclude(e => e.Email)
                    .Include(p => p.Person).ThenInclude(p => p.Documents).ThenInclude(d => d.DocumentType)
                    .Include(p => p.Person).ThenInclude(p => p.Guardians).ThenInclude(g => g.Guardian).ThenInclude(g => g.Address).ThenInclude(a => a.Region)
                    .Include(p => p.Person).ThenInclude(p => p.Guardians).ThenInclude(g => g.Guardian).ThenInclude(g => g.Address).ThenInclude(a => a.District)
                    .Include(p => p.Person).ThenInclude(p => p.Guardians).ThenInclude(g => g.Guardian).ThenInclude(g => g.Address).ThenInclude(a => a.City).ThenInclude(c => c.CityType)
                    .Include(p => p.Person).ThenInclude(p => p.Guardians).ThenInclude(g => g.Guardian).ThenInclude(g => g.Address).ThenInclude(a => a.Street).ThenInclude(s => s.StreetType)
                    .AsNoTracking().SingleOrDefaultAsync(p => p.Id == id);
            }
            else
                throw new ArgumentException("Patient Getter needs argument of type long");
        }

        public async Task<IEnumerable<Patient>> SearchPatients(PatientSearchModel model) => await db.SearchPatients(model)
            .Include(p => p.Person).ThenInclude(p => p.Address).ThenInclude(a => a.Country)
            .Include(p => p.Person).ThenInclude(p => p.Address).ThenInclude(a => a.Region)
            .Include(p => p.Person).ThenInclude(p => p.Address).ThenInclude(a => a.District)
            .Include(p => p.Person).ThenInclude(p => p.Address).ThenInclude(a => a.City).ThenInclude(c => c.CityType)
            .Include(p => p.Person).ThenInclude(p => p.Address).ThenInclude(a => a.Street).ThenInclude(s => s.StreetType)
            .Include(p => p.Person).ThenInclude(p => p.CountryOfBirth)
            .Include(p => p.Person).ThenInclude(p => p.PlaceOfBirth)
            .Include(p => p.Person).ThenInclude(p => p.Gender)
            .Include(p => p.Person).ThenInclude(p => p.Phones).ThenInclude(p => p.Phone)
            .Include(p => p.Person).ThenInclude(p => p.Emails).ThenInclude(e => e.Email)
            .Include(p => p.Person).ThenInclude(p => p.Documents).ThenInclude(d => d.DocumentType)
            .Include(p => p.Person).ThenInclude(p => p.Guardians).ThenInclude(g => g.Guardian).ThenInclude(g => g.Address).ThenInclude(a => a.Region)
            .Include(p => p.Person).ThenInclude(p => p.Guardians).ThenInclude(g => g.Guardian).ThenInclude(g => g.Address).ThenInclude(a => a.District)
            .Include(p => p.Person).ThenInclude(p => p.Guardians).ThenInclude(g => g.Guardian).ThenInclude(g => g.Address).ThenInclude(a => a.City).ThenInclude(c => c.CityType)
            .Include(p => p.Person).ThenInclude(p => p.Guardians).ThenInclude(g => g.Guardian).ThenInclude(g => g.Address).ThenInclude(a => a.Street).ThenInclude(s => s.StreetType)
            .Include(p => p.CaseHistories).ThenInclude(h => h.DocumentRegistry)
            .Include(p => p.CaseHistories).ThenInclude(h => h.Hospital)
            .Include(p => p.CaseHistories).ThenInclude(h => h.Admissions).ThenInclude(a => a.Department)
            .AsNoTracking().ToListAsync();

        public override async Task<Patient> Post(Patient item)
        {
            dbSet.Update(item);
            await db.SaveChangesAsync();
            return await dbSet
                .Include(p => p.Person).ThenInclude(p => p.Address).ThenInclude(a => a.Country)
                .Include(p => p.Person).ThenInclude(p => p.Address).ThenInclude(a => a.Region)
                .Include(p => p.Person).ThenInclude(p => p.Address).ThenInclude(a => a.District)
                .Include(p => p.Person).ThenInclude(p => p.Address).ThenInclude(a => a.City).ThenInclude(c => c.CityType)
                .Include(p => p.Person).ThenInclude(p => p.Address).ThenInclude(a => a.Street).ThenInclude(s => s.StreetType)
                .Include(p => p.Person).ThenInclude(p => p.CountryOfBirth)
                .Include(p => p.Person).ThenInclude(p => p.PlaceOfBirth)
                .Include(p => p.Person).ThenInclude(p => p.Gender)
                .Include(p => p.Person).ThenInclude(p => p.Phones).ThenInclude(p => p.Phone)
                .Include(p => p.Person).ThenInclude(p => p.Emails).ThenInclude(e => e.Email)
                .Include(p => p.Person).ThenInclude(p => p.Documents).ThenInclude(d => d.DocumentType)
                .Include(p => p.Person).ThenInclude(p => p.Guardians).ThenInclude(g => g.Guardian).ThenInclude(g => g.Address).ThenInclude(a => a.Region)
                .Include(p => p.Person).ThenInclude(p => p.Guardians).ThenInclude(g => g.Guardian).ThenInclude(g => g.Address).ThenInclude(a => a.District)
                .Include(p => p.Person).ThenInclude(p => p.Guardians).ThenInclude(g => g.Guardian).ThenInclude(g => g.Address).ThenInclude(a => a.City).ThenInclude(c => c.CityType)
                .Include(p => p.Person).ThenInclude(p => p.Guardians).ThenInclude(g => g.Guardian).ThenInclude(g => g.Address).ThenInclude(a => a.Street).ThenInclude(s => s.StreetType)
                .AsNoTracking().SingleOrDefaultAsync(p => p.Id == item.Id);
        }

        public override async Task<Patient> Put(Patient item)
        {
            dbSet.Update(item);
            await db.SaveChangesAsync();
            return await dbSet
                .Include(p => p.Person).ThenInclude(p => p.Address).ThenInclude(a => a.Country)
                .Include(p => p.Person).ThenInclude(p => p.Address).ThenInclude(a => a.Region)
                .Include(p => p.Person).ThenInclude(p => p.Address).ThenInclude(a => a.District)
                .Include(p => p.Person).ThenInclude(p => p.Address).ThenInclude(a => a.City).ThenInclude(c => c.CityType)
                .Include(p => p.Person).ThenInclude(p => p.Address).ThenInclude(a => a.Street).ThenInclude(s => s.StreetType)
                .Include(p => p.Person).ThenInclude(p => p.CountryOfBirth)
                .Include(p => p.Person).ThenInclude(p => p.PlaceOfBirth)
                .Include(p => p.Person).ThenInclude(p => p.Gender)
                .Include(p => p.Person).ThenInclude(p => p.Phones).ThenInclude(p => p.Phone)
                .Include(p => p.Person).ThenInclude(p => p.Emails).ThenInclude(e => e.Email)
                .Include(p => p.Person).ThenInclude(p => p.Documents).ThenInclude(d => d.DocumentType)
                .Include(p => p.Person).ThenInclude(p => p.Guardians).ThenInclude(g => g.Guardian).ThenInclude(g => g.Address).ThenInclude(a => a.Region)
                .Include(p => p.Person).ThenInclude(p => p.Guardians).ThenInclude(g => g.Guardian).ThenInclude(g => g.Address).ThenInclude(a => a.District)
                .Include(p => p.Person).ThenInclude(p => p.Guardians).ThenInclude(g => g.Guardian).ThenInclude(g => g.Address).ThenInclude(a => a.City).ThenInclude(c => c.CityType)
                .Include(p => p.Person).ThenInclude(p => p.Guardians).ThenInclude(g => g.Guardian).ThenInclude(g => g.Address).ThenInclude(a => a.Street).ThenInclude(s => s.StreetType)
                .Include(p => p.CaseHistories).ThenInclude(h => h.DocumentRegistry)
                .Include(p => p.CaseHistories).ThenInclude(h => h.Hospital)
                .Include(p => p.CaseHistories).ThenInclude(h => h.Admissions).ThenInclude(a => a.Department)
                .AsNoTracking().SingleOrDefaultAsync(p => p.Id == item.Id);
        }

        public async Task<IEnumerable<PatientDiagnosys>> GetDiagnoses(long id) =>
            await db.PatientDiagnoses.Include(pd => pd.Diagnosys).ThenInclude(d => d.ICD)
                .Where(pd => pd.PatientId == id).AsNoTracking().ToListAsync();
    }
}
