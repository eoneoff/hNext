using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace hNext.Model
{
    public class Person
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.Resources),
            ErrorMessageResourceName = nameof(Resources.Resources.EnterFirstName))]
        [Display(ResourceType = typeof(Resources.Resources),
            Name = nameof(Resources.Resources.FirstName))]
        public string FirstName { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.Resources),
            ErrorMessageResourceName = nameof(Resources.Resources.EnterFamilyName))]
        [Display(ResourceType = typeof(Resources.Resources),
            Name = nameof(Resources.Resources.FamilyName))]
        public string FamilyName { get; set; }
        [Display(ResourceType = typeof(Resources.Resources),
            Name = nameof(Resources.Resources.Patronimic))]
        public string Patronimic { get; set; }
        [Display(ResourceType = typeof(Resources.Resources),
            Name = nameof(Resources.Resources.Address))]
        public int AddressId { get; set; }
        [Display(ResourceType = typeof(Resources.Resources),
            Name = nameof(Resources.Resources.DateOfBirth))]
        public DateTime DateOfBirth { get; set; }
        [Display(ResourceType = typeof(Resources.Resources),
            Name = nameof(Resources.Resources.CountryOfBirth))]
        public int CountryOfBirthId { get; set; }
        [Display(ResourceType = typeof(Resources.Resources),
            Name = nameof(Resources.Resources.PlaceOfBirth))]
        public int PlaceOfBirthId { get; set; }

        [Display(ResourceType = typeof(Resources.Resources),
            Name = nameof(Resources.Resources.Gender))]
        public int GenderId { get; set; }
        [MaxLength(20)]
        [Display(ResourceType = typeof(Resources.Resources),
            Name = nameof(Resources.Resources.TaxId))]
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
