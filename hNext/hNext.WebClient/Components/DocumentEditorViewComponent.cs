using hNext.IRepository;
using hNext.Model;
using hNext.WebClient.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using hNext.Infrastructure;
using hNext.WebClient.Infrastructure;

namespace hNext.WebClient.Components
{
    public class DocumentEditorViewComponent:ViewComponent
    {
        private IRepository<DocumentType> _repository;

        public DocumentEditorViewComponent(IRepository<DocumentType> repository) => _repository = repository;

        public async Task<IViewComponentResult> InvokeAsync(UniqueList<string> modules)
        {
            modules.Add(nameof(ConfirmationDialogViewComponent).ViewComponentName());

            return View(new DocumentEditorModel
            {
                DocumentTypes = await _repository.Get()
            });
        }
    }
}
