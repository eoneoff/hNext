using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Text;

namespace hNext.Resources
{
    public class Localizer
    {
        private readonly IStringLocalizer<Localizer> _localizer;

        public Localizer(IStringLocalizer<Localizer> localizer)
        {
            _localizer = localizer;
        }

        public string this [string index]
        {
            get
            {
                return _localizer[index];
            }
        }
    }
}
