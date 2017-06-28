using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SATProject//.Models
{
    [MetadataType(typeof(ScheduledClassesMetaData))]
    public partial class ScheduledClass { }

    public class ScheduledClassesMetaData
    {
        [DataType(DataType.DateTime)]
        [Display(Name="Start Date")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime startDate { get; set; }
        [DataType(DataType.DateTime)]
        [Display(Name = "End Date")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime endDate { get; set; }
        [StringLength(75, ErrorMessage="Instructor Name cannot exceed 75 characters")]
        [Display(Name="Instructor Name")]
        public string instructorName { get; set; }
        [StringLength(50, ErrorMessage = "Location cannot exceed 50 characters")]
        [Display(Name = "Location")]
        public string location { get; set; }
        [Display(Name="Credit Hours")]
        [Range(0, 20, ErrorMessage = "Enter Valid Credit Hours")]
        public float creditHours { get; set; }
    }
}