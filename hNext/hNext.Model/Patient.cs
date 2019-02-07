﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace hNext.Model
{
    public class Patient
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        public long PersonId { get; set; }

        [MaxLength(50)]
        public string eHealthId { get; set; }
        public string Login { get; set; }

        [ForeignKey(nameof(PersonId))]
        public virtual Person Person { get; set; }
    }
}
