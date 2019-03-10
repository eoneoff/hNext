using hNext.Model;
using hNext.WebClient.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using hNext.IRepository;
using hNext.WebClient.Infrastructure;

namespace hNext.WebClient.Components
{
    public class PersonEditorViewComponent : ViewComponent
    {
        private ICountryRepository _countryRepository;
        private IRepository<Gender> _genderRepository;
        private IRepository<CityType> _cityTypeRepository;
        private IRepository<StreetType> _streetTypeRepository;

        public PersonEditorViewComponent(ICountryRepository countryRepository,
                                        IRepository<Gender> genderRepository,
                                        IRepository<StreetType> streetTypeRepository,
                                        IRepository<CityType> cityTypeRepository)
        {
            _countryRepository = countryRepository;
            _genderRepository = genderRepository;
            _cityTypeRepository = cityTypeRepository;
            _streetTypeRepository = streetTypeRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync(HashSet<string> modules)
        {
            modules.Add(nameof(ConfirmationDialogViewComponent).ViewComponentName());
            PersonEditorViewModel model = new PersonEditorViewModel
            {
                Genders = await _genderRepository.Get(),
                Countries = await _countryRepository.Get(),
                CityTypes = await _cityTypeRepository.Get(),
                StreetTypes = await _streetTypeRepository.Get()
            };
            return View(model);
        }
    }
}
