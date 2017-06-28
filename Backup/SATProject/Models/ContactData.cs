using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;//validation-display attributes

//This namespace is fine because contact data is free standing from our
//SAT classes.
namespace SATProject.Models
{

    public class ContactData
    {
        [Required(ErrorMessage = "Your name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Your email is required")]
        [RegularExpression(@"^\w+[\w-\.]*\@\w+((-\w+)|(\w*))\.[a-z]{2,3}$", ErrorMessage = "Invalid Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Content is required")]
        [UIHint("MultilineText")]
        public string Comments { get; set; }
    }//end class
}//end namespace