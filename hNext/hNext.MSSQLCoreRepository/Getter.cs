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
    public class Getter<T> : IGetter<T> where T:class
    {
        protected hNextDbContext db;
        protected DbSet<T> dbSet;

        public Getter(hNextDbContext db)
        {
            this.db = db;
            dbSet = db.Set<T>();
        }

        public virtual async Task<IEnumerable<T>> Get()
        {
            return await dbSet.AsNoTracking().ToListAsync();
        }

        public virtual async Task<T> Get(long id)
        {
            return await dbSet.FindAsync(id);
        }

        public virtual async Task<bool> Exists(long id)
        {
            var result = await dbSet.FindAsync(id);
            if(result != null)
            {
                db.Entry(result).State = EntityState.Detached;
            }

            return result != null;
        }
    }
}
