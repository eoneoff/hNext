using hNext.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace hNext.IRepository
{
    public interface IHospitalRepository : IRepository<Hospital>
    {
        Task<bool> Exists(string name);
    }
}
