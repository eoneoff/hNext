using hNext.ResourceLibrary.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace hNext.Model
{
    public class CaseHistory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources),
            ErrorMessageResourceName = nameof(Resources.SelectPatient))]
        [Display(ResourceType = typeof(Resources),
            Name = nameof(Resources.Patient))]
        public long PatientId { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources),
            ErrorMessageResourceName = nameof(Resources.SelectHospital))]
        [Display(ResourceType = typeof(Resources),
            Name = nameof(Resources.Hospital))]
        public int HospitalId { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources),
            ErrorMessageResourceName = nameof(Resources.EnterDate))]
        [Display(ResourceType = typeof(Resources),
            Name = nameof(Resources.Admitted))]
        [DataType(DataType.Date)]
        [Column(TypeName = "date")]
        public DateTime Admitted { get; set; }

        [Display(ResourceType = typeof(Resources),
            Name = nameof(Resources.Discharged))]
        [DataType(DataType.Date)]
        [Column(TypeName = "date")]
        public DateTime? Discharged { get; set; }

        [Display(ResourceType = typeof(Resources),
            Name = nameof(Resources.Result))]
        public byte? Result { get; set; }

        [Display(ResourceType = typeof(Resources),
            Name = nameof(Resources.Severity))]
        public byte? Severity { get; set; }

        [Display(ResourceType = typeof(Resources),
            Name = nameof(Resources.Hospitalization))]
        public byte? Urgency { get; set; }

        [Display(ResourceType = typeof(Resources),
            Name = nameof(Resources.ReferredBy))]
        public int? ReferredById { get; set; }

        [Display(ResourceType = typeof(Resources),
            Name = nameof(Resources.Insured))]
        public bool Insured { get; set; }

        public virtual Patient Patient { get; set; }
        public virtual Hospital Hospital { get; set; }
        public virtual Hospital ReferredBy { get; set; }

        [ForeignKey(nameof(Id))]
        public virtual DocumentRegistry DocumentRegistry { get; set; }
        public virtual ICollection<CaseHistoryAdmission> Admissions { get; set; }
        public virtual ICollection<CaseHistoryDiagnosys> Diagnoses { get; set; }
    }
}
