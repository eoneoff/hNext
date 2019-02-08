using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace hNext.Model
{
    public class Document
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resources),
            ErrorMessageResourceName = nameof(Resources.Resources.SelectType))]
        [Display(ResourceType = typeof(Resources.Resources),
            Name = nameof(Resources.Resources.Type))]
        public int DocumentTypeId { get; set; }

        [MaxLength(20)]
        public string Number { get; set; }

        [Required]
        public long PersonId { get; set; }

        [MaxLength(100)]
        public string IssuedBy { get; set; }

        public DateTime? DateOfIssue { get; set; }

        [ForeignKey(nameof(DocumentTypeId))]
        public virtual DocumentType DocumentType { get; set; }

        [ForeignKey(nameof(PersonId))]
        [Newtonsoft.Json.JsonIgnore]
        public virtual Person Person { get; set; }
    }
}
