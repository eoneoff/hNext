using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace hNext.Model
{
    public class Gender:IModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [MaxLength(50)]
        public string eHealthId { get; set; }

        public virtual ICollection<Person> People { get; set; }
    }
}
