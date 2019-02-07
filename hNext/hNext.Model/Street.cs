using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace hNext.Model
{
    public class Street
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resources),
            ErrorMessageResourceName = nameof(Resources.Resources.EnterStreetName))]
        [Display(ResourceType = typeof(Resources.Resources),
            Name = nameof(Resources.Resources.Name))]
        public string Name { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resources),
            ErrorMessageResourceName = nameof(Resources.Resources.SelectCity))]
        [Display(ResourceType = typeof(Resources.Resources),
            Name = nameof(Resources.Resources.City))]
        public int CityId { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resources),
            ErrorMessageResourceName = nameof(Resources.Resources.SelectStreetType))]
        [Display(ResourceType = typeof(Resources.Resources),
            Name = nameof(Resources.Resources.Type))]
        public int StreetTypeId { get; set; }

        [MaxLength(50)]
        public string eHealthId { get; set; }

        [ForeignKey(nameof(StreetTypeId))]
        public virtual StreetType StreetType { get; set; }
        public virtual City City { get; set; }
        public virtual ICollection<Address> Addresses { get; set; }
    }
}
