using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityCourseResultManagementApp.Gateway;
using UniversityCourseResultManagementApp.Models;

namespace UniversityCourseResultManagementApp.Manager
{
    public class TeacherManager
    {
        TeacherGateway teacherGateway = new TeacherGateway();
        public string Save(Teacher teacher)
        {
            bool isEmailExists = teacherGateway.IsEmailExists(teacher.Email);
            string message = "";
            if (isEmailExists)
            {
                message = "This Email Already Exists... Please Try With New One";

            }
            else
            {
                int rowAffected = teacherGateway.Save(teacher);
                if (rowAffected > 0)
                {
                    message = "Teacher Information Saved Successfully...";
                }
            }
            return message;
        }


        //public string AssignCourseToATeacher(CourseAssignToTeacher courseAssign)
        //{
        //    CourseAssignToTeacher exists = GetAllCourseAssignInfo().Find(x => x.CourseId == courseAssign.CourseId);
        //    if (exists == null)
        //    {
        //        int rowAffected = teacherGateway.AssignCourse(courseAssign);
        //        if (rowAffected > 0)
        //        {
        //            return "Course Assigned Successfully";
        //        }
        //        return "Course Not Assigned";
        //    }
        //    return "Course already Assigned";

        //}

        public List<Teacher> GetAllTeachers()
        {
            return teacherGateway.GetAllTeachers();
        }

        public Teacher GetTeacherByTeacherId(int teacherId)
        {
            Teacher teachers = GetAllTeachers().Find(x => x.Id == teacherId);
            return teachers;
        }

    
        public List<CourseAssignToTeacher> GetAllCourseAssignInfo()
        {
            return teacherGateway.GetAllCourseAssignInfo();
        }

        public List<Teacher> GetTeacherByDeptId(int departmentId)
        {
            List<Teacher> teachers = GetAllTeachers().Where(x => x.DepartmentId == departmentId).ToList();
            return teachers;
        }
        //public List<Teacher> GetCreditInfoByTeacherId(int teacherId)
        //{
        //    List<Teacher> teachers = GetAllTeachers().Where(x => x.Id == teacherId).ToList();
        //    return teachers;
        //}
        public Teacher GetTeacherDetailsByTeacherId(int teacherId)
        {
            return teacherGateway.GetTeacherDetailsByTeacherId(teacherId);
        }
    }
}