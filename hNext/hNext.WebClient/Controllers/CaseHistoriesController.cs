using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using hNext.IRepository;
using hNext.Model;
using hNext.ResourceLibrary.Resources;
using hNext.WebClient.Components;
using hNext.WebClient.Infrastructure;
using hNext.WebClient.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace hNext.WebClient.Controllers
{
    public class CaseHistoriesController : Controller
    {
        private readonly IStringLocalizer<Resources> _localizer;
        private ICaseHistoryRepository _repository;

        public CaseHistoriesController(IStringLocalizer<Resources> localizer,
                                        ICaseHistoryRepository repository)
        {
            _localizer = localizer;
            _repository = repository;
        }

        [Route("[controller]/{id:long}")]
        [Route("[controller]/[action]/{id?}")]
        public async Task<IActionResult> Index(long? id)
        {
            var history = await _repository.Info(id ?? 0);

            if (history == null)
                return BadRequest();

            CaseHistoryViewModel model = new CaseHistoryViewModel
            {
                CaseHistory = await _repository.Info(id ?? 0)
            };

            model.Tabs.Add(nameof(CaseHistoryGeneralInfoViewComponent).ViewComponentName(), _localizer[nameof(Resources.GeneralInfo)]);
            model.Tabs.Add(nameof(CaseHistoryRecordsViewComponent).ViewComponentName(), _localizer[nameof(Resources.Records)]);

            return View(model);
        }
    }
}