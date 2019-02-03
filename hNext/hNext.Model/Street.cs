using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace hNext.Model
{
    public class Street:IModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int CityId { get; set; }
        [Required]
        public int StreetTypeId { get; set; }
        [MaxLength(50)]
        public string eHealthId { get; set; }

        [ForeignKey(nameof(StreetTypeId))]
        public virtual StreetType StreetType { get; set; }
        [ForeignKey(nameof(CityId))]
        public virtual City City { get; set; }

        public virtual ICollection<Address> Addresses { get; set; }
    }
}
