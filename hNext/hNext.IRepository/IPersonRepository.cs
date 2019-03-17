using hNext.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace hNext.IRepository
{
    public interface IPersonRepository:IRepository<Person>
    {
        Task<Person> Exists(Person person);
    }
}
