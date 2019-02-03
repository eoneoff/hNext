using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace hNext.Model
{
    public class Address:IModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public int CountryId { get; set; }
        public int RegionId { get; set; }
        public int DistrictId { get; set; }
        [Required]
        public int CityId { get; set; }
        public int StreetId { get; set; }
        [MaxLength(50)]
        public string Building { get; set; }
        [MaxLength(50)]
        public string Apartment { get; set; }
        [MaxLength(10)]
        public string Zip { get; set; }
        public int AddressTypeId { get; set; }

        [ForeignKey(nameof(CountryId))]
        public virtual Country Country { get; set; }
        [ForeignKey(nameof(RegionId))]
        public virtual Region Region { get; set; }
        [ForeignKey(nameof(DistrictId))]
        public virtual District District { get; set; }
        [ForeignKey(nameof(CityId))]
        public virtual City City { get; set; }
        [ForeignKey(nameof(StreetId))]
        public virtual Street Street { get; set; }
        [ForeignKey(nameof(AddressTypeId))]
        public virtual AddressType AddressType { get; set; }

        public virtual ICollection<Person> People { get; set; }
    }
}
