using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UniversityCourseResultManagementApp.Models
{
    public class Department
    {
        public int? Id { set; get; }
        [Required(ErrorMessage = "Please Enter Department Code")]
        [MinLength(2, ErrorMessage = "Code must be at least Two (2) characters long")]
        [MaxLength(7, ErrorMessage = "Code must be at least Seven (7) characters long")]
        public string Code { set; get; }
         [Required(ErrorMessage = "Please Enter Department Name")]
        public string Name { set; get; }

        public Department(int? id, string code, string name)
        {
            Id = id;
            Code = code;
            Name = name;
        }

        public Department()
        {
            // TODO: Complete member initialization
        }
    }
}