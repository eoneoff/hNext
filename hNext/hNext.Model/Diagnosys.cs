using hNext.ResourceLibrary.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace hNext.Model
{
    public class Diagnosys
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources),
            ErrorMessageResourceName = nameof(Resources.EnterDiagnosys))]
        [Display(ResourceType = typeof(Resources),
            Name = nameof(Resources.Diagnosys))]
        public string Name { get; set; }

        [Display(ResourceType = typeof(Resources),
            Name = nameof(Resources.ICD))]
        public int? ICDId { get; set; }

        [ForeignKey(nameof(ICDId))]
        public virtual ICD ICD { get; set; }

        public virtual ICollection<PatientDiagnosys> Patients { get; set; }
        public virtual ICollection<CaseHistoryDiagnosys> CaseHistories { get; set; }
    }
}
