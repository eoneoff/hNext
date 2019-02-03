using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace hNext.Model
{
    public class Country
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.Resources),
            ErrorMessageResourceName = nameof(Resources.Resources.EnterCountryName))]
        [Display(ResourceType = typeof(Resources.Resources),
            Name = nameof(Resources.Resources.Name))]
        public string Name { get; set; }
        [MaxLength(50)]
        public string eHealthId { get; set; }

        public virtual ICollection<Region> Regions { get; set; }
        public virtual ICollection<City> Cities { get; set; }
        public virtual ICollection<Address> Addresses { get; set; }
        [ForeignKey(nameof(Person.CountryOfBirthId))]
        public virtual ICollection<Person> PeopleBorn { get; set; }
    }
}
