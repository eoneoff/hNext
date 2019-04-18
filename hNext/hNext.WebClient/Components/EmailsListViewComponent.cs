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
        public IViewComponentResult Invoke(List<string> modules)
        {
            if (!modules.Contains(nameof(EmailEditorViewComponent).ViewComponentName()))
            {
                modules.Add(nameof(EmailEditorViewComponent).ViewComponentName());
            }
            if (!modules.Contains(nameof(ConfirmationDialogViewComponent).ViewComponentName()))
            {
                modules.Add(nameof(EmailEditorViewComponent).ViewComponentName());
            }
            return View();
        }
    }
}
