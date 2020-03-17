using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using hNext.ResourceLibrary.Resources;

namespace hNext.Model
{
    public class DrugDosage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id {get; set;}

        [Required]
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.Substance))]
        public int SubstanceId{ get; set; }

        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.Dosage))]
        public decimal Dosage { get; set; }

        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.Unit))]
        public string Unit { get; set; }

        [ForeignKey(nameof(SubstanceId))]
        public virtual DrugSubstance DrugSubstance {get; set;}

        public virtual ICollection<DrugComponent> Drugs { get; set; }
    }
}