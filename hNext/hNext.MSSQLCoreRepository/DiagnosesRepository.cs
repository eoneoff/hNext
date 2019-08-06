using hNext.DbAccessMSSQLCore;
using hNext.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace hNext.MSSQLCoreRepository
{
    public class DiagnosesRepository : Poster<Diagnosys>
    {
        public DiagnosesRepository(hNextDbContext db) : base(db)
        {
        }

        public override async Task<IEnumerable<Diagnosys>> Get() =>
            await dbSet.Include(d => d.ICD).AsNoTracking().ToListAsync();

        public override async Task<Diagnosys> Post(Diagnosys diagnosys)
        {
            diagnosys.ICD = null;
            dbSet.Add(diagnosys);
            await db.SaveChangesAsync();
            return await dbSet.Include(d => d.ICD)
                .AsNoTracking()
                .SingleOrDefaultAsync(d => d.Id == diagnosys.Id);
        }
    }
}
