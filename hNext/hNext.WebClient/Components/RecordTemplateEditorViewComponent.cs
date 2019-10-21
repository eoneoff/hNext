using hNext.Infrastructure;
using hNext.IRepository;
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
    public class RecordTemplateEditorViewComponent : ViewComponent
    {
        private IGetter<Hospital> _getter;

        public RecordTemplateEditorViewComponent(IGetter<Hospital> getter) => _getter = getter;

        public async Task<IViewComponentResult> InvokeAsync(UniqueList<string> modules)
        {
            modules.Add(nameof(RecordFieldTemplateEditorViewComponent).ViewComponentName());
            modules.Add(nameof(ConfirmationDialogViewComponent).ViewComponentName());

            var model = new RecordTemplateEditorViewModel
            {
                Hospitals = await _getter.Get()
            };

            return View(model);
        }
    }
}
