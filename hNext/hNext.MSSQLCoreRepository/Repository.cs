using hNext.DbAccessMSSQLCore;
using hNext.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace hNext.MSSQLCoreRepository
{
    public class Repository<T> : Poster<T>, IRepository<T> where T : class
    {
        public Repository(hNextDbContext db) : base(db) { }

        public virtual async Task<T> Put(T item)
        {
            dbSet.Add(item);
            await db.SaveChangesAsync();
            return item;
        }

        public virtual async Task<T> Delete(params object[] key)
        {
            T item = await dbSet.FindAsync(key);
            if (item != null)
            {
                dbSet.Remove(item);
                await db.SaveChangesAsync();
            }
            return item;
        }
    }
}
