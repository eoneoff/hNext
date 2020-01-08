using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace hNext.Model
{
    public class CaseHistoryConsultation : Record
    {
        public long CaseHistoryId { get; set; }

        public virtual CaseHistory CaseHistory { get; set; }
    }
}
