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
    public class CaseHistoryRepository : Repository<CaseHistory>, ICaseHistoryRepository
    {
        public CaseHistoryRepository(hNextDbContext db) : base(db)
        {
        }

        public override async Task<CaseHistory> Get(params object[] keys)
        {
            if (keys[0] is long id)
            {
                return await dbSet
                    .Include(h => h.DocumentRegistry)
                    .Include(h => h.Hospital).ThenInclude(h => h.Departments)
                    .Include(h => h.Admissions).ThenInclude(a => a.Department)
                    .Include(h => h.Patient).ThenInclude(h => h.Person)
                    .Include(h => h.Patient).ThenInclude(h => h.Diagnoses)
                    .ThenInclude(d => d.Diagnosys).ThenInclude(d => d.ICD)
                    .Include(h => h.Diagnoses).ThenInclude(d => d.Diagnosys).ThenInclude(d => d.ICD)
                    .Include(h => h.Admissions).ThenInclude(a => a.Department)
                    .Include(r => r.Records).ThenInclude(r => r.Diagnoses)
                    .Include(h => h.Records).ThenInclude(r => r.RecordFields)
                    .Include(h => h.Records).ThenInclude(r => r.RecordTemplate)
                    .Include(h => h.Records).ThenInclude(r => r.RecordFields).ThenInclude(f => f.RecordFieldTemplate)
                    .Include(h => h.Consultations).ThenInclude(r => r.RecordTemplate)
                    .Include(h => h.Consultations).ThenInclude(c => c.RecordFields).ThenInclude(f => f.RecordFieldTemplate)
                    .Include(h => h.Consultations).ThenInclude(r => r.RecordTemplate).
                    Include(h => h.Prescriptions).ThenInclude(p => p.Prescription)
                    .AsNoTracking().SingleOrDefaultAsync(h => h.Id == id);
            }
            else
                throw new ArgumentException("Get Case History requires one argument of type long");
        }

        public async Task<CaseHistory> Info(long id) => await dbSet
                    .Include(h => h.DocumentRegistry)
                    .Include(h => h.Hospital)
                    .Include(h => h.Admissions).ThenInclude(a => a.Department)
                    .Include(h => h.Patient).ThenInclude(h => h.Person)
                    .AsNoTracking().SingleOrDefaultAsync(h => h.Id == id);

        public override async Task<CaseHistory> Post(CaseHistory history)
        {
            db.DocumentRegistries.Add(history.DocumentRegistry);
            await db.SaveChangesAsync();
            dbSet.Update(history);
            await db.SaveChangesAsync();

            return await dbSet
                .Include(h => h.DocumentRegistry)
                .Include(h => h.Hospital)
                .Include(h => h.Admissions).ThenInclude(a => a.Department)
                .AsNoTracking().SingleOrDefaultAsync(h => h.Id == history.Id);
        }

        public override async Task<CaseHistory> Put(CaseHistory history, params object[] key)
        {
            dbSet.Update(history);
            await db.SaveChangesAsync();
            return await dbSet
                .Include(h => h.DocumentRegistry)
                .Include(h => h.Hospital)
                .Include(h => h.Admissions).ThenInclude(a => a.Department)
                .AsNoTracking().SingleOrDefaultAsync(h => h.Id == history.Id);
        }

        public async Task<bool> AdmissionExists(long id, long admissionId) =>
            await db.CaseHistoryAdmissions.AnyAsync(a => a.Id == admissionId && a.CaseHistoryId == id);

        public async Task<bool> RecordExists(long id, long recordId) =>
            await db.CaseHistoryRecords.AnyAsync(r => r.Id == recordId && r.CaseHistoryId == id);
    }
}
