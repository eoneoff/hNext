using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using hNext.ResourceLibrary.Resources;
using Newtonsoft.Json;

namespace hNext.Model
{
    public class Person
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources),
            ErrorMessageResourceName = nameof(Resources.EnterFirstName))]
        [Display(ResourceType = typeof(Resources),
            Name = nameof(Resources.FirstName))]
        public string FirstName { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources),
            ErrorMessageResourceName = nameof(Resources.EnterFamilyName))]
        [Display(ResourceType = typeof(Resources),
            Name = nameof(Resources.FamilyName))]
        public string FamilyName { get; set; }

        [Display(ResourceType = typeof(Resources),
            Name = nameof(Resources.Patronimic))]
        public string Patronimic { get; set; }

        [Display(ResourceType = typeof(Resources),
            Name = nameof(Resources.Address))]
        public long AddressId { get; set; }

        [Display(ResourceType = typeof(Resources),
            Name = nameof(Resources.DateOfBirth))]
        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }

        [Display(ResourceType = typeof(Resources),
            Name = nameof(Resources.CountryOfBirth))]
        public int? CountryOfBirthId { get; set; }

        [Display(ResourceType = typeof(Resources),
            Name = nameof(Resources.PlaceOfBirth))]
        public int? PlaceOfBirthId { get; set; }


        [Display(ResourceType = typeof(Resources),
            Name = nameof(Resources.Gender))]
        [Required(ErrorMessageResourceType =typeof(Resources),
            ErrorMessageResourceName = nameof(Resources.SelectGender))]
        public int GenderId { get; set; }

        [Display(ResourceType = typeof(Resources),
            Name = nameof(Resources.TaxId))]
        [MaxLength(10)]
        public string TaxId { get; set; }

        [ForeignKey(nameof(AddressId))]
        public virtual Address Address { get; set; }

        public virtual Country CountryOfBirth { get; set; }

        public virtual City PlaceOfBirth { get; set; }

        [ForeignKey(nameof(GenderId))]
        public virtual Gender Gender { get; set; }

        [Display(ResourceType = typeof(Resources),
            Name = nameof(Resources.Phones))]
        public virtual ICollection<PersonPhone> Phones { get; set; }

        public virtual ICollection<PersonEmails> Emails { get; set; }

        [JsonIgnore]
        public virtual Patient Patient { get; set; }

        [JsonIgnore]
        public virtual Doctor Doctor { get; set; }

        [Display(ResourceType = typeof(Resources),
            Name = nameof(Resources.Wards))]
        public virtual ICollection<GuardianWard> Wards { get; set; }

        [Display(ResourceType = typeof(Resources),
            Name = nameof(Resources.Guardians))]
        public virtual ICollection<GuardianWard> Guardians { get; set; }

        [Display(ResourceType = typeof(Resources),
            Name = nameof(Resources.Documents))]
        public virtual ICollection<Document> Documents { get; set; }
        public virtual ICollection<DocumentRegistry> DocumentRegistries { get; set; }
    }
}
