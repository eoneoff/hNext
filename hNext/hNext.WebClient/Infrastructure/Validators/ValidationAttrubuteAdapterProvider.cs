using hNext.Infrastructure.Attributes;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace hNext.WebClient.Infrastructure.Validators
{
    public class ValidationAttrubuteAdapterProvider : IValidationAttributeAdapterProvider
    {
        IValidationAttributeAdapterProvider baseProvider = new Microsoft.AspNetCore.Mvc.DataAnnotations.ValidationAttributeAdapterProvider();
        public IAttributeAdapter GetAttributeAdapter(ValidationAttribute attribute, IStringLocalizer stringLocalizer)
        {
            switch(attribute)
            {
                case PastAttribute past:
                    return new PastAttributeAdapter(past, stringLocalizer);
                default:
                    return baseProvider.GetAttributeAdapter(attribute, stringLocalizer);
            }
        }
    }
}
