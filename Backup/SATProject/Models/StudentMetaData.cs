using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SATProject//.Models
{
    [MetadataType(typeof(StudentMetaData))]
    public partial class Student { }

    public class StudentMetaData
    {
        
        [Required(ErrorMessage="First Name is required")]
        [StringLength(50, ErrorMessage="First Name cannot exceed 50 characters")]
        [Display(Name="First Name")]
        public string firstName { get; set; }
        [Required(ErrorMessage = "Last Name is required")]
        [StringLength(50, ErrorMessage = "Last Name cannot exceed 50 characters")]
        [Display(Name = "Last Name")]
        public string lastName { get; set; }
        [StringLength(50, ErrorMessage = "Major cannot exceed 50 characters")]
        [Display(Name = "Major")]
        public string major { get; set; }
        [StringLength(20, ErrorMessage = "Classification cannot exceed 20 characters")]
        [Display(Name = "Classification")]
        public string classification { get; set; }
        [StringLength(50, ErrorMessage = "Address cannot exceed 50 characters")]
        [Display(Name = "Address")]
        public string address { get; set; }
        [StringLength(50, ErrorMessage = "City cannot exceed 50 characters")]
        [Display(Name = "City")]
        public string city { get; set; }
        [StringLength(25, ErrorMessage = "State cannot exceed 25 characters")]
        [Display(Name = "State")]
        public string state { get; set; }
        [StringLength(50, ErrorMessage = "Zip cannot exceed 50 characters")]
        [DataType(DataType.PostalCode)]
        [Display(Name = "Zip")]
        [RegularExpression(@"^\d{5}$", ErrorMessage = "Invalid Zip")]
        public string zip { get; set; }
        [StringLength(20, ErrorMessage = "Phone cannot exceed 20 characters")]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone")]
        [RegularExpression(@"((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}", ErrorMessage = "Invalid Phone #")]
        public string phone { get; set; }
        [StringLength(50, ErrorMessage = "Email cannot exceed 50 characters")]
        [RegularExpression(@"^\w+[\w-\.]*\@\w+((-\w+)|(\w*))\.[a-z]{2,3}$", ErrorMessage = "Invalid Email")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string email { get; set; }
        [DataType(DataType.ImageUrl)]
        [StringLength(150, ErrorMessage = "Picture Url cannot exceed 150 characters")]
        [Display(Name="Picture Url")]
        public string pictureUrl { get; set; }
    }
}