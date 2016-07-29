using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using UniversityCourseResultManagementApp.Gateway;
using UniversityCourseResultManagementApp.Models;

namespace UniversityCourseResultManagementApp.DAL
{
    public class CourseEnrollGateway:CommonGateway
    {
        public List<ViewStudentDetails> GetAllStudentDetails(int studentId)
        {
            List<ViewStudentDetails> viewStudentDetailses = new List<ViewStudentDetails>();
            string query = "SELECT * FROM ViewStudentDetails where StudentId='" + studentId + "'";
            Command = new SqlCommand(query,connection);
            connection.Open();
            SqlDataReader reader = Command.ExecuteReader();
            while (reader.Read())
            {
                ViewStudentDetails aViewStudentDetails = new ViewStudentDetails();
                aViewStudentDetails.Id = Convert.ToInt32(reader["StudentId"]);
                aViewStudentDetails.Name = reader["StudentName"].ToString();
                aViewStudentDetails.Email = reader["StudentEmail"].ToString();
                aViewStudentDetails.RegNo = reader["StudentRegNo"].ToString();
                aViewStudentDetails.DepartmentName = reader["DepartmentName"].ToString();
                viewStudentDetailses.Add(aViewStudentDetails);
            }

            reader.Close();
            connection.Close();
            return viewStudentDetailses;
        }


        public List<ViewCourseByStudentDepartmentName> GetAllCourseFromStudentDepartmentNames(int studentId)
        {
            List<ViewCourseByStudentDepartmentName> viewCourseFromStudentDepartment = new List<ViewCourseByStudentDepartmentName>();
            string query = "SELECT * FROM ViewCourseByStudentDepartmentName where StudentId='" + studentId + "'";
            Command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = Command.ExecuteReader();
            while (reader.Read())
            {
                ViewCourseByStudentDepartmentName aViewStudentDepartmentName = new ViewCourseByStudentDepartmentName();
                aViewStudentDepartmentName.CourseId = Convert.ToInt32(reader["CourseId"]);
                aViewStudentDepartmentName.CourseName = reader["CourseName"].ToString();
                aViewStudentDepartmentName.DepartmentId = Convert.ToInt32(reader["DepartmentId"]);
                aViewStudentDepartmentName.StudentId = Convert.ToInt32(reader["StudentId"]);
                aViewStudentDepartmentName.StudentDepartmentId = Convert.ToInt32(reader["StudentDepartmentId"]);
                viewCourseFromStudentDepartment.Add(aViewStudentDepartmentName);
            }

            reader.Close();
            connection.Close();
            return viewCourseFromStudentDepartment;
        }


        public int EnrollCourse(CourseEnroll courseEnroll)
        {
            string query = "INSERT INTO EnrollCourse VALUES('" + courseEnroll.StudentId + "','" + courseEnroll.CourseId + "','" + courseEnroll.EnrollDate + "')";
            Command = new SqlCommand(query, connection);
            connection.Open();
            int rowAffected = Command.ExecuteNonQuery();
            connection.Close();
            return rowAffected;
        }


        public string FindSameCourseForAStudent(CourseEnroll courseEnroll)
        {
            string query = "SELECT * FROM EnrollCourse WHERE StudentId='" + courseEnroll.StudentId + "' AND CourseId='" + courseEnroll.CourseId + "'";
            Command = new SqlCommand(query, connection);
            connection.Open();
            string message = null;
            
            SqlDataReader reader = Command.ExecuteReader();
            if (reader.HasRows)
            {
                message = "This Student Already Enrolled in This Course!";
            }
            reader.Close();
            connection.Close();
            return message;
        }

        public int UnassaignCourse(string assigned)
        {
            string query = "UPDATE CourseAssignToTeacher SET Assigned='" + assigned + "'";

            Command = new SqlCommand(query,connection);
            connection.Open();
            int rowAffected = Command.ExecuteNonQuery();
            connection.Close();
            return rowAffected;
        }
    }
}