using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Components.Authorization;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Builder;
using hNext.IRepository;
using hNext.WebApiRepository;
using hNext.Model;

namespace hNext.WebClientBlazor
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");
            builder.Services.AddAuthorizationCore();
            builder.Services.AddLocalization();
            var supportedCultures = new List<CultureInfo>
            {
                new CultureInfo("uk")
            };
            builder.Services.Configure<RequestLocalizationOptions>(options =>
            {
                options.DefaultRequestCulture = new RequestCulture("uk");
                options.SupportedUICultures = supportedCultures;
                options.SupportedCultures = supportedCultures;
            });

            builder.Services.AddScoped<AuthenticationStateProvider, ApiAuthenticationStateProvider>();
            builder.Services.AddSingleton(sp =>
            {
                var configuration = sp.GetRequiredService<IConfiguration>();
                return new HttpClient { BaseAddress = new Uri(configuration["ApiServer"]) };
             });
            builder.Services.AddSingleton<ViewModels.AppStateViewModel>();
            builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            builder.Services.AddScoped<IRepository<Patient>, PatientsRepository>();
            builder.Services.AddScoped<IRepository<Country>, CountryRepository>();
            builder.Services.AddScoped<IRepository<Region>, RegionsRepository>();

            await builder.Build().RunAsync();
        }
    }
}
