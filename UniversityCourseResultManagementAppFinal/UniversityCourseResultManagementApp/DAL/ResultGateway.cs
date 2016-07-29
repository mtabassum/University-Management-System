using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using UniversityCourseResultManagementApp.Gateway;
using UniversityCourseResultManagementApp.Models;

namespace UniversityCourseResultManagementApp.DAL
{
    public class ResultGateway:CommonGateway
    {
        public List<ViewEnrollCourseByAStudentId> GetAllEnrollCourseFromStudentId(int studentId)
        {
            List<ViewEnrollCourseByAStudentId> viewEnrollCourseByAStudentIds = new List<ViewEnrollCourseByAStudentId>();
             string Query = "SELECT * FROM ViewEnrollCourseByStudentId where StudentId='" + studentId + "'";
            Command = new SqlCommand(Query,connection);
            connection.Open();
            SqlDataReader Reader = Command.ExecuteReader();
            while (Reader.Read())
            {
                ViewEnrollCourseByAStudentId aViewEnrollCourseByAStudentId = new ViewEnrollCourseByAStudentId();
                aViewEnrollCourseByAStudentId.CourseId = Convert.ToInt32(Reader["CourseId"]);
                aViewEnrollCourseByAStudentId.CourseName = Reader["CourseName"].ToString();
                aViewEnrollCourseByAStudentId.EnrollStudentId = Convert.ToInt32(Reader["EnrollStudentId"]);
                aViewEnrollCourseByAStudentId.EnrollCourseId = Convert.ToInt32(Reader["EnrollCourseId"]);
                aViewEnrollCourseByAStudentId.StudentId = Convert.ToInt32(Reader["StudentId"]);
                viewEnrollCourseByAStudentIds.Add(aViewEnrollCourseByAStudentId);
            }

            Reader.Close();
            connection.Close();
            return viewEnrollCourseByAStudentIds;
        }


        public int SaveStudentResult(StudentResultSave studentResultSave)
        {
            string Query = "INSERT INTO StudentResult VALUES('" + studentResultSave.StudentId + "','" + studentResultSave.CourseId + "','" + studentResultSave.Grade + "')";
            Command = new SqlCommand(Query, connection);
            connection.Open();
            int rowAffected = Command.ExecuteNonQuery();
            connection.Close();
            return rowAffected;
        }


        public string FindSameCourseForAStudentWhichAssignAGrade(StudentResultSave studentResultSaveModel)
        {
            string Query = "SELECT * FROM StudentResult WHERE StudentId='" + studentResultSaveModel.StudentId + "' AND CourseId='" + studentResultSaveModel.CourseId + "'";
            Command = new SqlCommand(Query, connection);
            connection.Open();
            string message = null;
            SqlDataReader Reader = Command.ExecuteReader();
            if (Reader.HasRows)
            {
                message = "Grade Already assign for this Course by this Student";
            }
            Reader.Close();
            connection.Close();
            return message;
        }

    }
}