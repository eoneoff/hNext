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
    public class DoctorSpecialtyRepository : Repository<DoctorSpecialty>, IDoctorSpecialtyRepository
    {
        public DoctorSpecialtyRepository(hNextDbContext db) : base(db)
        {
        }

        public async Task<DoctorSpecialty> Exists(long doctorId, int specialtyId) => await dbSet
                .Include(s => s.Specialty)
                .AsNoTracking().SingleOrDefaultAsync(s => s.DoctorId == doctorId && s.SpecialtyId == specialtyId);

        public override async Task<DoctorSpecialty> Post(DoctorSpecialty specialty)
        {
            dbSet.Add(specialty);
            await db.SaveChangesAsync();
            return await dbSet
                .Include(s => s.Specialty).AsNoTracking()
                .SingleOrDefaultAsync(s => s.DoctorId == specialty.DoctorId && s.SpecialtyId == specialty.SpecialtyId);
        }

        public override async Task<DoctorSpecialty> Put(DoctorSpecialty specialty, params object[] key)
        {
            dbSet.Update(specialty);
            await db.SaveChangesAsync();
            return await dbSet
                .Include(s => s.Specialty).AsNoTracking()
                .SingleOrDefaultAsync(s => s.DoctorId == specialty.DoctorId && s.SpecialtyId == specialty.SpecialtyId);
        }
    }
}
