using hNext.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace hNext.IRepository
{
    public interface IDoctorSpecialtyRepository : IRepository<DoctorSpecialty>
    {
        Task<DoctorSpecialty> Exists(long doctorId, int specialtyId);
    }
}
