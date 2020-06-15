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
    public class DoctorPositionRepository : Repository<DoctorPosition>, IDoctorPositionRepository
    {
        public DoctorPositionRepository(hNextDbContext db) : base(db)
        {
        }

        public override async Task<DoctorPosition> Post(DoctorPosition position)
        {
            dbSet.Update(position);
            await db.SaveChangesAsync();
            return await dbSet
                .Include(p => p.Hospital)
                .Include(p => p.Department)
                .Include(p => p.Position)
                .Include(p => p.Specialty)
                .AsNoTracking().SingleOrDefaultAsync(p => p.Id == position.Id);
        }

        public override async Task<DoctorPosition> Put(DoctorPosition position, params object[] key) => await Post(position);

        public async Task<DoctorPosition> Exists(DoctorPosition position) => await dbSet
            .Include(p => p.Hospital)
            .Include(p => p.Department)
            .Include(p => p.Position)
            .Include(p => p.Specialty)
            .AsNoTracking().SingleOrDefaultAsync(p => p.Id == position.Id
                && p.DoctorId == position.DoctorId
                && p.SpecialtyId == position.SpecialtyId
                && p.PositionId == position.PositionId
                && p.HospitalId == position.HospitalId
                && p.DepartmentId == position.DepartmentId);
    }
}
