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
    public class HospitalsViewComponent:ViewComponent
    {
        ICountryRepository _countries;
        IRepository<HospitalType> _hospitalTypes;
        IRepository<PropertyType> _propertyTypes;

        public HospitalsViewComponent(ICountryRepository countries,
            IRepository<HospitalType> hospitalTypes, IRepository<PropertyType> propertyTypes)
        {
            _countries = countries;
            _hospitalTypes = hospitalTypes;
            _propertyTypes = propertyTypes;
        }

        public async Task<IViewComponentResult> InvokeAsync(UniqueList<string> modules)
        {
            modules.Add(nameof(PhonesListViewComponent).ViewComponentName());
            modules.Add(nameof(EmailsListViewComponent).ViewComponentName());
            modules.Add(nameof(ConfirmationDialogViewComponent).ViewComponentName());

            return View(new HospitalsViewModel
            {
                Countries = await _countries.Get(),
                HospitalTypes = await _hospitalTypes.Get(),
                PropertyTypes = await _propertyTypes.Get()
            });
        }
    }
}
