using hNext.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace hNext.DbAccessMSSQLCore
{
    public partial class hNextDbContext
    {
        public virtual IQueryable<Patient> SearchPatients(PatientSearchModel model)
        {
            var name = new SqlParameter("@name", (object)model.Name ?? DBNull.Value);
            var year = new SqlParameter("@year", (object)model.YearOfBirth ?? DBNull.Value);
            var regionId = new SqlParameter("@regionId", (object)model.RegionId ?? DBNull.Value);
            var districtId = new SqlParameter("@districtId", (object)model.DistrictId ?? DBNull.Value);
            var cityId = new SqlParameter("@cityId", (object)model.CityId ?? DBNull.Value);

            var patients = Patients.FromSql("SELECT * FROM SearchPatients(@name, @year, @regionId, @districtId, @cityId)",
                name, year, regionId, districtId, cityId);

            return patients;
        }
    }
}
