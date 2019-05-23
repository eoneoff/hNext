using hNext.ResourceLibrary.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace hNext.Model
{
    public class License
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(ResourceType = typeof(Resources),
            Name = nameof(Resources.Number))]
        public string Number { get; set; }

        [Display(ResourceType = typeof(Resources),
            Name = nameof(Resources.IssuedBy))]
        public string IssuedBy { get; set; }

        [DataType(DataType.Date)]
        [Display(ResourceType = typeof(Resources),
            Name = nameof(Resources.WhenIssued))]
        public DateTime? DateOfIssue { get; set; }

        [DataType(DataType.Date)]
        [Display(ResourceType = typeof(Resources),
            Name = nameof(Resources.ExpiryDate))]
        public DateTime? ExpiryDate { get; set; }

        public string OrderNo { get; set; }

        [Display(ResourceType = typeof(Resources),
            Name = nameof(Resources.ActivityLicensed))]
        public string ActivityLicensed { get; set; }
    }
}
