using hNext.ResourceLibrary.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace hNext.Model
{
    public class CaseHistoryAdmission
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        public long CaseHistoryId { get; set; }

        [Display(ResourceType = typeof(Resources),
            Name = nameof(Resources.Department))]
        public int? DepartmentId { get; set; }

        [Display(ResourceType = typeof(Resources),
            Name = nameof(Resources.Discharged))]
        [DataType(DataType.Date)]
        [Column(TypeName = "date")]
        public DateTime? Discharged { get; set; }

        [ForeignKey(nameof(CaseHistoryId))]
        public virtual CaseHistory CaseHistory { get; set; }

        [ForeignKey(nameof(DepartmentId))]
        public virtual Department Department { get; set; }
    }
}
