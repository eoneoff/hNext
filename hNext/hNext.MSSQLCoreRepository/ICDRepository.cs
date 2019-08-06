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
    public class ICDRepository : Getter<ICD>, IICDRepository
    {
        public ICDRepository(hNextDbContext db) : base(db)
        {
        }

        public async Task<ICD> Search(ICD icd)
        {
            return await dbSet
                .SingleOrDefaultAsync(i => i.Letter == icd.Letter && i.PrimaryNumber == icd.PrimaryNumber
                && i.SecondaryNumber == icd.SecondaryNumber);
        }
    }
}
