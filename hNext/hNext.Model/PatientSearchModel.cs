using hNext.Infrastructure.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace hNext.Model
{
    public class PatientSearchModel
    {
        public string Name { get; set; }
        [CurrentYearRange(120)]
        public int? YearOfBirth { get; set; }
        public int? RegionId { get; set; }
        public int? DistrictId { get; set; }
        public int? CityId { get; set; }
    }
}
