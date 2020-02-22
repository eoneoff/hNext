using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace hNext.Model
{
    public class RecordDiagnosys
    {
        public long RecordId { get; set; }

        public long DiagnosysId { get; set; }

        [ForeignKey(nameof(RecordId))]
        public virtual Record Record { get; set; }

        [ForeignKey(nameof(DiagnosysId))]
        public virtual Diagnosys Diagnosys { get; set; }
    }
}
