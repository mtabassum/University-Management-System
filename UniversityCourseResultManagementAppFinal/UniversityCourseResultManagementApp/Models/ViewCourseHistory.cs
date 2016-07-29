using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversityCourseResultManagementApp.Models
{
    public class ViewCourseHistory
    {
        public  int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public string CourseCode { get; set; }
        public double CourseCredit { get; set; }
        public string CourseName { get; set; }
        public string SemesterName { get; set; }
        public string TeacherName { get; set; }
        public string Assigned { get; set; }
    }
}