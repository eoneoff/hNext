using hNext.Model;
using hNext.ResourceLibrary.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace hNext.WebClient.Models
{
    public class SpecialtiesSelectorViewModel
    {
        public IEnumerable<Specialty> Specialties { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources),
            ErrorMessageResourceName = nameof(Resources.SelectSpecialty))]
        [Display(ResourceType = typeof(Resources),
            Name = nameof(Resources.Specialty))]
        public int SpecialtyId { get; set; }
    }
}
