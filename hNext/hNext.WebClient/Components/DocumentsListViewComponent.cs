using hNext.Infrastructure;
using hNext.WebClient.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hNext.WebClient.Components
{
    public class DocumentsListViewComponent:ViewComponent
    {
        public IViewComponentResult Invoke(UniqueList<string> modules)
        {
            modules.Add(nameof(DocumentEditorViewComponent).ViewComponentName());
            modules.Add(nameof(DocumentEditorViewComponent).ViewComponentName());
            
            return View();
        }
    }
}
