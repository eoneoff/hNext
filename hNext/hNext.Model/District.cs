using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace hNext.Model
{
    public class District
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resources),
            ErrorMessageResourceName = nameof(Resources.Resources.EnterDistrictName))]
        [Display(ResourceType = typeof(Resources.Resources),
            Name = nameof(Resources.Resources.Name))]
        public string Name { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resources),
            ErrorMessageResourceName = nameof(Resources.Resources.SelectRegion))]
        [Display(ResourceType = typeof(Resources.Resources),
            Name = nameof(Resources.Resources.District))]
        public int RegionId { get; set; }

        [MaxLength(50)]
        public string eHealthId { get; set; }

        public virtual Region Region { get; set; }

        [JsonIgnore]
        public virtual ICollection<City> Cities { get; set; }

        [JsonIgnore]
        public virtual ICollection<Address> Addresses { get; set; }
    }
}
