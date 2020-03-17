using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using hNext.ResourceLibrary.Resources;

namespace hNext.Model
{
    public class Drug
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.Name))]
        public string Name { get; set; }

        public string Manufacturer {get; set; }

        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.Manufacturer))]
        public string eHealthId { get; set; }

        public virtual ICollection<DrugComponent> DrugComponents {get; set; }
    }
}