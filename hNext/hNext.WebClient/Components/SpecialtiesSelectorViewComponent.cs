﻿using hNext.Infrastructure;
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
    public class SpecialtiesSelectorViewComponent : ViewComponent
    {
        private IGetter<Specialty> _repository;

        public SpecialtiesSelectorViewComponent(IGetter<Specialty> repository) => _repository = repository;

        public async Task<IViewComponentResult> InvokeAsync(UniqueList<string> modules)
        {
            modules.Add(nameof(ConfirmationDialogViewComponent).ViewComponentName());

            return View(new SpecialtiesSelectorViewModel { Specialties = await _repository.Get() });
        }
    }
}
