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
    public class CaseHistoryRepository : Repository<CaseHistory>, ICaseHistoryRepository
    {
        public CaseHistoryRepository(hNextDbContext db) : base(db)
        {
        }

        public override async Task<CaseHistory> Post(CaseHistory history)
        {
            db.DocumentRegistries.Add(history.DocumentRegistry);
            await db.SaveChangesAsync();
            dbSet.Update(history);
            await db.SaveChangesAsync();

            return await dbSet
                .Include(h => h.Hospital)
                .Include(h => h.Department)
                .AsNoTracking().SingleOrDefaultAsync(h => h.Id == history.Id);
        }
    }
}
