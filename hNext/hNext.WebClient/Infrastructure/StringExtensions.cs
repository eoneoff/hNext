using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hNext.WebClient.Infrastructure
{
    public static class StringExtensions
    {
        public static string VewComponentName(this string name)
        {
            return name.Replace("ViewComponent", "");
        }

        public static string ToCamelCase(this string name)
        {
            return $"{name[0].ToString().ToLower()}{name.Substring(1)}";
        }
    }
}
