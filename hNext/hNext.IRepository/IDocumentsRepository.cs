using hNext.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace hNext.IRepository
{
    public interface IDocumentsRepository:IRepository<Document>
    {
        Task<bool> Exists(int documentTypeId, string number);
    }
}
