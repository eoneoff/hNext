using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using hNext.WebClient.Models;
using R = hNext.Resources;

namespace hNext.WebClient.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ApplicationViewModel model = new ApplicationViewModel();
            model.Tabs.Add("Patients", R.Resources.Patients);
            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
