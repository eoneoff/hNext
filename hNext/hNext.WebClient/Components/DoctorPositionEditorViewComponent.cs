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
    public class DoctorPositionEditorViewComponent : ViewComponent
    {
        private IGetter<Specialty> _specialties;
        private IGetter<Position> _positions;

        public DoctorPositionEditorViewComponent(IGetter<Specialty> specialties, IGetter<Position> positions)
        {
            _specialties = specialties;
            _positions = positions;
        }

        public async Task<IViewComponentResult> InvokeAsync(UniqueList<string> modules)
        {
            modules.Add(nameof(ConfirmationDialogViewComponent).ViewComponentName());

            return View(new DoctorPositionEditorViewModel
            {
               Specialties = await _specialties.Get(),
               Positions = await _positions.Get()
            });
        }
    }
}
