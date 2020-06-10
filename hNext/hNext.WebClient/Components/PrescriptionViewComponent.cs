using hNext.Infrastructure;
using hNext.Model;
using hNext.WebClient.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace hNext.WebClient.Components
{
    public class PrescriptionViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(UniqueList<string> modules)
        {
            modules.Add(nameof(DrugPrescriptionViewComponent).ViewComponentName());

            return View(new Prescription());
        }
    }
}