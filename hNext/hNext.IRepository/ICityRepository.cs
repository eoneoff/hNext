using hNext.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace hNext.IRepository
{
    public interface ICityRepository:IRepository<City>
    {
        Task<IEnumerable<Street>> GetStreets(int id);
    }
}
