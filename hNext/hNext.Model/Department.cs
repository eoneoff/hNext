using hNext.ResourceLibrary.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace hNext.Model
{
    public class Department
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources),
            ErrorMessageResourceName = nameof(Resources.EnterDepartmentName))]
        [Display(ResourceType = typeof(Resources),
            Name = nameof(Resources.Name))]
        public string Name { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources),
            ErrorMessageResourceName = nameof(Resources.SelectHospital))]
        [Display(ResourceType = typeof(Resources),
            Name = nameof(Resources.Hospital))]
        public int HospitalId { get; set; }

        [Display(ResourceType = typeof(Resources),
            Name = nameof(Resources.Login))]
        public string Login { get; set; }

        [MaxLength(10)]
        public string eHealthId { get; set; }

        [ForeignKey(nameof(HospitalId))]
        public virtual Hospital Hospital { get; set; }
        public virtual ICollection<DepartmentPhone> Phones { get; set; }
        public virtual ICollection<DepartmentEmail> Emails { get; set; }
    }
}
