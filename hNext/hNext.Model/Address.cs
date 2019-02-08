using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hNext.Model
{
    public class Address
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resources),
            ErrorMessageResourceName = nameof(Resources.Resources.SelectCountry))]
        [Display(ResourceType = typeof(Resources.Resources),
            Name = nameof(Resources.Resources.Country))]
        public int CountryId { get; set; }

        [Display(ResourceType = typeof(Resources.Resources),
            Name = nameof(Resources.Resources.Region))]
        public int? RegionId { get; set; }

        [Display(ResourceType = typeof(Resources.Resources),
            Name = nameof(Resources.Resources.District))]
        public int? DistrictId { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resources),
            ErrorMessageResourceName =nameof(Resources.Resources.SelectCity))]
        [Display(ResourceType = typeof(Resources.Resources),
            Name = nameof(Resources.Resources.City))]
        public int CityId { get; set; }

        [Display(ResourceType = typeof(Resources.Resources),
            Name = nameof(Resources.Resources.Street))]
        public int? StreetId { get; set; }

        [MaxLength(50)]
        [Display(ResourceType = typeof(Resources.Resources),
            Name = nameof(Resources.Resources.Building))]
        public string Building { get; set; }

        [MaxLength(50)]
        [Display(ResourceType = typeof(Resources.Resources),
            Name = nameof(Resources.Resources.Apartment))]
        public string Apartment { get; set; }

        [MaxLength(10)]
        [Display(ResourceType = typeof(Resources.Resources),
            Name = nameof(Resources.Resources.Zip))]
        public string Zip { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resources),
            ErrorMessageResourceName = nameof(Resources.Resources.EnterAddressType))]
        [Display(ResourceType = typeof(Resources.Resources),
            Name = nameof(Resources.Resources.Type))]
        public int AddressTypeId { get; set; }

        public virtual Country Country { get; set; }
        public virtual Region Region { get; set; }
        public virtual District District { get; set; }
        public virtual City City { get; set; }
        public virtual Street Street { get; set; }

        [ForeignKey(nameof(AddressTypeId))]
        public virtual AddressType AddressType { get; set; }

        [JsonIgnore]
        public virtual ICollection<Person> People { get; set; }
    }
}
