using hNext.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace hNext.IRepository
{
    public interface IDrugRepository : IRepository<Drug>
    {
        public Task<IEnumerable<Drug>> Search(string searhLine);
    }
}