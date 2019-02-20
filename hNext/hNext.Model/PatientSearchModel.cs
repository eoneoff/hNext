using hNext.Infrastructure.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace hNext.Model
{
    public class PatientSearchModel
    {
        [Display(ResourceType = typeof(Resources.Resources),
            Name = nameof(Resources.Resources.FamilyName))]
        public string Name { get; set; }
        [CurrentYearRange(120)]
        [Display(ResourceType = typeof(Resources.Resources),
            Name = nameof(Resources.Resources.YearOfBirth))]
        public int? YearOfBirth { get; set; }
        [Display(ResourceType = typeof(Resources.Resources),
            Name = nameof(Resources.Resources.Region))]
        public int? RegionId { get; set; }
        [Display(ResourceType = typeof(Resources.Resources),
            Name = nameof(Resources.Resources.District))]
        public int? DistrictId { get; set; }
        [Display(ResourceType = typeof(Resources.Resources),
            Name = nameof(Resources.Resources.City))]
        public int? CityId { get; set; }
    }
}
