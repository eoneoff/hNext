using hNext.ResourceLibrary.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace hNext.Model
{
    public class DocumentRegistry
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Display(ResourceType = typeof(Resources),
            Name = nameof(Resources.Number))]
        public string Number { get; set; }

        [Display(ResourceType = typeof(Resources),
            Name = nameof(Resources.Date))]
        [DataType(DataType.Date)]
        [Column(TypeName = "date")]
        public DateTime? CreationDate { get; set; }

        [Display(ResourceType = typeof(Resources),
            Name = nameof(Resources.Author))]
        public long? AuthorId { get; set; }

        [ForeignKey(nameof(AuthorId))]
        public virtual Person Author { get; set; }

        public virtual CaseHistory CaseHistory {get;set;}
    }
}
