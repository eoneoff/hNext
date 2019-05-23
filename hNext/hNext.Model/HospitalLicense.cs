using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace hNext.Model
{
    public class HospitalLicense:License
    {
        public int HospitalId { get; set; }

        [ForeignKey(nameof(HospitalId))]
        public virtual Hospital Hospital { get; set; }
    }
}
