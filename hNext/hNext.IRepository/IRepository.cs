using hNext.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace hNext.IRepository
{
    public interface IRepository<T>:IPoster<T>, IEditor<T>
    {
    }
}
