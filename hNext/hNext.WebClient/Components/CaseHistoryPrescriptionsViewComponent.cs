using Microsoft.AspNetCore.Mvc;
using hNext.Infrastructure;
using hNext.WebClient.Infrastructure;

namespace hNext.WebClient.Components
{
    public class CaseHistoryPrescriptionsViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(UniqueList<string> modules)
        {
            modules.Add(nameof(PrescriptionViewComponent).ViewComponentName());
            return View();
        }
    } 
}