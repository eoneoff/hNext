using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace hNext.Model
{
    public class City:IModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int CityTypeId { get; set; }
        [Required]
        public int CountryId { get; set; }
        public int RegionId { get; set; }
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
