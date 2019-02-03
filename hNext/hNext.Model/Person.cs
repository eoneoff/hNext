using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace hNext.Model
{
    public class Person:IModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string FamilyName { get; set; }
        public string Patronimic { get; set; }
        public int AddressId { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int CountryOfBirthId { get; set; }
        public int PlaceOfBirthId { get; set; }
        public int GenderId { get; set; }
        [MaxLength(20)]
        public string TaxId { get; set; }
        [MaxLength(50)]
        public string eHealthId { get; set; }

        [ForeignKey(nameof(AddressId))]
        public virtual Address Address { get; set; }
        [ForeignKey(nameof(CountryOfBirthId))]
        public virtual Country CountryOfBirth { get; set; }
        [ForeignKey(nameof(PlaceOfBirthId))]
        public virtual City PlaceOfBirth { get; set; }
        [ForeignKey(nameof(GenderId))]
        public virtual Gender Gender { get; set; }

        public virtual ICollection<Patient> Patients { get; set; }
    }
}
