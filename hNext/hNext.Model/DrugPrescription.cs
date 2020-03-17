using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hNext.Model
{
    public class DrugPrescription : Prescription
    {
        [Required]
        public int DrugId {get; set;}

        [ForeignKey(nameof(DrugId))]
        public virtual Drug Drug { get; set; }
    }
}