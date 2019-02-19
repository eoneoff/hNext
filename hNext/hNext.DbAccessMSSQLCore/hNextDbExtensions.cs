using hNext.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hNext.DbAccessMSSQLCore
{
    public static class hNextDbExtensions
    {
        public static IQueryable<Patient> SearchPatients(this hNextDbContext db, PatientSearchModel model)
        {
            var name = new SqlParameter("@name", model.Name);
            var year = new SqlParameter("@year", model.YearOfBirth);
            var regionId = new SqlParameter("@regionId", model.RegionId);
            var districtId = new SqlParameter("@districtId", model.DistrictId);
            var cityId = new SqlParameter("@cityId", model.CityId);

            var patients = db.Patients.FromSql("SELECT * FROM SearchPatients(@name, @year, @regionId, @districtId, @cityId)",
                name, year, regionId, districtId, cityId);

            return patients;
        }
    }
}
