﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using hNext.ResourceLibrary.Resources;

namespace hNext.Model
{
    public class Email
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources),
            ErrorMessageResourceName = nameof(Resources.EnterEmail))]
        [Display(Name = "e-mail")]
        [MaxLength(100)]
        public string Address { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        public virtual ICollection<PersonEmails> People { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        public virtual ICollection<HospitalEmail> Hospitals { get; set; }
    }
}
