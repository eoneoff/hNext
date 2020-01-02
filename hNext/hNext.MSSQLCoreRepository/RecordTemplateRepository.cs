using hNext.DbAccessMSSQLCore;
using hNext.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace hNext.MSSQLCoreRepository
{
    public class RecordTemplateRepository : Repository<RecordTemplate>
    {
        public RecordTemplateRepository(hNextDbContext db) : base(db) {}

        public override async Task<IEnumerable<RecordTemplate>> Get()
        {
            return await dbSet
                .Include(t => t.Hospital)
                .Include(t => t.Department)
                .Include(t => t.Specialty)
                .Include(t => t.Doctor)
                .AsNoTracking().ToListAsync();
        }

        public override async Task<RecordTemplate> Get(params object[] keys)
        {
            if(keys[0] is int id)
            {
                return await dbSet
                .Include(t => t.Hospital)
                .Include(t => t.Department)
                .Include(t => t.Specialty)
                .Include(t => t.Doctor)
                .AsNoTracking().SingleOrDefaultAsync(t => t.Id == id);
            }
            else
                throw new ArgumentException("Get Record Template requires one argument of type int");
        }

        public override async Task<RecordTemplate> Post(RecordTemplate item)
        {
            dbSet.Add(item);
            await db.SaveChangesAsync();
            return await dbSet
                .Include(t => t.Hospital)
                .Include(t => t.Department)
                .Include(t => t.Specialty)
                .Include(t => t.Doctor)
                .AsNoTracking().SingleOrDefaultAsync(t => t.Id == item.Id);
        }
    }
}
