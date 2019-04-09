using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using hNext.Model;

namespace hNext.IRepository
{
    public interface IEditor<T>
    {
        Task<T> Put(T item);
        Task<T> Delete(params object[] key);
    }
}
