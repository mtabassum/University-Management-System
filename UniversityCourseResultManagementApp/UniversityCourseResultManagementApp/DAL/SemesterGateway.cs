using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using UniversityCourseResultManagementApp.Models;

namespace UniversityCourseResultManagementApp.Gateway
{
    public class SemesterGateway:CommonGateway
    {
        public List<Semester> GetAllSemesters()
        {
            string query = "SELECT *FROM Semester";
            Command = new SqlCommand(query,connection);
            connection.Open();
            SqlDataReader reader = Command.ExecuteReader();
            List<Semester> semesterList = new List<Semester>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Semester semester = new Semester();
                    semester.Id = int.Parse(reader["Id"].ToString());
                    semester.Name = reader["Name"].ToString();
                    semesterList.Add(semester);
                }
                reader.Close();
            }
            connection.Close();
            return semesterList;
        }
    }
}