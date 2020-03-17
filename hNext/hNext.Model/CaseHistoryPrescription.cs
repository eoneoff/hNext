using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace hNext.Model
{
    public class CaseHistoryPrescription
    {
        public long CaseHistoryId { get; set; }
        public long PrescriptionId { get; set; }

        [ForeignKey(nameof(CaseHistoryId))]
        public virtual CaseHistory CaseHistory { get; set; }
        public virtual Prescription Prescription { get; set; }
    }
}
