using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hNext.WebClientBlazor.ViewModels
{
    public class InputSelectInt<T>:InputSelect<T>
    {
        protected override bool TryParseValueFromString(string value, out T result, out string validationErrorMessage)
        {
            if(typeof(T) == typeof(int) || typeof(T) == typeof(int?))
            {
                if(int.TryParse(value, out var number))
                {
                    result = (T)(object)number;
                    validationErrorMessage = null;
                    return true;
                }
                else
                {
                    result = default;
                    if(value == null)
                    {
                        validationErrorMessage = null;
                        return true;
                    }
                    else
                    {
                        validationErrorMessage = "The value it not a valid number";
                        return false;
                    }
                }
            }
            else
            {
                return base.TryParseValueFromString(value, out result, out validationErrorMessage);
            }
        }
    }
}
