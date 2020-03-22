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

        [Display(ResourceType = typeof(Resources), Name=nameof(Resources.InternationalName))]
        public string InternationalName {get; set;}

        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.DrugForm))]
        public string Form { get; set;}

        [Display(ResourceType=typeof(Resources), Name= nameof(Resources.Compound))]
        public string Compound {get; set;}

        [Display(ResourceType=typeof(Resources), Name = nameof(Resources.Group))]
        public string Group { get; set; }

        public string ATC { get; set; }

        [Display(ResourceType = typeof(Resources), Name=nameof(Resources.Manufacturer))]
        public string Manufatcturer {get; set; }

        public virtual ICollection<DrugPrescription> Prescriptions {get; set; }
    }
}