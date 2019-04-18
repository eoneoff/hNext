﻿using hNext.WebClient.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hNext.WebClient.Components
{
    public class EmailEditorViewComponent:ViewComponent
    {
        public IViewComponentResult Invoke(List<string> modules)
        {
            if (!modules.Contains(nameof(ConfirmationDialogViewComponent).ViewComponentName()))
            {
                modules.Add(nameof(ConfirmationDialogViewComponent).ViewComponentName());
            }
            return View(new Model.Email());
        }
    }
}
