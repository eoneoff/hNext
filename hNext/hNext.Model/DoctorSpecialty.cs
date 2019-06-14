using hNext.ResourceLibrary.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace hNext.Model
{
    public class DoctorSpecialty
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        public long DoctorId { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources),
            ErrorMessageResourceName = nameof(Resources.SelectSpecialty))]
        [Display(ResourceType = typeof(Resources),
            Name = nameof(Resources.Specialty))]
        public int SpecialtyId { get; set; }

        [Display(ResourceType = typeof(Resources),
            Name = nameof(Resources.Category))]
        public int? Category { get; set; }

        [Display(ResourceType = typeof(Resources),
            Name = nameof(Resources.IssuedBy))]
        public string SertifiedBy { get; set; }

        [Display(ResourceType = typeof(Resources),
            Name = nameof(Resources.WhenIssued))]
        [DataType(DataType.Date)]
        public DateTime? IssuedDate { get; set; }

        [Display(ResourceType = typeof(Resources),
            Name = nameof(Resources.ExpiryDate))]
        [DataType(DataType.Date)]
        public DateTime? Expires { get; set; }

        [Display(ResourceType = typeof(Resources),
            Name = nameof(Resources.Number))]
        public string Number { get; set; }

        [ForeignKey(nameof(DoctorId))]
        public virtual Doctor Doctor { get; set; }

        [ForeignKey(nameof(SpecialtyId))]
        public virtual Specialty Specialty { get; set; }
    }
}
