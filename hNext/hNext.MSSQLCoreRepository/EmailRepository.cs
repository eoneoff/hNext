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
    public class EmailRepository:Repository<Email>, IEmailRepository 
    {
        public EmailRepository(hNextDbContext db) : base(db) { }

        public async Task<bool> BelongToOthers(long id)
        {
            return await db.PersonEmails.Where(pe => pe.PersonId == id).CountAsync() > 1;
        }

        public async Task<Email> Exists(string address)
        {
            return await dbSet.SingleOrDefaultAsync(e => e.Address == address);
        }
    }
}
