using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using hNext.ResourceLibrary.Resources;

namespace hNext.Model
{
    public class DrugSubstance
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id {get; set;}

        [Required]
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.Name))]
        public string Name { get; set; }

        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.InternationalName))]
        public string InternationalName {get; set; }

        public string ATC { get; set; }

        public string eHealthId { get; set; }

        public virtual ICollection<DrugDosage> DrugDosages {get; set; }
    }
}