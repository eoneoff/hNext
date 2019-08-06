using hNext.WebClient.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hNext.WebClient.Components
{
    public class CaseHistoryMainViewViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(CaseHistoryViewModel model)
        {
            return View(model);
        }
    }
}
