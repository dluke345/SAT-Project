using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SATProject//.Models
{
    [MetadataType(typeof(EnrollmentMetaData))]
    public partial class Enrollment { }

    public class EnrollmentMetaData
    {
        [Required(ErrorMessage="Enrollment Date is required")]
        [DataType(DataType.DateTime)]
        [Display(Name="Enrollment Date")]
        [DisplayFormat(DataFormatString="{0:d}")]
        public DateTime enrollmentDate { get; set; }
    }
}