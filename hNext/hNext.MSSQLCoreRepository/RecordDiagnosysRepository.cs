using hNext.Model;
using hNext.DbAccessMSSQLCore;
using System.Threading.Tasks;

namespace hNext.MSSQLCoreRepository
{
    public class RecordDiagnosysRepository : Repository<RecordDiagnosys>
    {
        public RecordDiagnosysRepository(hNextDbContext db) : base(db){}

        public override async Task<RecordDiagnosys> Post(RecordDiagnosys diagnosys)
        {
            dbSet.Add(diagnosys);
            await db.SaveChangesAsync();
            return diagnosys;
        }
    }
}