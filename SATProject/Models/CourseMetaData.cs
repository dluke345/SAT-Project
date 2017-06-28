using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SATProject//.Models
{
    [MetadataType(typeof(CourseMetaData))]
    public partial class Cours { }

    public class CourseMetaData
    {
        [Required(ErrorMessage="*Course Name is required")]
        [StringLength(150, ErrorMessage="*Course Name cannot exceed 150 characters")]
        [Display(Name="Course Name")]
        public string courseName { get; set; }
        [StringLength(75, ErrorMessage="Department cannot exceed 75 characters")]
        [Display(Name = "Department")]
        public string department { get; set; }
        [StringLength(150, ErrorMessage = "Curriculum cannot exceed 150 characters")]
        [Display(Name = "Curriculum")]
        public string curriculum { get; set; }
        [UIHint("MultilineText")]
        [Display(Name ="Description")]
        public string courseDescription { get; set; }
        [UIHint("MultilineText")]
        [Display(Name = "Notes")]
        public string notes { get; set; }
    }
}