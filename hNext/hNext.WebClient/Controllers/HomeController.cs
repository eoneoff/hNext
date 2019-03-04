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
            model.Tabs.Add(nameof(PatientSearchViewComponent).VewComponentName(), _localizer[nameof(Resources.Patients)]);
            model.Modals.Add(nameof(PatientEditorViewComponent).VewComponentName(), "");
            model.SideBar.Add(nameof(PatientDetailsViewComponent).VewComponentName(), _localizer[nameof(Resources.Patient)]);
            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
