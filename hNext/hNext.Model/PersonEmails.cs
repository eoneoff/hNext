using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace hNext.Model
{
    public class PersonEmails
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long PersonId { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long EmailId { get; set; }

        [ForeignKey(nameof(PersonId))]
        public virtual Person Person { get; set; }

        [ForeignKey(nameof(EmailId))]
        public virtual Email Email { get; set; }
    }
}
