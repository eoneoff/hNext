﻿using hNext.ResourceLibrary.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace hNext.Model
{
    public class Doctor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        public long PersonId { get; set; }

        [Display(ResourceType = typeof(Resources),
            Name = nameof(Resources.Code))]
        public int? Code { get; set; }

        [Display(ResourceType = typeof(Resources),
            Name = nameof(Resources.Login))]
        public string Login { get; set; }

        [ForeignKey(nameof(PersonId))]
        public virtual Person Person { get; set; }
    }
}
