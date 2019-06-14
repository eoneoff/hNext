using hNext.ResourceLibrary.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace hNext.Model
{
    public class DoctorSearchModel
    {
        [Display(ResourceType = typeof(Resources),
            Name = nameof(Resources.FamilyName))]
        public string Name { get; set; }

        [Display(ResourceType = typeof(Resources),
            Name = nameof(Resources.Specialty))]
        public int? SpecialtyId { get; set; }

        [Display(ResourceType = typeof(Resources),
            Name = nameof(Resources.HospitalShort))]
        public int? HospitalId { get; set; }

        [Display(ResourceType = typeof(Resources),
            Name = nameof(Resources.Department))]
        public int? DepartmentId { get; set; }
    }
}
