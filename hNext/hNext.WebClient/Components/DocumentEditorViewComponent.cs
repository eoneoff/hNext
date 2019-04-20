using hNext.IRepository;
using hNext.Model;
using hNext.WebClient.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hNext.WebClient.Components
{
    public class DocumentEditorViewComponent:ViewComponent
    {
        private IRepository<DocumentType> _repository;

        public DocumentEditorViewComponent(IRepository<DocumentType> repository) => _repository = repository;

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(new DocumentEditorModel
            {
                DocumentTypes = await _repository.Get()
            });
        }
    }
}
