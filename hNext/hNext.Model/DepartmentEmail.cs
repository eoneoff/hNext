using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace hNext.Model
{
    public class DepartmentEmail
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column(Order = 0)]
        public int DepartmentId { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column(Order = 0)]
        public long EmailId { get; set; }

        [ForeignKey(nameof(DepartmentId))]
        public virtual Department Department { get; set; }

        [ForeignKey(nameof(EmailId))]
        public virtual Email Email { get; set; }
    }
}
