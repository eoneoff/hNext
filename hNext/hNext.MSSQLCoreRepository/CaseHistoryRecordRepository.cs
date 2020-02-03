using hNext.DbAccessMSSQLCore;
using hNext.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace hNext.MSSQLCoreRepository
{
    public class CaseHistoryRecordRepository : Repository<CaseHistoryRecord>
    {
        public CaseHistoryRecordRepository(hNextDbContext db) : base(db)
        {
        }

        public override async Task<CaseHistoryRecord> Post(CaseHistoryRecord record)
        {
            record.DocumentRegistry = new DocumentRegistry
            {
                CreationDate = record.Date
            };
            await db.DocumentRegistries.AddAsync(record.DocumentRegistry);
            await db.SaveChangesAsync();
            await dbSet.AddAsync(RemoveTemplates(record));
            await db.SaveChangesAsync();
            return await dbSet
                .Include(r => r.RecordTemplate)
                .Include(r => r.RecordFields).ThenInclude(f => f.RecordFieldTemplate)
                .AsNoTracking().SingleOrDefaultAsync(r => r.Id == record.Id);
        }

        public override async Task<CaseHistoryRecord> Put(CaseHistoryRecord record)
        {
            dbSet.Update(RemoveTemplates(record));
            await db.SaveChangesAsync();

            return await dbSet
                .Include(r => r.RecordTemplate)
                .Include(r => r.RecordFields).ThenInclude(f => f.RecordFieldTemplate)
                .AsNoTracking().SingleOrDefaultAsync(r => r.Id == record.Id);
        }

        private CaseHistoryRecord RemoveTemplates (CaseHistoryRecord record)
        {
            if (record.RecordTemplate != null)
            {
                record.RecordTemplateId = record.RecordTemplate.Id;
                record.RecordTemplate = null;
                foreach (var field in record.RecordFields)
                {
                    field.RecordFieldTemplateId = field.RecordFieldTemplate?.Id;
                    field.RecordFieldTemplate = null;
                }
            }

            return record;
        }
    }
}
