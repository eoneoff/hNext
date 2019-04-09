using hNext.WebClient.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hNext.WebClient.Components
{
    public class PatientAdditionalDataViewComponent:ViewComponent
    {
        public IViewComponentResult Invoke(List<string> modules)
        {
            if (!modules.Contains(nameof(PhonesListViewComponent).ViewComponentName()))
            {
                modules.Add(nameof(PhonesListViewComponent).ViewComponentName()); 
            }
            return View();
        }
    }
}
