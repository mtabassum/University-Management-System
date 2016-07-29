using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UniversityCourseResultManagementApp.Models
{
    public class Teacher
    {
        public  int Id { get; set; }
        [Required(ErrorMessage = "Please Enter Teacher Name")]

        public string Name { get; set; }
        [Required(ErrorMessage = "Please Enter Address")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Please Provide an Email")]
        [RegularExpression(@"[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?\.)+[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?", ErrorMessage = "Invalid Email ")]
        public string Email { get; set; }
        [Required(ErrorMessage ="Please Provide Contact Number")]
        [Display(Name = "Contact No.")]
        public int ContactNo { get; set; }
        [Required(ErrorMessage = "Please Select a Designation")]
        [Display(Name = "Designation")]
        public int DesignationId { get; set; }
        [Required(ErrorMessage = "Please Select a Department")]
        [Display(Name = "Department")]
        public int DepartmentId { get; set; }
        [Required(ErrorMessage = "Credit To Be Taken is Required")]
        [Range(0, 100000000000000, ErrorMessage = "Credit to be taken Can not be Negetive")]
        [Display(Name = "Credit to be taken")]
        public  double CreditToBeTaken { get; set; }

        public double RemainingCredit { get; set; }
    }
}