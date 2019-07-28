using hNext.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hNext.WebClient.Components
{
    public class ICDReferenceViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(UniqueList<string> modules)
        {
            return View();
        }
    }
}
