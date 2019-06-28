using hNext.ResourceLibrary.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace hNext.Model
{
    public class Diploma
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources),
            ErrorMessageResourceName = nameof(Resources.EnterNumber))]
        [Display(ResourceType = typeof(Resources),
            Name = nameof(Resources.Number))]
        public string Number { get; set; }

        [Display(ResourceType = typeof(Resources),
            Name = nameof(Resources.WhenIssued))]
        [Required(ErrorMessageResourceType = typeof(Resources),
            ErrorMessageResourceName = nameof(Resources.EnterDate))]
        [DataType(DataType.Date)]
        public DateTime WhenIssued { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources),
            ErrorMessageResourceName = nameof(Resources.EnterUniversity))]
        [Display(ResourceType = typeof(Resources),
            Name = nameof(Resources.University))]
        public string University { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources),
            ErrorMessageResourceName = nameof(Resources.EnterSpecialty))]
        [Display(ResourceType = typeof(Resources),
            Name = nameof(Resources.Specialty))]
        public string Specialty { get; set; }

        [Required]
        public long DoctorId { get; set; }

        [ForeignKey(nameof(DoctorId))]
        public virtual Doctor Doctor { get; set; }
    }
}
