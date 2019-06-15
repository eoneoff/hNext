using hNext.ResourceLibrary.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace hNext.Model
{
    public class DoctorPosition
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        public long DoctorId { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources),
            ErrorMessageResourceName = nameof(Resources.SelectHospital))]
        [Display(ResourceType = typeof(Resources),
            Name = nameof(Resources.Hospital))]
        public int HospitalId { get; set; }

        [Display(ResourceType = typeof(Resources),
            Name = nameof(Resources.Department))]
        public int? DepartmentId { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources),
            ErrorMessageResourceName = nameof(Resources.SelectPosition))]
        [Display(ResourceType = typeof(Resources),
            Name = nameof(Resources.Position))]
        public int PositionId { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources),
            ErrorMessageResourceName = nameof(Resources.SelectSpecialty))]
        [Display(ResourceType = typeof(Resources),
            Name = nameof(Resources.Specialty))]
        public int SpecialtyId { get; set; }

        [Display(ResourceType = typeof(Resources),
            Name = nameof(Resources.StartPosition))]
        public DateTime? StartDate { get; set; }

        [Display(ResourceType = typeof(Resources),
            Name = nameof(Resources.EndPosition))]
        public DateTime? EndDate { get; set; }

        public virtual Doctor Doctor { get; set; }
        public virtual Hospital Hospital { get; set; }
        public virtual Department Department { get; set; }

        [ForeignKey(nameof(PositionId))]
        public virtual Position Position { get; set; }

        [ForeignKey(nameof(SpecialtyId))]
        public virtual Specialty Specialty { get; set; }
    }
}
