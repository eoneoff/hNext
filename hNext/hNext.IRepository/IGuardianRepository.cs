using hNext.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace hNext.IRepository
{
    public interface IGuardianRepository:IRepository<GuardianWard>
    {
        Task<GuardianWard> Exists(GuardianWard guardian);
    }
}
