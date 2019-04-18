using hNext.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace hNext.IRepository
{
    public interface IEmailRepository : IRepository<Email>
    {
        Task<Email> Exists(string address);

        Task<bool> BelongToOthers(long id);
    }
}
