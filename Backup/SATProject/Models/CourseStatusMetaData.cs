using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SATProject//.Models
{
    [MetadataType(typeof(CourseStatusMetaData))]
    public partial class CourseStatu { }

    public class CourseStatusMetaData
    {
        [Required(ErrorMessage="Course Status is required")]
        [StringLength(50, ErrorMessage="Course Status cannot exceed 50 characters")]
        [Display(Name = "Course Status")]
        public string CSName { get; set; }
    }
}