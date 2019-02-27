using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using hNext.ResourceLibrary.Resources;

namespace hNext.Model
{
    public class Region
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessageResourceType = typeof (Resources),
            ErrorMessageResourceName = nameof(Resources.EnterRegionName))]
        [Display(ResourceType = typeof(Resources),
            Name = nameof(Resources.Name))]
        public string Name { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources),
            ErrorMessageResourceName = nameof(Resources.SelectCountry))]
        [Display(ResourceType = typeof(Resources),
            Name = nameof(Resources.Country))]
        public int CountryId { get; set; }

        [MaxLength(50)]
        public string eHealthId { get; set; }


        [ForeignKey(nameof(CountryId))]
        [JsonIgnore]
        public virtual Country Country { get; set; }

        [JsonIgnore]
        public virtual ICollection<District> Districts { get; set; }

        [JsonIgnore]
        public virtual ICollection<City> Cities { get; set; }

        [JsonIgnore]
        public virtual ICollection<Address> Addresses { get; set; }
    }
}
