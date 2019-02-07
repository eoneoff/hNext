using hNext.DbAccessMSSQLCore;
using hNext.IRepository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace hNext.MSSQLCoreRepository
{
    public class Poster<T>:Getter<T>, IPoster<T> where T:class
    {
        public Poster(hNextDbContext db) : base(db) { }

        public virtual async Task<T> Post(T item)
        {
            dbSet.Add(item);
            await db.SaveChangesAsync();
            return item;
        }
    }
}
