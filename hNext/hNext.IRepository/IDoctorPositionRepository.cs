using hNext.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace hNext.IRepository
{
    public interface IDoctorPositionRepository: IRepository<DoctorPosition>
    {
        Task<DoctorPosition> Exists(DoctorPosition position);
    }
}
