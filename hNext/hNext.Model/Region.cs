using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace hNext.Model
{
    public class Region
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessageResourceType = typeof (Resources.Resources),
            ErrorMessageResourceName = nameof(Resources.Resources.EnterRegionName))]
        [Display(ResourceType = typeof(Resources.Resources),
            Name = nameof(Resources.Resources.Name))]
        public string Name { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.Resources),
            ErrorMessageResourceName = nameof(Resources.Resources.SelectCountry))]
        [Display(ResourceType = typeof(Resources.Resources),
            Name = nameof(Resources.Resources.Country))]
        public int CountryId { get; set; }
        [MaxLength(50)]
        public string eHealthId { get; set; }

        [ForeignKey(nameof(CountryId))]
        public virtual Country Country { get; set; }
        public virtual ICollection<District> Districts {get;set;}
        public virtual ICollection<City> Cities { get; set; }
        public virtual ICollection<Address> Addresses { get; set; }
    }
}
