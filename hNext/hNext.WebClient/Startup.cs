﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using hNext.DbAccessMSSQLCore;
using hNext.IRepository;
using hNext.MSSQLCoreRepository;
using hNext.WebClient.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace hNext.WebClient
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLocalization();
            services.Configure<RequestLocalizationOptions>(options =>
            {
                CultureInfo[] supportedCultures = new[]
                {
                    new CultureInfo("uk")
                };
                options.DefaultRequestCulture = new RequestCulture("uk");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });
            services.AddDbContext<hNextDbContext>(options =>
                options.UseSqlServer(Configuration["ConnectionsStrings:hNextDbConnectionString"]));
            services.AddScoped(typeof(IGetter<>), typeof(Getter<>));
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<ICountryRepository, CountryRepository>();
            services.AddScoped<ICaseHistoryRepository, CaseHistoryRepository>();
            services.AddSingleton<IValidationAttributeAdapterProvider, Infrastructure.Validators.ValidationAttrubuteAdapterProvider>();
            //services.Configure<CookiePolicyOptions>(options =>
            //{
            //    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
            //    options.CheckConsentNeeded = context => true;
            //    options.MinimumSameSitePolicy = SameSiteMode.None;
            //});


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
               .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix).AddDataAnnotationsLocalization(options => 
                options.DataAnnotationLocalizerProvider = (type, factory) => 
                factory.Create(typeof(ResourceLibrary.Resources.Resources)));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseRequestLocalization();

            app.UseRouting();

            app.UseEndpoints(endpoints => 
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}")
            );
        }
    }
}
