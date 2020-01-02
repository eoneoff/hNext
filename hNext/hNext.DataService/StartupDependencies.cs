using hNext.DbAccessMSSQLCore;
using hNext.IRepository;
using hNext.Model;
using hNext.MSSQLCoreRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hNext.DataService
{
    public partial class Startup
    {
        private void AddDependencies(IServiceCollection services)
        {
            services.AddDbContext<hNextDbContext>(options =>
                options.UseSqlServer(Configuration["ConnectionsStrings:hNextDbConnectionString"],
                sqlServerOptions => sqlServerOptions.CommandTimeout(180)));

            services.AddScoped(typeof(IGetter<>), typeof(Getter<>));
            services.AddScoped(typeof(IPoster<>), typeof(Poster<>));
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            services.AddScoped<IPatientsRepository, PatientsRepository>();
            services.AddScoped<IPersonRepository, PersonRepository>();

            services.AddScoped<IPhoneRepository, PhoneRepository>();
            services.AddScoped<IEmailRepository, EmailRepository>();
            services.AddScoped<IDocumentsRepository, DocumentsRepository>();
            services.AddScoped<IGuardianRepository, GuardianRepository>();

            services.AddScoped<IAddressRepository, AddressRepository>();
            services.AddScoped<ICountryRepository, CountryRepository>();
            services.AddScoped<IRegionsRepository, RegionsRepository>();
            services.AddScoped<IDistrictsRepository, DistrictsRepository>();
            services.AddScoped<ICityRepository, CityRepository>();

            services.AddScoped<IHospitalRepository, HospitalRepository>();
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            services.AddScoped<IRepository<DepartmentSpecialty>, DepartmentSpecialtyRepository>();

            services.AddScoped<IDoctorRepository, DoctorRepository>();
            services.AddScoped<IDoctorSpecialtyRepository, DoctorSpecialtyRepository>();
            services.AddScoped<IDoctorPositionRepository, DoctorPositionRepository>();

            services.AddScoped<IPoster<RecordTemplate>, RecordTemplateRepository>();

            services.AddScoped<ICaseHistoryRepository, CaseHistoryRepository>();
            services.AddScoped<IRepository<CaseHistoryDiagnosys>, CaseHistoryDiagnosysRepository>();
            services.AddScoped<IRepository<PatientDiagnosys>, PatientDiagnosysRepository>();
            services.AddScoped<IPoster<Diagnosys>, DiagnosesRepository>();
            services.AddScoped<IICDRepository, ICDRepository>();
            services.AddScoped<IRepository<CaseHistoryAdmission>, CaseHistoryAdmissionRepository>();
        }
    }
}
