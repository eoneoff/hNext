using hNext.Infrastructure;
using hNext.IRepository;
using hNext.Model;
using hNext.WebClient.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hNext.WebClient.Components
{
    public class CaseHistoryRecordsViewComponent : ViewComponent
    {
        private IRepository<RecordTemplate> _repository;

        public CaseHistoryRecordsViewComponent (IRepository<RecordTemplate> repository) => _repository = repository;

        public async Task<IViewComponentResult> InvokeAsync(UniqueList<string> modules)
        {
            modules.Add(nameof(RecordViewComponent).ViewComponentName());
            modules.Add(nameof(CaseHistoryDiagnosysEditorViewComponent).ViewComponentName());

            return View(await _repository.Get());
        }
    }
}
