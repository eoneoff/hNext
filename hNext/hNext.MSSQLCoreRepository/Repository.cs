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
            db.Entry(item).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return item;
        }

        public virtual async Task<T> Delete(int id)
        {
            T item = await dbSet.FindAsync(id);
            if (item != null)
            {
                dbSet.Remove(item);
                await db.SaveChangesAsync();
            }
            return item;
        }
    }
}
