using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace hNext.Model
{
    public class HospitalPhone
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column(Order = 0)]
        public int HospitalId { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column(Order = 1)]
        public long PhoneId { get; set; }

        [ForeignKey(nameof(HospitalId))]
        public virtual Hospital Hospital { get; set; }

        [ForeignKey(nameof(PhoneId))]
        public virtual Phone Phone { get; set; }
    }
}
