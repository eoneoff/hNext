using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace hNext.Model
{
    public class StreetType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [MaxLength(50)]
        public string eHealthId { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        public virtual ICollection<Street> Streets { get; set; }
    }
}
