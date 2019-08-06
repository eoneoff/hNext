using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using hNext.WebClient.Models;
using hNext.ResourceLibrary.Resources;
using hNext.WebClient.Components;
using Microsoft.Extensions.Localization;
using hNext.WebClient.Infrastructure;
using hNext.IRepository;

namespace hNext.WebClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly IStringLocalizer<Resources> _localizer;

        public HomeController(IStringLocalizer<Resources> localizer)
        {
            _localizer = localizer;
        }

        public IActionResult Index()
        {
            ApplicationViewModel model = new ApplicationViewModel();
            model.Tabs.Add(nameof(PatientSearchViewComponent).ViewComponentName(), _localizer[nameof(Resources.Patients)]);
            model.Tabs.Add(nameof(HospitalsViewComponent).ViewComponentName(), _localizer[nameof(Resources.Hospitals)]);
            model.Tabs.Add(nameof(DepartmentsViewComponent).ViewComponentName(), _localizer[nameof(Resources.Departments)]);
            model.Tabs.Add(nameof(DoctorViewComponent).ViewComponentName(), _localizer[nameof(Resources.Doctors)]);
            model.SideBar.Add(nameof(PatientDetailsViewComponent).ViewComponentName(), _localizer[nameof(Resources.Patient)]);
            model.SideBar.Add(nameof(PatientAdditionalDataViewComponent).ViewComponentName(),
                _localizer[nameof(Resources.PatientAdditionalData)]);
            model.SideBar.Add(nameof(CaseHistoriesListViewComponent).ViewComponentName(), _localizer[nameof(Resources.CaseHistories)]);

            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
