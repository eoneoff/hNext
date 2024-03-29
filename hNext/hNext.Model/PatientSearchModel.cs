﻿using hNext.Infrastructure.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using hNext.ResourceLibrary.Resources;

namespace hNext.Model
{
    public class PatientSearchModel
    {
        [Display(ResourceType = typeof(Resources),
            Name = nameof(Resources.FamilyName))]
        public string Name { get; set; }
        [CurrentYearRange(120)]
        [Display(ResourceType = typeof(Resources),
            Name = nameof(Resources.YearOfBirth))]
        public int? YearOfBirth { get; set; }
        [Display(ResourceType = typeof(Resources),
            Name = nameof(Resources.Region))]
        public int? RegionId { get; set; }
        [Display(ResourceType = typeof(Resources),
            Name = nameof(Resources.District))]
        public int? DistrictId { get; set; }
        [Display(ResourceType = typeof(Resources),
            Name = nameof(Resources.City))]
        public int? CityId { get; set; }
    }
}
