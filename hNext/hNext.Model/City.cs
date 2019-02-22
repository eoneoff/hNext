using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using hNext.ResourceLibrary.Resources;

namespace hNext.Model
{
    public class City
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources),
            ErrorMessageResourceName = nameof(Resources.EnterCityName))]
        [Display(ResourceType = typeof(Resources),
            Name = nameof(Resources.Name))]
        public string Name { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources),
            ErrorMessageResourceName = nameof(Resources.SelectCityType))]
        [Display(ResourceType = typeof(Resources),
            Name = nameof(Resources.Type))]
        public int CityTypeId { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources),
            ErrorMessageResourceName = nameof(Resources.SelectCountry))]
        [Display(ResourceType = typeof(Resources),
            Name = nameof(Resources.Country))]
        public int CountryId { get; set; }

        [Display(ResourceType = typeof(Resources),
            Name = nameof(Resources.Region))]
        public int? RegionId { get; set; }

        [Display(ResourceType = typeof(Resources),
            Name = nameof(Resources.District))]
        public int? DistrictId { get; set; }

        [MaxLength(50)]
        public string eHealthId { get; set; }

        [ForeignKey(nameof(CityTypeId))]
        public virtual CityType CityType { get; set; }

        public virtual Country Country { get; set; }
        public virtual Region Region { get; set; }
        public virtual District District { get; set; }

        [JsonIgnore]
        public virtual ICollection<Street> Streets { get; set; }

        [JsonIgnore]
        public virtual ICollection<Address> Addresses { get; set; }

        [JsonIgnore]
        public virtual ICollection<Person> PeopleBorn { get; set; }
    }
}
