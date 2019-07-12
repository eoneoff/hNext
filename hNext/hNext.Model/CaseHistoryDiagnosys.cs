using hNext.ResourceLibrary.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace hNext.Model
{
    public class CaseHistoryDiagnosys
    {
        public long CaseHistoryId { get; set; }
        public long DiagnosysId { get; set; }

        [Display(ResourceType = typeof(Resources),
            Name = nameof(Resources.Date))]
        [DataType(DataType.Date)]
        [Column(TypeName = "date")]
        public DateTime? Date { get; set; }
        public byte? Type { get; set; }
        public byte? WhenSet { get; set; }
        public bool Active { get; set; } = true;

        [ForeignKey(nameof(CaseHistoryId))]
        public virtual CaseHistory CaseHistory { get; set; }

        [ForeignKey(nameof(DiagnosysId))]
        public virtual Diagnosys Diagnosys { get; set; }
    }
}
