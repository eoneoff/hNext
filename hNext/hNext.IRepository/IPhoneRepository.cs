using hNext.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace hNext.IRepository
{
    public interface IPhoneRepository : IRepository<Phone>
    {
        Task<Phone> Exists(string number);

        Task<bool> BelongToOthers(long id);
    }
}
