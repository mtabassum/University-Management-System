using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversityCourseResultManagementApp.Models
{
    public class ViewCourseByStudentDepartmentName
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public int DepartmentId { get; set; }
        public int StudentId { set; get; }
        public int StudentDepartmentId { set; get; }
    }
}