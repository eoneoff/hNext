using hNext.ResourceLibrary.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace hNext.Model
{
    public class RecordFieldTemplate
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources),
            ErrorMessageResourceName = nameof(Resources.EnterHeader))]
        [Display(ResourceType = typeof(Resources),
            Name = nameof(Resources.Header))]
        public string Header { get; set; }

        [Required]
        public int OrderNo { get; set; }

        [Display(ResourceType = typeof(Resources),
            Name = nameof(Resources.NewLine))]
        public bool NewLine { get; set; } = false;
        public byte RecordFieldType { get; set; }

        [Display(ResourceType = typeof(Resources),
            Name = nameof(Resources.DefaultValue))]
        public string DefaultValue { get; set; }

        [Required]
        public int RecordTemplateId { get; set; }

        [ForeignKey(nameof(RecordTemplateId))]
        public virtual RecordTemplate RecordTemplate { get; set; }
        public virtual ICollection<RecordField> RecordFields { get; set; }
        public virtual ICollection<RecordFieldTemplateOption> RecordFieldTemplateOptions { get; set; }
    }
}
