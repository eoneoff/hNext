using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace hNext.Model
{
    public class ICD
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [MaxLength(1)]
        [Required]
        public string Letter { get; set; }

        [Range(0, 99)]
        [Required]
        public int PrimaryNumber { get; set; }

        [Range(0,9)]
        public int? SecondaryNumber { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public string PrimaryName { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Diagnosys> Diagnoses { get; set; }
    }
}
