using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UniversityCourseResultManagementApp.Models
{
    public class Student
    {
        public  int? Id { get; set; }
        [Required(ErrorMessage = "Please Enter Student Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please Provide an Email")]
        
     [RegularExpression(@"[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?\.)+[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?", ErrorMessage = "Email is not valid ")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please Provide Contact Number")]
        [DisplayName("Contact No.")]
        [MaxLength(11, ErrorMessage = "Invalid Contact Number")]
        public string ContactNo { get; set; }
        [Required(ErrorMessage = "Please Enter Date")]
        
        public DateTime RegDate { get; set; }
        [Required(ErrorMessage = "Please Enter student Address")]
        public string Address { get; set; }
        
        [DisplayName("Department")]
        [Required(ErrorMessage = "Please Select Department")]
        public  int DepartmentId { get; set; }
        public  string RegNo { get; set; }

        public Student(int? id, string name, string regNo)
        {
            Id = id;
            Name = name;
            RegNo = regNo;

        }

        public Student()
        {

        }
    }
}