using hNext.DbAccessMSSQLCore;
using hNext.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace hNext.MSSQLCoreRepository
{
    public class PatientDiagnosysRepository : Repository<PatientDiagnosys>
    {
        public PatientDiagnosysRepository(hNextDbContext db) : base(db)
        {
        }

        public override async Task<PatientDiagnosys> Post(PatientDiagnosys patient)
        {
            if(patient.Diagnosys != null && patient.Diagnosys.Id == 0)
            {
                patient.Diagnosys.ICD = null;
                dbSet.Update(patient);
            }
            else
            {
                patient.Diagnosys = null;
                dbSet.Add(patient);
            }
            await db.SaveChangesAsync();
            return await dbSet.
                Include(p => p.Diagnosys).ThenInclude(d => d.ICD)
                .AsNoTracking().SingleOrDefaultAsync(h => h.PatientId == patient.PatientId
                    && h.DiagnosysId == patient.DiagnosysId);
        }

        public override async Task<PatientDiagnosys> Delete(params object[] key)
        {
            if (key[0] is long patientId && key[1] is long diagnosysId)
            {
                if (await dbSet.FindAsync(patientId, diagnosysId) is PatientDiagnosys diagnosys)
                {
                    diagnosys.Active = false;
                    dbSet.Update(diagnosys);
                    return diagnosys;
                }
                else
                    return null;
            }
            else
                throw new ArgumentException("PatientDiagnosys delete requires two arguments of type long");
        }
    }
}
