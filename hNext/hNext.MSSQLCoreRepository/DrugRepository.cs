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
    public class DrugRepository : Repository<Drug>, IDrugRepository
    {
        public DrugRepository(hNextDbContext context):base(context){}

        public async Task<IEnumerable<Drug>> Search(string searchString)
        {
            return await dbSet.Where(d => d.Name.Contains(searchString) || d.InternationalName.Contains(searchString)).AsNoTracking().ToListAsync();
        }
    }
}