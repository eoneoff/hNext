using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using hNext.ResourceLibrary.Resources;

namespace hNext.Model
{
    public class Prescription
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id {get; set;}

        [Required(ErrorMessageResourceType = typeof(Resources),
        ErrorMessageResourceName = nameof (Resources.EnterDate))]
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.Date))]
        public DateTime Date {get; set;}

        [Display(ResourceType = typeof(Resources),
        Name = nameof(Resources.Doctor))]
        public long DoctorId {get; set; }

        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.EndDate))]
        public DateTime? StartDate { get; set; }

        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.EndDate))]
        public DateTime? EndDate { get; set; }

        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.Duration))]
        public TimeSpan Duration { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources),
        ErrorMessageResourceName = nameof(Resources.SelectPatient))]
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.Patient))]
        public long PatientId {get; set;}


        [ForeignKey(nameof(DoctorId))]
        public virtual Doctor Doctor {get; set;}
        public virtual Patient Patient {get; set; }
        public virtual ICollection<RecordPrescription> Records { get; set; }
        public virtual ICollection<CaseHistoryPrescription> CaseHistories { get; set; }
    }
}