using hNext.Infrastructure;
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
        public IViewComponentResult Invoke(UniqueList<string> modules)
        {
            modules.Add(nameof(PhonesListViewComponent).ViewComponentName());
            modules.Add(nameof(EmailsListViewComponent).ViewComponentName());
            modules.Add(nameof(DocumentsListViewComponent).ViewComponentName());
            modules.Add(nameof(GuardiansListViewComponent).ViewComponentName());
            
            return View();
        }
    }
}
