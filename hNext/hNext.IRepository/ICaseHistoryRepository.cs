using hNext.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace hNext.IRepository
{
    public interface ICaseHistoryRepository:IRepository<CaseHistory>
    {
        Task<CaseHistory> Info(long id);
        Task<bool> AdmissionExists(long id, long admissionId);
    }
}
