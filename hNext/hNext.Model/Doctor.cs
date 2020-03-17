using hNext.ResourceLibrary.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace hNext.Model
{
    public class Doctor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources),
            ErrorMessageResourceName = nameof(Resources.EnterPerson))]
        public long PersonId { get; set; }

        [Display(ResourceType = typeof(Resources),
            Name = nameof(Resources.Code))]
        public int? Code { get; set; }

        [Display(ResourceType = typeof(Resources),
            Name = nameof(Resources.Login))]
        public string Login { get; set; }

        [ForeignKey(nameof(PersonId))]
        public virtual Person Person { get; set; }

        [Display(ResourceType = typeof(Resources),
            Name = nameof(Resources.Positions))]
        public virtual ICollection<DoctorPosition> DoctorPositions { get; set; }

        [Display(ResourceType = typeof(Resources),
            Name = nameof(Resources.Diplomas))]
        public virtual ICollection<Diploma> Diplomas { get; set; }

        [Display(ResourceType = typeof(Resources),
            Name = nameof(Resources.Specialties))]
        public virtual ICollection<DoctorSpecialty> DoctorSpecialties { get; set; }

        public virtual ICollection<RecordTemplate> RecordTemplates { get; set; }
        public virtual ICollection<Prescription> Prescriptions { get; set; }
    }
}
