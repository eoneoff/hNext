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

        public override async Task<CaseHistory> Get(params object[] keys)
        {
            if (keys[0] is long id)
            {
                return await dbSet
                    .Include(h => h.DocumentRegistry)
                    .Include(h => h.Hospital)
                    .Include(h => h.Admissions).ThenInclude(a => a.Department)
                    .Include(h => h.Patient).ThenInclude(h => h.Person)
                    .AsNoTracking().SingleOrDefaultAsync(h => h.Id == id);
            }
            else
                throw new ArgumentException("Get Case History requires one argument of type long");
        }

        public async Task<CaseHistory> Info(long id) => await dbSet
                    .Include(h => h.DocumentRegistry)
                    .Include(h => h.Hospital)
                    .Include(h => h.Admissions).ThenInclude(a => a.Department)
                    .Include(h => h.Patient).ThenInclude(h => h.Person)
                    .AsNoTracking().SingleOrDefaultAsync(h => h.Id == id);

        public override async Task<CaseHistory> Post(CaseHistory history)
        {
            db.DocumentRegistries.Add(history.DocumentRegistry);
            await db.SaveChangesAsync();
            dbSet.Update(history);
            await db.SaveChangesAsync();

            return await dbSet
                .Include(h => h.DocumentRegistry)
                .Include(h => h.Hospital)
                .Include(h => h.Admissions).ThenInclude(a => a.Department)
                .AsNoTracking().SingleOrDefaultAsync(h => h.Id == history.Id);
        }
    }
}
