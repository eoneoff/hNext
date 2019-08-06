using hNext.Infrastructure;
using hNext.Model;
using hNext.WebClient.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hNext.WebClient.Components
{
    public class PatientDiagnosysEditorViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(UniqueList<string> modules)
        {
            modules.Add(nameof(ConfirmationDialogViewComponent).ViewComponentName());
            modules.Add(nameof(DiagnosysEditorViewComponent).ViewComponentName());

            return View(new PatientDiagnosys());
        }
    }
}
