using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using hNext.ResourceLibrary.Resources;

namespace hNext.Model
{
    public class GuardianWard
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required(ErrorMessageResourceType = typeof(Resources),
            ErrorMessageResourceName = nameof(Resources.SelectWard))]
        [Column(Order = 0)]
        public long WardId { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required(ErrorMessageResourceType = typeof(Resources),
            ErrorMessageResourceName = nameof(Resources.SelectGuardian))]
        [Column(Order = 1)]
        public long GuardianId { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources),
            ErrorMessageResourceName = nameof(Resources.EnterRelation))]
        [MaxLength(100)]
        [Display(ResourceType = typeof(Resources),
            Name = nameof(Resources.Relation))]
        public string Relation { get; set; }

        public virtual Person Guardian { get; set; }

        public virtual Person Ward { get; set; }
    }
}
