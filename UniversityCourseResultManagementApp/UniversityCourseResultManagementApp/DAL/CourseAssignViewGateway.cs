using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using UniversityCourseResultManagementApp.Gateway;
using UniversityCourseResultManagementApp.Models;

namespace UniversityCourseResultManagementApp.DAL
{
    public class CourseAssignViewGateway:CommonGateway
    {
        public double GetTakenCredit(int dId, int tId)
        {
           double takenCredit = 0;
            string query = "SELECT * FROM ViewCourseHistory WHERE DepartmentId=" + dId + " AND TeacherId=" + tId + "";
            Command = new SqlCommand(query, connection);
              connection.Open();
            SqlDataReader reader = Command.ExecuteReader();
            while (reader.Read())
            {
                takenCredit += Convert.ToDouble(reader["CourseCredit"].ToString());
            }
            reader.Close();
            connection.Close();         
            return takenCredit;
        }

        public List<ViewCourseHistory> ViewCourseInformation(int departmentId)
        {
            string query = "SELECT * FROM ViewCourseHistory WHERE DepartmentId =" + departmentId + "";
       // string query = "SELECT c.Code AS CourseCode, c.Name AS CourseName, s.Name AS SemesterName, te.Name AS AssignTo, Department.Id AS DepartmentId FROM CourseAssignToTeacher AS t RIGHT OUTER JOIN Course AS c ON t.CourseId = c.Id LEFT OUTER JOIN Teacher AS te ON t.TeacherId = te.Id LEFT OUTER JOIN Semester AS s ON c.SemesterId = s.Id LEFT OUTER JOIN Department ON c.DepartmentId = Department.Id where Department.Id = " + departmentId+"";
            Command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = Command.ExecuteReader();
            List<ViewCourseHistory> courseHistory = new List<ViewCourseHistory>();
          
            while (reader.Read())
            {
                ViewCourseHistory viewCourseHistory = new ViewCourseHistory();
              
                viewCourseHistory.CourseCode = reader["CourseCode"].ToString();
                viewCourseHistory.CourseName = reader["CourseName"].ToString();
                viewCourseHistory.SemesterName = reader["SemesterName"].ToString();
                viewCourseHistory.Assigned = reader["Assigned"].ToString();
                if (reader["TeacherName"] == DBNull.Value || viewCourseHistory.Assigned != "Assigned" || reader["Assigned"] == DBNull.Value)
                {

                    viewCourseHistory.TeacherName = "<b>Not Assigned Yet</b>";

                }
                else
                {
                    viewCourseHistory.TeacherName = reader["TeacherName"].ToString();

                }
                courseHistory.Add(viewCourseHistory);
            }
            reader.Close();
            connection.Close();
            return courseHistory;
        }
    }
}