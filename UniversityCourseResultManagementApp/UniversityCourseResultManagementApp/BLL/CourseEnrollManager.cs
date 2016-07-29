using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityCourseResultManagementApp.DAL;
using UniversityCourseResultManagementApp.Models;

namespace UniversityCourseResultManagementApp.BLL
{
    public class CourseEnrollManager
    {
        CourseEnrollGateway courseEnrollGateway = new CourseEnrollGateway();

        public List<ViewStudentDetails> GetAllStudentDetails(int studentId)
        {
            return courseEnrollGateway.GetAllStudentDetails(studentId);
        }

        public List<ViewCourseByStudentDepartmentName> GetAllCourseFromStudentDepartmentNames(int studentDepartmentId)
        {
            return courseEnrollGateway.GetAllCourseFromStudentDepartmentNames(studentDepartmentId);
        }

        public string EnrollCourse(CourseEnroll courseEnroll)
        {
            if (courseEnrollGateway.FindSameCourseForAStudent(courseEnroll) == null)
            {

                if (courseEnrollGateway.EnrollCourse(courseEnroll) > 0)
                {
                    return "Course Enrolled Successfully....";
                }
                else
                {
                    return "Sorry! Course Enrolled Operation Fail.";
                }

            }

            else
            {
                return courseEnrollGateway.FindSameCourseForAStudent(courseEnroll);
            }
        }

        public string UnassignCourse(string assigned)
        {
            string message = "";
            int rowAffected = courseEnrollGateway.UnassaignCourse(assigned);
            if (rowAffected > 0)
            {
                message = "All Courses Unassigned Successfully...";
            }
            else
            {
                message = "Courses Unassigned Failed";
            }
            return message;
        }
    }
}