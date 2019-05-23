using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using hNext.ResourceLibrary.Resources;


namespace hNext.Model
{
    public class Address
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

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

        [Required(ErrorMessageResourceType = typeof(Resources),
            ErrorMessageResourceName =nameof(Resources.SelectCity))]
        [Display(ResourceType = typeof(Resources),
            Name = nameof(Resources.City))]
        public int CityId { get; set; }

        [Display(ResourceType = typeof(Resources),
            Name = nameof(Resources.Street))]
        public int? StreetId { get; set; }

        [MaxLength(50)]
        [Display(ResourceType = typeof(Resources),
            Name = nameof(Resources.Building))]
        public string Building { get; set; }

        [MaxLength(50)]
        [Display(ResourceType = typeof(Resources),
            Name = nameof(Resources.Apartment))]
        public string Apartment { get; set; }

        [MaxLength(10)]
        [Display(ResourceType = typeof(Resources),
            Name = nameof(Resources.Zip))]
        public string Zip { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources),
            ErrorMessageResourceName = nameof(Resources.EnterAddressType))]
        [Display(ResourceType = typeof(Resources),
            Name = nameof(Resources.Type))]
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

        [JsonIgnore]
        public virtual ICollection<Hospital> Hospitals { get; set; }
    }
}
