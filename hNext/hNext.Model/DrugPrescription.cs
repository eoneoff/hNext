using hNext.ResourceLibrary.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hNext.Model
{
    public class DrugPrescription : Prescription
    {
        [Required]
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.Drug))]
        public int DrugId {get; set;}

        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.Dosage))]
        public string Dosage { get; set; }

        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.TimesPerDay))]
        public int? Times { get; set; }

        [ForeignKey(nameof(DrugId))]
        public virtual Drug Drug { get; set; }
    }
}