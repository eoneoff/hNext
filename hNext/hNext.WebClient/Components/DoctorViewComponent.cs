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
    public class DoctorViewComponent:ViewComponent
    {
        private IGetter<Specialty> _specialties;

        public DoctorViewComponent(IGetter<Specialty> specialties) => _specialties = specialties;

        public async Task<IViewComponentResult> InvokeAsync(UniqueList<string> modules)
        {
            modules.Add(nameof(PersonEditorViewComponent).ViewComponentName());
            modules.Add(nameof(SpecialtiesListViewComponent).ViewComponentName());
            modules.Add(nameof(DoctorSpecialtyEditorViewComponent).ViewComponentName());
            modules.Add(nameof(DoctorPositionListViewComponent).ViewComponentName());
            modules.Add(nameof(ConfirmationDialogViewComponent).ViewComponentName());

            return View(new DoctorViewModel { Specialties = await _specialties.Get()});
        }
    }
}
