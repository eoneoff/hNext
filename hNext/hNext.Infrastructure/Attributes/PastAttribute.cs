using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace hNext.Infrastructure.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class PastAttribute:ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if ((value is DateTime date && date <= DateTime.Now) || value == null)
                return true;
            else
                return false;
        }
    }
}
