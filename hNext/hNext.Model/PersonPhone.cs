using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace hNext.Model
{
    public class PersonPhone
    {
        public long PersonId { get; set; }

        public long PhoneId { get; set; }

        [ForeignKey(nameof(PersonId))]
        public virtual Person Person { get; set; }

        [ForeignKey(nameof(PhoneId))]
        public virtual Phone Phone { get; set; }
    }
}
