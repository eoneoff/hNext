using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace hNext.Model
{
    public class Email
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resources),
            ErrorMessageResourceName = nameof(Resources.Resources.EnterEmail))]
        [Display(Name = "e-mail")]
        [MaxLength(100)]
        public string Address { get; set; }

        [ForeignKey(nameof(PersonEmails.EmailId))]
        [Newtonsoft.Json.JsonIgnore]
        public virtual ICollection<PersonEmails> People { get; set; }
    }
}
