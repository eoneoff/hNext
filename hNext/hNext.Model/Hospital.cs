using hNext.ResourceLibrary.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace hNext.Model
{
    public class Hospital
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources),
            ErrorMessageResourceName = nameof(Resources.EnterHospitalName))]
        [Display(ResourceType = typeof(Resources),
            Name = nameof(Resources.Name))]
        public string Name { get; set; }

        [Display(ResourceType = typeof(Resources),
            Name = nameof(Resources.ShortName))]
        public string ShortName { get; set; }

        [Display(ResourceType = typeof(Resources),
            Name = nameof(Resources.EDRPOU))]
        [Newtonsoft.Json.JsonProperty(PropertyName="edrpou")]
        public string EDRPOU { get; set; }

        [Display(ResourceType = typeof(Resources),
            Name = nameof(Resources.Url))]
        public string Url { get; set; }

        [Display(ResourceType = typeof(Resources),
            Name = nameof(Resources.Login))]
        public string Login { get; set; }

        public long AddressId { get; set; }

        [Display(ResourceType = typeof(Resources),
            Name = nameof(Resources.HospitalType))]
        [Required(ErrorMessageResourceType = typeof(Resources),
            ErrorMessageResourceName = nameof(Resources.SelectHospitalType))]
        public int HospitalTypeId { get; set; }

        [Display(ResourceType = typeof(Resources),
            Name = nameof(Resources.PropertyType))]
        [Required(ErrorMessageResourceType = typeof(Resources),
            ErrorMessageResourceName = nameof(Resources.SelectPropertyType))]
        public int PropertyTypeId { get; set; }

        [MaxLength(10)]
        public string eHealtId { get; set; }

        [ForeignKey(nameof(AddressId))]
        public virtual Address Address {get;set;}

        [ForeignKey(nameof(HospitalTypeId))]
        public virtual HospitalType HospitalType { get; set; }

        [ForeignKey(nameof(PropertyTypeId))]
        public virtual PropertyType PropertyType { get; set; }

        public virtual ICollection<HospitalPhone> Phones { get; set; }
        public virtual ICollection<HospitalEmail> Emails { get; set; }
        public virtual ICollection<HospitalLicense> Licenses { get; set; }
        public virtual ICollection<Department> Departments { get; set; }
        public virtual ICollection<DoctorPosition> DoctorPositions { get; set; }
    }
}
