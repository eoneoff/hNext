using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace hNext.Model
{
    public class Patient
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public int PersonId { get; set; }
        public string Login { get; set; }

        [ForeignKey(nameof(PersonId))]
        public virtual Person Person { get; set; }
    }
}
