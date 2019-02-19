using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace hNext.Infrastructure.Attributes
{
    public enum TimeDirection
    {
        Past,
        Future
    }

    public class CurrentYearRangeAttribute : RangeAttribute
    {
        public CurrentYearRangeAttribute(int limit, TimeDirection direction = TimeDirection.Past)
            :base(direction == TimeDirection.Past ? limit : DateTime.Today.Year,
                 direction == TimeDirection.Future ? limit : DateTime.Today.Year) { }
    }
}
