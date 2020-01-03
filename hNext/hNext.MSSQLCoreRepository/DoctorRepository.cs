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
    public class DoctorRepository : Repository<Doctor>, IDoctorRepository
    {
        public DoctorRepository(hNextDbContext db) : base(db)
        {
        }

        public override async Task<IEnumerable<Doctor>> Get() => await dbSet
            .Include(d => d.Person).ThenInclude(p => p.Address).ThenInclude(a => a.Country)
            .Include(d => d.Person).ThenInclude(p => p.Address).ThenInclude(a => a.Region)
            .Include(d => d.Person).ThenInclude(p => p.Address).ThenInclude(a => a.District)
            .Include(d => d.Person).ThenInclude(p => p.Address).ThenInclude(a => a.City)
            .Include(d => d.Person).ThenInclude(p => p.Address).ThenInclude(a => a.Street)
            .Include(d => d.Person).ThenInclude(p => p.CountryOfBirth)
            .Include(d => d.Person).ThenInclude(p => p.PlaceOfBirth)
            .Include(d => d.Person).ThenInclude(p => p.Gender)
            .Include(d => d.Person).ThenInclude(p => p.Phones).ThenInclude(p => p.Phone)
            .Include(d => d.Person).ThenInclude(p => p.Emails).ThenInclude(e => e.Email)
            .Include(d => d.Person).ThenInclude(p => p.Documents).ThenInclude(d => d.DocumentType)
            .Include(d => d.DoctorPositions).Include(d => d.DoctorSpecialties)
            .AsNoTracking().ToListAsync();

        public override async Task<Doctor> Get(params object[] keys)
        {
            if (keys.Count() > 1 && keys[0] is long id)
            {
                return await dbSet
                    .Include(d => d.Person).ThenInclude(p => p.Address).ThenInclude(a => a.Country)
                    .Include(d => d.Person).ThenInclude(p => p.Address).ThenInclude(a => a.Region)
                    .Include(d => d.Person).ThenInclude(p => p.Address).ThenInclude(a => a.District)
                    .Include(d => d.Person).ThenInclude(p => p.Address).ThenInclude(a => a.City)
                    .Include(d => d.Person).ThenInclude(p => p.Address).ThenInclude(a => a.Street)
                    .Include(d => d.Person).ThenInclude(p => p.CountryOfBirth)
                    .Include(d => d.Person).ThenInclude(p => p.PlaceOfBirth)
                    .Include(d => d.Person).ThenInclude(p => p.Gender)
                    .Include(d => d.Person).ThenInclude(p => p.Phones).ThenInclude(p => p.Phone)
                    .Include(d => d.Person).ThenInclude(p => p.Emails).ThenInclude(e => e.Email)
                    .Include(d => d.Person).ThenInclude(p => p.Documents).ThenInclude(d => d.DocumentType)
                    .AsNoTracking().SingleOrDefaultAsync(d => d.Id == id);

            }
            else
                throw new ArgumentException("Get Doctor requires 1 argunent of type long");
        }

        public override async Task<Doctor> Post(Doctor doctor)
        {
            dbSet.Update(doctor);
            await db.SaveChangesAsync();
            return await dbSet
                .Include(d => d.Person).ThenInclude(p => p.Address).ThenInclude(a => a.Country)
                .Include(d => d.Person).ThenInclude(p => p.Address).ThenInclude(a => a.Region)
                .Include(d => d.Person).ThenInclude(p => p.Address).ThenInclude(a => a.District)
                .Include(d => d.Person).ThenInclude(p => p.Address).ThenInclude(a => a.City)
                .Include(d => d.Person).ThenInclude(p => p.Address).ThenInclude(a => a.Street)
                .Include(d => d.Person).ThenInclude(p => p.CountryOfBirth)
                .Include(d => d.Person).ThenInclude(p => p.PlaceOfBirth)
                .Include(d => d.Person).ThenInclude(p => p.Gender)
                .Include(d => d.Person).ThenInclude(p => p.Phones).ThenInclude(p => p.Phone)
                .Include(d => d.Person).ThenInclude(p => p.Emails).ThenInclude(e => e.Email)
                .Include(d => d.Person).ThenInclude(p => p.Documents).ThenInclude(d => d.DocumentType)
                .AsNoTracking().SingleOrDefaultAsync(d => d.Id == doctor.Id);

        }

        public override async Task<Doctor> Put(Doctor doctor) => await Post(doctor);

        public async Task<IEnumerable<Doctor>> SearchDoctors(DoctorSearchModel model) => await db.SearchDoctor(model)
            .Include(d => d.Person).ThenInclude(p => p.Address).ThenInclude(a => a.Country)
            .Include(d => d.Person).ThenInclude(p => p.Address).ThenInclude(a => a.Region)
            .Include(d => d.Person).ThenInclude(p => p.Address).ThenInclude(a => a.District)
            .Include(d => d.Person).ThenInclude(p => p.Address).ThenInclude(a => a.City)
            .Include(d => d.Person).ThenInclude(p => p.Address).ThenInclude(a => a.Street)
            .Include(d => d.Person).ThenInclude(p => p.CountryOfBirth)
            .Include(d => d.Person).ThenInclude(p => p.PlaceOfBirth)
            .Include(d => d.Person).ThenInclude(p => p.Gender)
            .Include(d => d.Person).ThenInclude(p => p.Phones).ThenInclude(p => p.Phone)
            .Include(d => d.Person).ThenInclude(p => p.Emails).ThenInclude(e => e.Email)
            .Include(d => d.Person).ThenInclude(p => p.Documents).ThenInclude(d => d.DocumentType)
            .Include(d => d.DoctorSpecialties).ThenInclude(s => s.Specialty)
            .Include(d => d.DoctorPositions).ThenInclude(p => p.Position)
            .Include(d => d.DoctorPositions).ThenInclude(p => p.Specialty)
            .Include(d => d.DoctorPositions).ThenInclude(p => p.Hospital)
            .Include(d => d.DoctorPositions).ThenInclude(p => p.Department)
            .Include(d => d.Diplomas)
            .AsNoTracking().ToListAsync();

        public async Task<IEnumerable<Specialty>> Specialties(long id) => await db.DoctorSpecialties
            .Where(ds => ds.DoctorId == id).Select(ds => ds.Specialty).ToListAsync();
    }
}
