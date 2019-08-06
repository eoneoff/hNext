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
    public class DepartmentSpecialtyRepository : Repository<DepartmentSpecialty>
    {
        public DepartmentSpecialtyRepository(hNextDbContext db) : base(db)
        {
        }

        public override async Task<DepartmentSpecialty> Post(DepartmentSpecialty specialty)
        {
            dbSet.Add(specialty);
            await db.SaveChangesAsync();
            return await dbSet
                .Include(s => s.Specialty).AsNoTracking()
                .SingleOrDefaultAsync(s => s.DeparmentId == specialty.DeparmentId && s.SpecialtyId == specialty.SpecialtyId);
        }
    }
}
