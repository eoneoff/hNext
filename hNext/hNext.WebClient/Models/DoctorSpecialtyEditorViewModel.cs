using hNext.Model;
using hNext.ResourceLibrary.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace hNext.WebClient.Models
{
    public class DoctorSpecialtyEditorViewModel
    {
        [Display(ResourceType = typeof(Resources),
            Name = nameof(Resources.Specialties))]
        public IEnumerable<Specialty> Specialties { get; set; }
        public DoctorSpecialty DoctorSpecialty { get; set; }
    }
}
