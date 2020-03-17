using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hNext.Model
{
    public class DrugDosage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id {get; set;}

        [Required]
        public int SubstanceId{ get; set; }
        public decimal Dosage { get; set; }
        public string Unit { get; set; }

        [ForeignKey(nameof(SubstanceId))]
        public virtual DrugSubstance DrugSubstance {get; set;}

        public virtual ICollection<DrugComponent> Drugs { get; set; }
    }
}