using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace hNext.Model
{
    public class HospitalEmail
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int HospitalId { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long EmailId { get; set; }

        [ForeignKey(nameof(HospitalId))]
        public virtual Hospital Hospital { get; set; }

        [ForeignKey(nameof(EmailId))]
        public virtual Email Email { get; set; }
    }
}
