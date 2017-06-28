using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SATProject//.Models
{
    [MetadataType(typeof(ScheduledClassStatusMetaData))]
    public partial class ScheduledClassStatu { }

    public class ScheduledClassStatusMetaData
    {
        [Required(ErrorMessage="*Class Status is required")]
        [StringLength(50, ErrorMessage="Class Status cannot exceed 50 characters")]
        [Display(Name="Class Status")]
        public string SCSName { get; set; }
    }
}