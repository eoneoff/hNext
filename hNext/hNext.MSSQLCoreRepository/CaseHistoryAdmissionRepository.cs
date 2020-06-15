using hNext.DbAccessMSSQLCore;
using hNext.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace hNext.MSSQLCoreRepository
{
    public class CaseHistoryAdmissionRepository : Repository<CaseHistoryAdmission>
    {
        public CaseHistoryAdmissionRepository(hNextDbContext db) : base(db)
        {
        }

        public override async Task<CaseHistoryAdmission> Post(CaseHistoryAdmission admission)
        {
            dbSet.Update(admission);
            await db.SaveChangesAsync();
            return await dbSet
                .Include(a => a.Department)
                .AsNoTracking().SingleOrDefaultAsync(a => a.Id == admission.Id);
        }

        public override async Task<CaseHistoryAdmission> Put(CaseHistoryAdmission admission, params object[] key) =>
            await Post(admission);
    }
}
