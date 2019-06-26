using hNext.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace hNext.IRepository
{
    public interface IDoctorRepository:IRepository<Doctor>
    {
        Task<IEnumerable<Doctor>> SearchDoctors(DoctorSearchModel model);
        Task<IEnumerable<Specialty>> Specialties(long id);
    }
}
