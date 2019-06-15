﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using hNext.DbAccessMSSQLCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using hNext.IRepository;
using hNext.MSSQLCoreRepository;
using Microsoft.AspNetCore.Http.Features;

namespace hNext.DataService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<hNextDbContext>(options =>
                options.UseSqlServer(Configuration["ConnectionsStrings:hNextDbConnectionString"],
                sqlServerOptions => sqlServerOptions.CommandTimeout(180)));

            services.AddScoped(typeof(IGetter<>), typeof(Getter<>));
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
            services.AddScoped<IDepartmentSpecialtyRepository, DepartmentSpecialtyRepository>();

            services.AddScoped<IDoctorRepository, DoctorRepository>();
            services.AddScoped<IDoctorSpecialtyRepository, DoctorSpecialtyRepository>();
            services.AddScoped<IDoctorPositionRepository, DoctorPositionRepository>();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                {
                    builder.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
                });
            });

            services.AddMvc().AddJsonOptions(options => options.SerializerSettings.ReferenceLoopHandling =
                Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.AddMemoryCache();
            services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("AllowAll");
            app.UseStatusCodePages();
            app.UseStaticFiles();
            app.UseSession();
            app.UseMvcWithDefaultRoute();
        }
    }
}
