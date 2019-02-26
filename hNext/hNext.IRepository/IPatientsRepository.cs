using hNext.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace hNext.IRepository
{
    public interface IPatientsRepository:IRepository<Patient>
    {
        Task<IEnumerable<Patient>> SearchPatients(PatientSearchModel model);
    }
}
