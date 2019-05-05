using hNext.DbAccessMSSQLCore;
using hNext.IRepository;
using hNext.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hNext.MSSQLCoreRepository
{
    public class PhoneRepository : Repository<Phone>, IPhoneRepository
    {
        public PhoneRepository(hNextDbContext db) : base(db) { }

        public async Task<bool> BelongToOthers(long id)
        {
            return await db.PersonPhones.Where(pp => pp.PhoneId == id).CountAsync() > 0;
        }

        public async Task<Phone> Exists(string number)
        {
            return await dbSet.SingleOrDefaultAsync(p => p.Number == number);
        }
    }
}
