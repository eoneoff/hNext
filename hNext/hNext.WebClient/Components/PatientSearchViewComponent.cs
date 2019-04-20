using hNext.WebClient.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using hNext.Model;
using hNext.IRepository;
using hNext.WebClient.Infrastructure;
using hNext.Infrastructure;

namespace hNext.WebClient.Components
{
    public class PatientSearchViewComponent : ViewComponent
    {
        IRepository<Region> _repository;

        public PatientSearchViewComponent(IRepository<Region> repository)
        {
            _repository = repository;
        }

        public async  Task<IViewComponentResult> InvokeAsync(UniqueList<string> modules)
        {
            modules.Add(nameof(PersonEditorViewComponent).ViewComponentName());

            return View(new PatientSearchViewModel
            {
                Regions = await _repository.Get()
            });
        }
    }
}
