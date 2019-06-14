using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace hNext.Model
{
    public class Specialty
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [MaxLength(10)]
        public string eHealthId { get; set; }

        [JsonIgnore]
        public virtual ICollection<DepartmentSpecialty> DepartmentSpecialties { get; set; }

        [JsonIgnore]
        public virtual ICollection<DoctorPosition> DoctorPositions { get; set; }

        [JsonIgnore]
        public virtual ICollection<DoctorSpecialty> DoctorSpecialties { get; set; }
    }
}
