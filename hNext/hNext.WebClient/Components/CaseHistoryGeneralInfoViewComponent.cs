using hNext.Infrastructure;
using hNext.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hNext.WebClient.Components
{
    public class CaseHistoryGeneralInfoViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(UniqueList<string> modules, CaseHistory history = null)
        {
            return View(history ?? new CaseHistory());
        }
    }
}
