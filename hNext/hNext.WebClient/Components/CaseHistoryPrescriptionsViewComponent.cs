using Microsoft.AspNetCore.Mvc;
using hNext.Infrastructure;

namespace hNext.WebClient.Controllers
{
    public class CaseHistoryPrescriptionsViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(UniqueList<string> modules)
        {
            return View();
        }
    } 
}