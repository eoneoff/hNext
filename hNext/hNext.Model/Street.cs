using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using hNext.ResourceLibrary.Resources;

namespace hNext.Model
{
    public class Street
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources),
            ErrorMessageResourceName = nameof(Resources.EnterStreetName))]
        [Display(ResourceType = typeof(Resources),
            Name = nameof(Resources.Name))]
        public string Name { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources),
            ErrorMessageResourceName = nameof(Resources.SelectCity))]
        [Display(ResourceType = typeof(Resources),
            Name = nameof(Resources.City))]
        public int CityId { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources),
            ErrorMessageResourceName = nameof(Resources.SelectStreetType))]
        [Display(ResourceType = typeof(Resources),
            Name = nameof(Resources.Type))]
        public int StreetTypeId { get; set; }

        [MaxLength(50)]
        public string eHealthId { get; set; }

        [ForeignKey(nameof(StreetTypeId))]
        public virtual StreetType StreetType { get; set; }
        public virtual City City { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        public virtual ICollection<Address> Addresses { get; set; }
    }
}
