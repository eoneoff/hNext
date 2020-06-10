using hNext.Infrastructure;
using hNext.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hNext.WebClient.Components
{
    public class DrugPrescriptionViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(UniqueList<string> modules)
        {
            return View(new DrugPrescription());
        }
    }
}
