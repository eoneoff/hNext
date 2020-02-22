using hNext.ResourceLibrary.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace hNext.Model
{
    public class PatientDiagnosys
    {
        public long PatientId { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources),
            ErrorMessageResourceName = nameof(Resources.SelectDiagnosys))]
        [Display(ResourceType = typeof(Resources),
            Name = nameof(Resources.Diagnosys))]
        public long DiagnosysId { get; set; }

        [Display(ResourceType = typeof(Resources),
            Name = nameof(Resources.Date))]
        [DataType(DataType.Date)]
        [Column(TypeName = "date")]
        public DateTime? Date { get; set; }
        public bool Active { get; set; } = true;

        [ForeignKey(nameof(PatientId))]
        public virtual Patient Patient { get; set; }

        [ForeignKey(nameof(DiagnosysId))]
        public virtual Diagnosys Diagnosys { get; set; }

        public virtual Record Records { get; set; }
    }
}
