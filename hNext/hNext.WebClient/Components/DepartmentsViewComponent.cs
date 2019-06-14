using hNext.Infrastructure;
using hNext.Model;
using hNext.WebClient.Infrastructure;
using hNext.WebClient.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hNext.WebClient.Components
{
    public class DepartmentsViewComponent:ViewComponent
    {
        public IViewComponentResult Invoke(UniqueList<string> modules)
        {
            modules.Add(nameof(SpecialtiesListViewComponent).ViewComponentName());
            modules.Add(nameof(SpecialtiesSelectorViewComponent).ViewComponentName());
            modules.Add(nameof(PhonesListViewComponent).ViewComponentName());
            modules.Add(nameof(EmailsListViewComponent).ViewComponentName());
            modules.Add(nameof(ConfirmationDialogViewComponent).ViewComponentName());
            return View(new DepartmentsViewModel());
        }
    }
}
