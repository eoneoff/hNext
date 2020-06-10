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

        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.StartDate))]
        [DataType(DataType.Date)]
        public DateTime? StartDate { get; set; }

        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.EndDate))]
        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources),
        ErrorMessageResourceName = nameof(Resources.SelectPatient))]
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.Patient))]
        public long PatientId {get; set;}

        public string Text { get; set; }


        [ForeignKey(nameof(DoctorId))]
        public virtual Doctor Doctor {get; set;}
        public virtual Patient Patient {get; set; }
        public virtual ICollection<RecordPrescription> Records { get; set; }
        public virtual ICollection<CaseHistoryPrescription> CaseHistories { get; set; }
    }
}