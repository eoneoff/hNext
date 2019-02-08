using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace hNext.Model
{
    public class CityType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [MaxLength(10)]
        public string eHealthId { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        public virtual ICollection<City> Cities { get; set; }
    }
}
