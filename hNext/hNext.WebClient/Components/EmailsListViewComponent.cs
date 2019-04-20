using hNext.Infrastructure;
using hNext.WebClient.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hNext.WebClient.Components
{
    public class EmailsListViewComponent:ViewComponent
    {
        public IViewComponentResult Invoke(UniqueList<string> modules)
        {
            modules.Add(nameof(EmailEditorViewComponent).ViewComponentName());
            modules.Add(nameof(EmailEditorViewComponent).ViewComponentName());
           
            return View();
        }
    }
}
