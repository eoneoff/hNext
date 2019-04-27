using hNext.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace hNext.IRepository
{
    public interface IGetter<T>
    {
        Task<IEnumerable<T>> Get();
        Task<T> Get(params object[] key);
        Task<bool> Exists(params object[] key);
    }
}
