using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using UniversityCourseResultManagementApp.Models;

namespace UniversityCourseResultManagementApp.Gateway
{
    public class CourseGateway:CommonGateway
    {
        public int Save(Course course)
        {
            string query = "INSERT INTO Course VALUES('" + course.Code + "','" + course.Name + "'," + course.Credit + ",'" + course.Description + "'," + course.DepartmentId + "," + course.SemesterId + ")";
            //    string query = "INSERT INTO Course(Code, Name,Credit,Description,DepartmentId,SemesterId) VALUES(@Code,@Name,@Credit,@Description,@DepartmentId,@SemesterId)";
            Command = new SqlCommand(query, connection);
            //Command.Parameters.Clear();
            //Command.Parameters.Add("Code", SqlDbType.VarChar);
            //Command.Parameters["Code"].Value = course.Code;
            //Command.Parameters.Add("Name", SqlDbType.VarChar);
            //Command.Parameters["Name"].Value = course.Name;
            //Command.Parameters.Add("Credit", SqlDbType.Float);
            //Command.Parameters["Credit"].Value = course.Credit;
            //Command.Parameters.Add("Description", SqlDbType.VarChar);
            //Command.Parameters["Description"].Value = course.Description;
            //Command.Parameters.Add("DepartmentId", SqlDbType.Int);
            //Command.Parameters["DepartmentId"].Value = course.DepartmentId;
            //Command.Parameters.Add("SemesterId", SqlDbType.Int);
            //Command.Parameters["SemesterId"].Value = course.SemesterId;
            connection.Open();
            int rowsAffected = Command.ExecuteNonQuery();
            connection.Close();
            return rowsAffected;
        }
        //public int Save(Course course)
        //{

        //    string query = "INSERT INTO Course VALUES(@Code,@Name,@Credit,@Description,@DepartmentId,@SemesterId)";
        //    Command = new SqlCommand(query, connection);
        //    Command.Parameters.Clear();
        //    Command.Parameters.AddWithValue("@Code",course.Code);

        //    Command.Parameters.AddWithValue("@Name", course.Name);

        //    Command.Parameters.AddWithValue("@Credit", course.Credit);

        //    Command.Parameters.AddWithValue("@Description", course.Description);

        //    Command.Parameters.AddWithValue("@DepartmentId", course.DepartmentId);

        //    Command.Parameters.AddWithValue("@SemesterId",course.SemesterId);

        //    connection.Open();
        //    int rowsAffected = Command.ExecuteNonQuery();
        //    connection.Close();
        //    return rowsAffected;
        //}
        public bool IsCourseCodeExist(string code)
        {
            string query = "SELECT *FROM Course WHERE Code = '" + code + "' ";
            Command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = Command.ExecuteReader();
            bool courseCodeExist = false;
            if (reader.HasRows)
            {
                courseCodeExist = true;
            }
            reader.Close();
            connection.Close();
            return courseCodeExist;
        }

        public bool IsCourseNameExist(string name)
        {
            string query = "SELECT *FROM Course WHERE Name = '" + name + "' ";
            Command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = Command.ExecuteReader();
            bool courseNameExist = false;
            if (reader.HasRows)
            {
                courseNameExist = true;
            }
            reader.Close();
            connection.Close();
            return courseNameExist;
        }

        public List<Course> GetAllCourses()
        {           
            string query = "SELECT * FROM Course";
            Command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = Command.ExecuteReader();
            List<Course> courseList = new List<Course>();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Course courses = new Course();
                    courses.Id = int.Parse(reader["Id"].ToString());
                    courses.Code = reader["Code"].ToString();
                    courses.Name = reader["Name"].ToString();
                    courses.Credit = Convert.ToDouble(reader["Credit"].ToString());
                    courses.DepartmentId = int.Parse(reader["DepartmentId"].ToString());
                    courses.SemesterId = int.Parse(reader["SemesterId"].ToString());
                    courseList.Add(courses);
                }
                reader.Close();
            }
            connection.Close();
            return courseList;
        }

        //public List<CourseView> ViewCourse(int departmentId)
        //{
        //    string query = "SELECT c.Code as CourseCode,c.Name as CourseName, t.Name as AssignedTo,d.Name as DepartmentName, se.Name  as SemesterName from Course as c LEft join CourseAssignToTeacher as cat on c.Id = cat.CourseId Left join Teacher as t on t.Id = cat.TeacherId Left join Department as d on d.Id = c.DepartmentId Left join Semester as se on se.Id = c.SemesterId where d.Id = " + departmentId + " ";
        //    Command = new SqlCommand(query, connection);
        //    connection.Open();
        //    SqlDataReader reader = Command.ExecuteReader();
        //    List<CourseView> courseList = new List<CourseView>();

        //    while (reader.Read())
        //    {
        //        CourseView courseView = new CourseView();

        //        courseView.CourseCode = reader["CourseCode"].ToString();
        //        courseView.CourseName = reader["CourseName"].ToString();
        //        courseView.SemesterName = reader["SemesterName"].ToString();
        //        courseView.AssignedTo = reader["AssignedTo"].ToString();
        //        courseList.Add(courseView);
        //    }
        //    reader.Close();
        //    connection.Close();
        //    return courseList;
        //}
        public bool IsThisCourseAssignedToThisTeacher(int teacherId, int courseId)
        {
            string query = "SELECT * FROM CourseAssignToTeacher  WHERE Assigned ='Assigned' AND TeacherId=" + teacherId + " AND CourseId=" + courseId + " ";
            Command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = Command.ExecuteReader();
            //bool isExists = reader.Read();

            //reader.Close();
            //connection.Close();
            //return isExists;
            if (reader.HasRows)
            {
                return true;
            }

            reader.Close();
            connection.Close();
            return false;
        }

        public int AssignCourseToTeacher(CourseAssignToTeacher courseAssign)
        {
   //         string query = "INSERT INTO CourseAssignToTeacher VALUES(@DepartmentId, @TeacherId, @CourseId,'Assigned')";
      string query = "INSERT INTO CourseAssignToTeacher VALUES ('"+courseAssign.DepartmentId+"' , '"+courseAssign.TeacherId+"' , '"+courseAssign.CourseId+ "' , 'Assigned')";

            Command = new SqlCommand(query, connection);
            connection.Open();
            //Command.Parameters.Clear();

            //Command.Parameters.Add("DepartmentId", SqlDbType.Int);
            //Command.Parameters["DepartmentId"].Value = courseAssign.DepartmentId;

            //Command.Parameters.Add("TeacherId", SqlDbType.Int);
            //Command.Parameters["TeacherId"].Value = courseAssign.TeacherId;

            //Command.Parameters.Add("CourseId", SqlDbType.Int);
            //Command.Parameters["CourseId"].Value = courseAssign.CourseId;

            int rowAffected = Command.ExecuteNonQuery();
            connection.Close();
            return rowAffected;
        }
        public List<Course> GetAllCoursesByDeptId(int deptId)
        {

            string query = "SELECT * FROM Course Where DepartmentId=" + deptId + "";
            Command = new SqlCommand(query, connection);
            connection.Open(); ;
            SqlDataReader Reader = Command.ExecuteReader();
            List<Course> courseList = new List<Course>();
            while (Reader.Read())
            {
                Course aCourse = new Course();
                aCourse.Id = int.Parse(Reader["Id"].ToString());
                aCourse.Name = Reader["Name"].ToString();
                aCourse.Code = Reader["Code"].ToString();
                courseList.Add(aCourse);
            }
            Reader.Close();
            connection.Close();

            return courseList;
        }

        public Course GetCourseDetailsByCourseId(int courseId)
        {
            string query = "SELECT * FROM Course WHERE Id=" + courseId + "";
            Command = new SqlCommand(query, connection);
            connection.Open(); ;
            SqlDataReader Reader = Command.ExecuteReader();
            Course aCourse = new Course();
            if (Reader.Read())
            {
                aCourse.Name = Reader["Name"].ToString();
                aCourse.Credit = double.Parse(Reader["Credit"].ToString());

            }
            Reader.Close();
            connection.Close();
            return aCourse;
        }

        public bool IsThisCourseAssignedToAnyTeacher(int courseId)
        {
            string query = "SELECT * FROM CourseAssignToTeacher  WHERE CourseId=" + courseId + " ";
            Command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = Command.ExecuteReader();
            //bool isExists = reader.Read();

            //reader.Close();
            //connection.Close();
            //return isExists;
            if (reader.HasRows)
            {
                return true;
            }

            reader.Close();
            connection.Close();
            return false;
        }
    }
}