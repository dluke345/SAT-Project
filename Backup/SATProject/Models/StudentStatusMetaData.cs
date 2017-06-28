using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SATProject//.Models
{
    [MetadataType(typeof(StudentStatusMetaData))]
    public partial class StudentStatu { }

    public class StudentStatusMetaData
    {
        [Required(ErrorMessage="Student Status is required")]
        [StringLength(50, ErrorMessage="Student Status cannot exceed 50 characters")]
        [Display(Name="Student Status")]
        public string SSName { get; set; }
    }
}