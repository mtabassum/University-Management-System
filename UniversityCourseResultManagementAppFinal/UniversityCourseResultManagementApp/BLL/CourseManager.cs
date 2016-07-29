using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityCourseResultManagementApp.Gateway;
using UniversityCourseResultManagementApp.Models;

namespace UniversityCourseResultManagementApp.Manager
{
    public class CourseManager
    {
        CourseGateway courseGateway = new CourseGateway();
        TeacherGateway teacherGateway = new TeacherGateway();
        //public string Save(Course course)
        //{
            
        //    if (IsCourseCodeExist(course.Code))
        //    {
        //        return "Course Code Already Exist";
        //    }
        //    else
        //    {
        //        if (IsCourseNameExist(course.Name))
        //        {
        //            return "Course Name Already Exist";
        //        }
        //        else if (CourseCodeValidation(course.Code) == false)
        //        {
        //            return "Code Must Be at least 5 Characters Long";
        //        }
        //    }

        //    if (CreditValidation(course.Credit) == false)
        //    {
        //        return "Credit range is from 0.5 to 5.0";
        //    }

        //    if (CourseCodeValidation(course.Code) && CreditValidation(course.Credit))
        //    {
        //        int rowAffected = courseGateway.Save(course);
        //        if (rowAffected > 0)
        //        {
        //            return "Course Saved Successfully";
        //        }
        //        else
        //        {
        //            return "Course Saved Failed";
        //        }
        //    }
        //    return courseGateway.Save(course).ToString();
        //}
        //bool IsCourseCodeExist(string code)
        //{
        //    CourseGateway courseGateway = new CourseGateway();
        //    return courseGateway.IsCourseCodeExist(code);
        //}


        public string SaveCourse(Course course)
        {
            bool isCourseCodeExist = courseGateway.IsCourseCodeExist(course.Code);
            bool isCourseNameExist = courseGateway.IsCourseNameExist(course.Name);
            string message = "";

            if (isCourseCodeExist)
            {
                message = "Course Code Already Exists ";
            }
            else
            {
                if (isCourseNameExist)
                {
                    message = "Course Name Already Exists ";
                }
                else if (CourseCodeValidation(course.Code) == false)
                {
                    message = "Code Must Be at least 5 Characters Long";
                }
                else if (CreditValidation(course.Credit) == false)
                {
                    message = "Credit range is from 0.5 to 5.0";
                }
                else
                {
                    int rowaffected = courseGateway.Save(course);
                    if (rowaffected > 0)
                    {
                        message = "Course Saved Successfully...";
                    }
                }
            }
            return message;
        }

        public string IsCourseCodeExist(string code)
        {
            bool isCodeExist = false;
            isCodeExist = courseGateway.IsCourseCodeExist(code);
            string message = "";

            if (isCodeExist == false)
            {
                message = "Code Unique Check Pass Successfully...";
            }
            else
            {
                message = "This Code Exist... Please Try With New One";
            }
            return message;
        }

        //bool IsCourseNameExist(string name)
        //{
        //    CourseGateway courseGateway = new CourseGateway();
        //    return courseGateway.IsCourseNameExist(name);
        //}
        bool CourseCodeValidation(string courseCode)
        {
            if (courseCode.Length < 5)
            {
                return false;
            }
            return true;
        }

        bool CreditValidation(double credit)
        {
            if (credit < 0.5 || credit > 5)
            {
                return false;
            }
            return true;
        }

        public List<Course> AllCourses()
        {
            if (courseGateway.GetAllCourses() != null)
            {
                return courseGateway.GetAllCourses();
            }
            return null;
        }

        public List<Course> GetCourseByCourseId(int courseId)
        {
            List<Course> course = AllCourses().Where(x => x.Id == courseId).ToList();
            return course;
        }

        public List<Course> GetCourseCodeByDeptId(int departmentId)
        {
            return AllCourses().Where(x => x.DepartmentId == departmentId).ToList();
        }


        public string AssignCourseToATeacher(CourseAssignToTeacher courseAssign)
        {
            //string message = "";
            //int rowAffected = 0;
            //bool isThisTeacherAssaigned = false;
            //bool isThisCourseAssigned = false;

            //isThisTeacherAssaigned = courseGateway.IsThisCourseAssignedToThisTeacher(courseAssign.TeacherId, courseAssign.CourseId);
            //isThisCourseAssigned = courseGateway.IsThisCourseAssignedToAnyTeacher(courseAssign.CourseId);

            //if (isThisTeacherAssaigned == false && isThisCourseAssigned == false)
            //{
            //    rowAffected = courseGateway.AssignCourseToTeacher(courseAssign);

            //    if (rowAffected > 0)
            //    {
            //        Course aCourse = courseGateway.GetCourseDetailsByCourseId(courseAssign.CourseId);
            //        teacherGateway.UpdateTeacherRemainingCredit(aCourse.Credit, courseAssign.TeacherId);

            //        message = "Course Successfully Assigned To This Teacher";
            //    }
            //}
            //else
            //{
            //    message = "Course Already Assigned !";
            //}

            //return message;

            //if (isThisTeacherAssaigned == false)
            //{
            //    if (isThisCourseAssigned == false)
            //    {
            //        rowAffected = courseGateway.AssignCourseToTeacher(courseAssign);
            //        if (rowAffected > 0)
            //        {
            //            Course aCourse = courseGateway.GetCourseDetailsByCourseId(courseAssign.CourseId);
            //            teacherGateway.UpdateTeacherRemainingCredit(aCourse.Credit, courseAssign.TeacherId);

            //            message = "Course Successfully Assigned To This Teacher";
            //        }
            //    }
            //    else
            //    {
            //        message = "Course Already Assigned !";
            //    }
            //}
            //else
            //{ message = "Course Assigned By this teacher already";}


            //return message;
            if (!courseGateway.IsThisCourseAssignedToThisTeacher(courseAssign.TeacherId, courseAssign.CourseId))
            {
                if (!courseGateway.IsThisCourseAssignedToAnyTeacher(courseAssign.CourseId))
                {
                    if (courseGateway.AssignCourseToTeacher(courseAssign) > 0)
                    {
                        Course aCourse = courseGateway.GetCourseDetailsByCourseId(courseAssign.CourseId);
                       teacherGateway.UpdateTeacherRemainingCredit(aCourse.Credit, courseAssign.TeacherId);
                        return "Course Successfully Assigned To This Teacher";
                    }
                    else
                    {
                        return "Sorry! Save Fail";
                    }
                }
                return "Course Already Assigned !";
            }
            return "This Teacher Already Assigned For This Course";
        }
        public List<Course> GetAllCoursesByDeptId(int deptId)
        {
            return courseGateway.GetAllCoursesByDeptId(deptId);
        }

    }

}
    