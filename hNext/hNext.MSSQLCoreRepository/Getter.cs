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
        protected hNextDbContext db = new hNextDbContext();
        protected DbSet<T> dbSet;

        public Getter()
        {
            dbSet = db.Set<T>();
        }

        public async Task<IEnumerable<T>> Get()
        {
            return await dbSet.AsNoTracking().ToListAsync();
        }

        public async Task<T> Get(int id)
        {
            return await dbSet.FindAsync(id);
        }
    }
}
