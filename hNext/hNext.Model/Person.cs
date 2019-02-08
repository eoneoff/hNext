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
        public long Id { get; set; }

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
        public long AddressId { get; set; }

        [Display(ResourceType = typeof(Resources.Resources),
            Name = nameof(Resources.Resources.DateOfBirth))]
        public DateTime? DateOfBirth { get; set; }

        [Display(ResourceType = typeof(Resources.Resources),
            Name = nameof(Resources.Resources.CountryOfBirth))]
        public int? CountryOfBirthId { get; set; }

        [Display(ResourceType = typeof(Resources.Resources),
            Name = nameof(Resources.Resources.PlaceOfBirth))]
        public int? PlaceOfBirthId { get; set; }


        [Display(ResourceType = typeof(Resources.Resources),
            Name = nameof(Resources.Resources.Gender))]
        public int GenderId { get; set; }

        [Display(ResourceType = typeof(Resources.Resources),
            Name = nameof(Resources.Resources.TaxId))]
        [MaxLength(10)]
        public string TaxId { get; set; }

        [ForeignKey(nameof(AddressId))]
        public virtual Address Address { get; set; }

        public virtual Country CountryOfBirth { get; set; }

        public virtual City PlaceOfBirth { get; set; }

        [ForeignKey(nameof(GenderId))]
        public virtual Gender Gender { get; set; }

        [ForeignKey(nameof(PersonPhone.PersonId))]
        [Display(ResourceType = typeof(Resources.Resources),
            Name = nameof(Resources.Resources.Phones))]
        public virtual ICollection<PersonPhone> Phones { get; set; }

        [ForeignKey(nameof(PersonEmails.PersonId))]
        public virtual ICollection<PersonEmails> Emails { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        public virtual Patient Patient { get; set; }

        [Display(ResourceType = typeof(Resources.Resources),
            Name = nameof(Resources.Resources.Wards))]
        public virtual ICollection<GuardianWard> Wards { get; set; }

        [Display(ResourceType = typeof(Resources.Resources),
            Name = nameof(Resources.Resources.Guardians))]
        public virtual ICollection<GuardianWard> Guardians { get; set; }

        [Display(ResourceType = typeof(Resources.Resources),
            Name = nameof(Resources.Resources.Documents))]
        public virtual ICollection<Document> Documents { get; set; }
    }
}
