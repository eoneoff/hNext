﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace hNext.Model
{
    public class Phone
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resources),
            ErrorMessageResourceName = nameof(Resources.Resources.EnterNumber))]
        [Display(ResourceType = typeof(Resources.Resources),
            Name =nameof(Resources.Resources.Number))]
        [MaxLength(15)]
        public string Number { get; set; }

        [Required]
        public int PhoneTypeId { get; set; }

        [ForeignKey(nameof(PhoneTypeId))]
        public virtual PhoneType PhoneType { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        public virtual ICollection<PersonPhone> People { get; set; }
    }
}
