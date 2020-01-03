using hNext.Model;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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

            var patients = Patients.FromSqlRaw("SELECT * FROM SearchPatients(@name, @year, @regionId, @districtId, @cityId)",
                name, year, regionId, districtId, cityId);

            return patients;
        }

        public virtual IQueryable<Doctor> SearchDoctor(DoctorSearchModel model)
        {
            var name = new SqlParameter("@name", (object)model.Name ?? DBNull.Value);
            var specialtyId = new SqlParameter("@specialtyId", (object)model.SpecialtyId ?? DBNull.Value);
            var hospitalId = new SqlParameter("@hospitalId", (object)model.HospitalId ?? DBNull.Value);
            var departmentId = new SqlParameter("@departmentId", (object)model.DepartmentId ?? DBNull.Value);

            var doctors = Doctors.FromSqlRaw("SELECT * FROM SearchDoctors(@name, @specialtyId, @hospitalId, @departmentId)",
                name, specialtyId, hospitalId, departmentId);

            return doctors;
        }
    }
}
