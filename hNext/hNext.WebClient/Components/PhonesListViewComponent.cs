using hNext.WebClient.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hNext.WebClient.Components
{
    public class PhonesListViewComponent:ViewComponent
    {
        public IViewComponentResult Invoke(List<string> modules)
        {
            if(!modules.Contains(nameof(PhoneEditorViewComponent).ViewComponentName()))
            {
                modules.Add(nameof(PhoneEditorViewComponent).ViewComponentName());
            }
            return View();
        }
    }
}
