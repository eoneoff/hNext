using hNext.ResourceLibrary.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace hNext.Model
{
    public class RecordTemplate
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(ResourceType = typeof(Resources),
            Name = nameof(Resources.Hospital))]
        public int? HospitalId { get; set; }

        [Display(ResourceType = typeof(Resources),
            Name = nameof(Resources.Department))]
        public int? DepartmentId { get; set; }

        [Display(ResourceType = typeof(Resources),
            Name = nameof(Resources.Specialty))]
        public int? SpecialtyId { get; set; }

        [Display(ResourceType = typeof(Resources),
            Name = nameof(Resources.Doctor))]
        public long? DoctorId { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources),
            ErrorMessageResourceName = nameof(Resources.EnterName))]
        [Display(ResourceType = typeof(Resources),
            Name = nameof(Resources.Name))]
        public string Name { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources),
            ErrorMessageResourceName = nameof(Resources.EnterHeader))]
        [Display(ResourceType = typeof(Resources),
            Name = nameof(Resources.Header))]
        public string Header { get; set; }


        [ForeignKey(nameof(HospitalId))]
        public virtual Hospital Hospital { get; set; }

        [ForeignKey(nameof(DepartmentId))]
        public virtual Department Department { get; set; }

        [ForeignKey(nameof(SpecialtyId))]
        public virtual Specialty Specialty { get; set; }

        [ForeignKey(nameof(DoctorId))]
        public virtual Doctor Doctor { get; set; }

        public virtual ICollection<Record> Records { get; set; }
        public virtual ICollection<RecordFieldTemplate> RecordFieldTemplates { get; set; }
    }
}
