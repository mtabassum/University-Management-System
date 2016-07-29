using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UniversityCourseResultManagementApp.Models
{
    public class CourseAssignToTeacher
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Select a Department")]
        [Display(Name = "Department")]
        public int DepartmentId { set; get; }
        [Required(ErrorMessage = "Select a Teacher")]
        [Display(Name = "Teacher")]
        public int TeacherId { get; set; }      
        public double CreditToBeTaken { set; get; }
        public double TotalCreditTaken { set; get; }
        [Required(ErrorMessage = "Select a Course")]
        [Display(Name = "Course")]
        public int CourseId { set; get; }
        public string Assigned { get; set; }
    }
}