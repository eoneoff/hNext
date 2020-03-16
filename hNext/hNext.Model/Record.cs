using hNext.ResourceLibrary.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace hNext.Model
{
    public class Record
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Id { get; set; }

        public int? RecordTemplateId { get; set; }

        [Required]
        public long PatientId { get; set; }

        [Display(ResourceType = typeof(Resources),
            Name = nameof(Resources.Specialty))]
        public int? SpecialtyId { get; set; }

        [Display(ResourceType = typeof(Resources),
            Name =nameof(Resources.Doctor))]
        public long? DoctorId { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources),
            ErrorMessageResourceName = nameof(Resources.EnterHeader))]
        public string Header { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources),
           ErrorMessageResourceName = nameof(Resources.EnterDate))]
        [Display(ResourceType = typeof(Resources),
            Name = nameof(Resources.Date))]
        public DateTime Date { get; set; }

        [ForeignKey(nameof(Id))]
        public DocumentRegistry DocumentRegistry { get; set; }

        [ForeignKey(nameof(RecordTemplateId))]
        public RecordTemplate RecordTemplate { get; set; }

        [ForeignKey(nameof(PatientId))]
        public virtual Patient Patient { get; set; }

        [ForeignKey(nameof(SpecialtyId))]
        public virtual Specialty Specialty { get; set; }

        [ForeignKey(nameof(DoctorId))]
        public virtual Doctor Doctor { get; set; }
        public virtual ICollection<RecordDiagnosys> Diagnoses { get; set; }
        public virtual ICollection<RecordField> RecordFields { get; set; }
        public virtual ICollection<RecordPrescription> Prescriptions { get; set; }
    }
}
