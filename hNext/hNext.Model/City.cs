using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace hNext.Model
{
    public class City
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.Resources),
            ErrorMessageResourceName = nameof(Resources.Resources.EnterCityName))]
        [Display(ResourceType = typeof(Resources.Resources),
            Name = nameof(Resources.Resources.Name))]
        public string Name { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.Resources),
            ErrorMessageResourceName = nameof(Resources.Resources.SelectCityType))]
        [Display(ResourceType = typeof(Resources.Resources),
            Name = nameof(Resources.Resources.Type))]
        public int CityTypeId { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.Resources),
            ErrorMessageResourceName = nameof(Resources.Resources.SelectCountry))]
        [Display(ResourceType = typeof(Resources.Resources),
            Name = nameof(Resources.Resources.Country))]
        public int CountryId { get; set; }
        [Display(ResourceType = typeof(Resources.Resources),
            Name = nameof(Resources.Resources.Region))]
        public int RegionId { get; set; }
        [Display(ResourceType = typeof(Resources.Resources),
            Name = nameof(Resources.Resources.District))]
        public int DistrictId { get; set; }
        [MaxLength(50)]
        public string eHealthId { get; set; }

        [ForeignKey(nameof(CityTypeId))]
        public virtual CityType CityType { get; set; }
        [ForeignKey(nameof(CountryId))]
        public virtual Country Country { get; set; }
        [ForeignKey(nameof(RegionId))]
        public virtual Region Region { get; set; }
        [ForeignKey(nameof(DistrictId))]
        public virtual District District { get; set; }

        public virtual ICollection<Street> Streets { get; set; }
        public virtual ICollection<Address> Addresses { get; set; }
        [ForeignKey(nameof(Person.PlaceOfBirthId))]
        public virtual ICollection<Person> PeopleBorn { get; set; }
    }
}
