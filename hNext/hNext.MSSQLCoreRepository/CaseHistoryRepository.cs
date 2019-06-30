using hNext.DbAccessMSSQLCore;
using hNext.IRepository;
using hNext.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace hNext.MSSQLCoreRepository
{
    public class CaseHistoryRepository : Repository<CaseHistory>, ICaseHistoryRepository
    {
        public CaseHistoryRepository(hNextDbContext db) : base(db)
        {
        }
    }
}
