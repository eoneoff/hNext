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

        [Required]
        public int RecordTemplateId { get; set; }

        [Required]
        public long PatientId { get; set; }
        public long? CaseHistoryId { get; set; }

        [Display(ResourceType = typeof(Resources),
            Name = nameof(Resources.Specialty))]
        public int? SpecialtyId { get; set; }

        [Display(ResourceType = typeof(Resources),
            Name =nameof(Resources.Doctor))]
        public long? DoctorId { get; set; }
        public string Header { get; set; }

        [Display(ResourceType = typeof(Resources),
            Name = nameof(Resources.Diagnosys))]
        public long? DiagnosysId { get; set; }

        [ForeignKey(nameof(Id))]
        public DocumentRegistry DocumentRegistry { get; set; }

        [ForeignKey(nameof(RecordTemplateId))]
        public RecordTemplate RecordTemplate { get; set; }

        [ForeignKey(nameof(PatientId))]
        public virtual Patient Patient { get; set; }

        [ForeignKey(nameof(CaseHistoryId))]
        public virtual CaseHistory CaseHistory { get; set; }

        [ForeignKey(nameof(SpecialtyId))]
        public virtual Specialty Specialty { get; set; }

        [ForeignKey(nameof(DoctorId))]
        public virtual Doctor Doctor { get; set; }

        [ForeignKey(nameof(DiagnosysId))]
        public virtual Diagnosys Diagnosys { get; set; }

        public virtual ICollection<RecordField> RecordFields { get; set; }
    }
}
