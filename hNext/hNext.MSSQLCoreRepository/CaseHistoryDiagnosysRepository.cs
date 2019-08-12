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
    public class CaseHistoryDiagnosysRepository : Repository<CaseHistoryDiagnosys>
    {
        public CaseHistoryDiagnosysRepository(hNextDbContext db) : base(db)
        {
        }

        public override async Task<CaseHistoryDiagnosys> Post(CaseHistoryDiagnosys history)
        {
            dbSet.Add(history);
            await db.SaveChangesAsync();
            return await dbSet.
                Include(h => h.Diagnosys).ThenInclude(d => d.ICD)
                .AsNoTracking().SingleOrDefaultAsync(h => h.CaseHistoryId == history.CaseHistoryId
                    && h.DiagnosysId == history.DiagnosysId);
        }

        public override async Task<CaseHistoryDiagnosys> Delete(params object[] key)
        {
            if (key[0] is long historyId && key[1] is long diagnosysId)
            {
                if (await dbSet.FindAsync(historyId, diagnosysId) is CaseHistoryDiagnosys diagnosys)
                {
                    diagnosys.Active = false;
                    dbSet.Update(diagnosys);
                    await db.SaveChangesAsync();
                    return diagnosys;
                }
                else
                    return null;
            }
            else
                throw new ArgumentException("CaseHistoryDiagnosys delete requires two arguments of type long");
        }
    }
}
