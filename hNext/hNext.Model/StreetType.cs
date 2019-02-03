using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace hNext.Model
{
    public class StreetType
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [MaxLength(50)]
        public string eHealthId { get; set; }

        public virtual ICollection<Street> Streets { get; set; }
    }
}
