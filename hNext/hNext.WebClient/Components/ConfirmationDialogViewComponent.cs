using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hNext.WebClient.Components
{
    public class ConfirmationDialogViewComponent: ViewComponent
    {
        public IViewComponentResult Invoke(HashSet<string> modules)
        {
            return View();
        }
    }
}
