using hNext.DbAccessMSSQLCore;
using hNext.IRepository;
using hNext.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hNext.MSSQLCoreRepository
{
    public class DocumentsRepository : Repository<Document>, IDocumentsRepository
    {
        public DocumentsRepository(hNextDbContext db) : base(db) { }

        public override async Task<IEnumerable<Document>> Get() =>
            await dbSet.Include(d => d.DocumentType).AsNoTracking().ToListAsync();

        public override async Task<Document> Get(params object[] key)
        {
            if (key.Count() > 0 && key[0] is long id)
                return await dbSet.Include(d => d.DocumentType).AsNoTracking().SingleOrDefaultAsync(d => d.Id == id);
            else
                throw new ArgumentException("Documents Getter needs argument of type long");
        }
            

        public override async Task<Document> Post(Document document) =>
            await LoadAdditionaInfo(await base.Post(document));

        public override async Task<Document> Put(Document document) =>
            await LoadAdditionaInfo(await base.Put(document));

        public override async Task<Document> Delete(params object[] key) =>
            await LoadAdditionaInfo(await base.Delete(key));

        public async Task<bool> Exists(int documentTypeId, string number) =>
           await dbSet.Where(d => d.DocumentTypeId == documentTypeId && d.Number == number).AsNoTracking().CountAsync() > 0;

        private async Task<Document> LoadAdditionaInfo(Document document)
        {
            document.DocumentType = await db.DocumentTypes.FindAsync(document.DocumentTypeId);
            return document;
        }
    }
}
