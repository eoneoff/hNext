using hNext.ResourceLibrary.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace hNext.Model
{
    public class RecordFieldTemplateOption
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int RecordFieldTemplateId { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources),
            ErrorMessageResourceName = nameof(Resources.EnterOption))]
        [Display(ResourceType = typeof(Resources),
            Name = nameof(Resources.Option))]
        public string Value { get; set; }
        public int OrderNo { get; set; }

        [ForeignKey(nameof(RecordFieldTemplateId))]
        public virtual RecordFieldTemplate RecordFieldTemplate { get; set; }
    }
}
