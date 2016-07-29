using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UniversityCourseResultManagementApp.Models
{
    public class Course
    {
        public int Id { set; get; }
        [Required(ErrorMessage = "Please Enter Course Code")]
        [MinLength(5, ErrorMessage = "Code must be at least five (5) characters long")]
        public string Code { set; get; }
        [Required(ErrorMessage = "Please Enter Course Name")]
        public string Name { set; get; }
        [Required(ErrorMessage = "You must Enter a Value For Credit")]
        [Range(0.5, 5, ErrorMessage = "Credit cannot be less than 0.5 and more than 5.0")]
        public double Credit { set; get; }
        public string Description { set; get; }
        [Required(ErrorMessage = "Please Select a Department")]
        public int? DepartmentId { set; get; }
        [Required(ErrorMessage = "Please Select a Semester")]
        public int? SemesterId { set; get; }
    }
}