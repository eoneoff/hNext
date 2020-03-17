using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hNext.Model
{
    public class DrugSubstance
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id {get; set;}

        [Required]
        public string Name { get; set; }

        public string InternationalName {get; set; }

        public string ATC { get; set; }

        public string eHealthId { get; set; }

        public virtual ICollection<DrugDosage> DrugDosages {get; set; }
    }
}