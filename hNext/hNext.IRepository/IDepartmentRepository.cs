using hNext.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace hNext.IRepository
{
    public interface IDepartmentRepository:IRepository<Department>
    {
        Task<Department> Exists(Department department);
    }
}
