using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace hNext.Model
{
    public class DepartmentPhone
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column(Order = 0)]
        public int DepartmentId { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column(Order = 0)]
        public long PhoneId { get; set; }

        [ForeignKey(nameof(DepartmentId))]
        public Department Department { get;set; }

        [ForeignKey(nameof(PhoneId))]
        public Phone Phone { get; set; }
    }
}
