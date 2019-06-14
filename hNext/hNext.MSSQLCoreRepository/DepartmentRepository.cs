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
    public class DepartmentRepository : Repository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(hNextDbContext db) : base(db)
        {
        }

        public override async Task<IEnumerable<Department>> Get()
        {
            return await dbSet
                .Include(d => d.Hospital)
                .Include(d => d.Phones).ThenInclude(p => p.Phone)
                .Include(d => d.Emails).ThenInclude(e => e.Email)
                .Include(d => d.Specialties).ThenInclude(s => s.Specialty)
                .AsNoTracking().ToListAsync();
        }

        public override async Task<Department> Get(params object[] keys)
        {
            if (keys.Count() > 0 && keys[0] is int id)
            {
                return await dbSet
                    .Include(d => d.Hospital)
                    .Include(d => d.Phones).ThenInclude(p => p.Phone)
                    .Include(d => d.Emails).ThenInclude(e => e.Email)
                    .Include(d => d.Specialties).ThenInclude(s => s.Specialty)
                    .AsNoTracking().SingleOrDefaultAsync(d => d.Id == id);
            }
            else
                throw new ArgumentException("Get Department requires argument of type int");
        }

        public async Task<Department> Exists(Department department)
        {
            return await dbSet.SingleOrDefaultAsync(d => d.HospitalId == department.HospitalId
                && d.Name == department.Name);
        }

        public override async Task<Department> Post(Department department)
        {
            dbSet.Update(department);
            await db.SaveChangesAsync();
            department = await dbSet
                .Include(d => d.Hospital)
                .Include(d => d.Phones).ThenInclude(p => p.Phone)
                .Include(d => d.Emails).ThenInclude(e => e.Email)
                .Include(d => d.Specialties).ThenInclude(s => s.Specialty)
                .AsNoTracking().SingleOrDefaultAsync(d => d.Id == department.Id);
            return department;
        }

        public override async Task<Department> Put(Department department) => await Post(department);
    }
}
