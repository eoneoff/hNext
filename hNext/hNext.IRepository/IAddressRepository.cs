using hNext.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace hNext.IRepository
{
    public interface IAddressRepository : IRepository<Address>
    {
        Task<Address> Exists(Address address);
    }
}
