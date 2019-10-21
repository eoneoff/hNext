using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using hNext.ResourceLibrary.Resources;
using hNext.Infrastructure.Attributes;

namespace hNext.Model
{
    public class Document
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources),
            ErrorMessageResourceName = nameof(Resources.SelectType))]
        [Display(ResourceType = typeof(Resources),
            Name = nameof(Resources.Type))]
        public int DocumentTypeId { get; set; }

        [MaxLength(20)]
        [Display(ResourceType = typeof(Resources),
            Name = nameof(Resources.Number))]
        [Required(ErrorMessageResourceType = typeof(Resources),
            ErrorMessageResourceName = nameof(Resources.EnterNumber))]
        public string Number { get; set; }

        [Required]
        public long PersonId { get; set; }

        [MaxLength(100)]
        [Display(ResourceType = typeof(Resources),
            Name = nameof(Resources.IssuedBy))]
        public string IssuedBy { get; set; }

        [DataType(DataType.Date)]
        [Past(ErrorMessageResourceType = typeof(Resources),
            ErrorMessageResourceName = nameof(Resources.DateInFutureError))]
        [Display(ResourceType = typeof(Resources),
            Name = nameof(Resources.WhenIssued))]
        public DateTime? DateOfIssue { get; set; }

        [ForeignKey(nameof(DocumentTypeId))]
        public virtual DocumentType DocumentType { get; set; }

        [ForeignKey(nameof(PersonId))]
        [System.Text.Json.Serialization.JsonIgnore]
        public virtual Person Person { get; set; }
    }
}
