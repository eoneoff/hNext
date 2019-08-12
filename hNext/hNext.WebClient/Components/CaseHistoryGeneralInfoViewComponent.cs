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
    public class CaseHistoryGeneralInfoViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(UniqueList<string> modules, CaseHistory history = null)
        {
            modules.Add(nameof(ConfirmationDialogViewComponent).ViewComponentName());
            modules.Add(nameof(CaseHistoryDiagnosysEditorViewComponent).ViewComponentName());
            modules.Add(nameof(CaseHistoryAdmissionEditorViewComponent).ViewComponentName());

            return View(history ?? new CaseHistory());
        }
    }
}
