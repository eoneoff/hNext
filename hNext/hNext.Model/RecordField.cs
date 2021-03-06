﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace hNext.Model
{
    public class RecordField
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public int? RecordFieldTemplateId { get; set; }

        [Required]
        public long RecordId { get; set; }

        public string Value { get; set; }

        [Required]
        public int OrderNo { get; set; }

        [ForeignKey(nameof(RecordFieldTemplateId))]
        public virtual RecordFieldTemplate RecordFieldTemplate { get; set; }

        public virtual Record Record { get; set; }
    }
}
