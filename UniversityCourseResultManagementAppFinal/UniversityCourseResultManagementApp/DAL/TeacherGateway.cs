using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using UniversityCourseResultManagementApp.Models;

namespace UniversityCourseResultManagementApp.Gateway
{
    public class TeacherGateway : CommonGateway
    {
        public int Save(Teacher teacher)
        {
            try
            {
                //string query = "INSERT INTO Teacher VALUES('" + teacher.Name + "','" + teacher.Address + "','" +
                //               teacher.Email + "','" + teacher.ContactNo + "'," + teacher.DesignationId + "," +
                //               teacher.DepartmentId + "," + teacher.CreditToBeTaken + "," + teacher.CreditToBeTaken + ")";
       string query = "INSERT INTO Teacher VALUES(@Name, @Address , @Email, @ContactNo, @DesignationId, @DepartmentId, @CreditToBeTaken,@CreditToBeTaken)";
                Command = new SqlCommand(query, connection);
                Command.Parameters.Clear();
                Command.Parameters.AddWithValue("@Name", teacher.Name);
                Command.Parameters.AddWithValue("@Address", teacher.Address);
                Command.Parameters.AddWithValue("@Email", teacher.Email);
                Command.Parameters.AddWithValue("@ContactNo", teacher.ContactNo);
                Command.Parameters.AddWithValue("@DesignationId", teacher.DesignationId);
                Command.Parameters.AddWithValue("@DepartmentId", teacher.DepartmentId);
                Command.Parameters.AddWithValue("@CreditToBeTaken", teacher.CreditToBeTaken);
              
                connection.Open();
                int rowAffected = Command.ExecuteNonQuery();
                //  connection.Close();
                return rowAffected;
            }
            catch (Exception e)
            {
                throw new Exception("Unable To Save Teacher...", e);
            }
            finally
            {
                connection.Close();
            }
        }

        public bool IsEmailExists(string email)
        {
            string query = "SELECT * FROM Teacher WHERE Email='" + email + "' ";
            Command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader Reader = Command.ExecuteReader();
            bool isEmailExists = Reader.Read();
            Reader.Close();
            connection.Close();
            return isEmailExists;
        }

        public List<Teacher> GetAllTeachers()
        {
            string query = "SELECT * FROM Teacher";
            Command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = Command.ExecuteReader();
            List<Teacher> teacherList = new List<Teacher>();
            while (reader.Read())
            {
                Teacher teacher = new Teacher()
                {
                    Id = int.Parse(reader["Id"].ToString()),
                    Name = reader["Name"].ToString(),
                    Address = reader["Address"].ToString(),
                    Email = reader["Email"].ToString(),
                    ContactNo = int.Parse(reader["ContactNo"].ToString()),
                    DesignationId = int.Parse(reader["DesignationId"].ToString()),
                    DepartmentId = int.Parse(reader["DepartmentId"].ToString()),
                    CreditToBeTaken = Convert.ToDouble(reader["CreditToBeTaken"])
                };
                teacherList.Add(teacher);
            }
            reader.Close();
            connection.Close();
            return teacherList;

        }
        //public int AssignCourse(CourseAssignToTeacher courseAssign)
        //{            
        //    string query = "INSERT INTO CourseAssignToTeacher VALUES('" + courseAssign.DepartmentId + "','" + courseAssign.TeacherId + "',"+courseAssign.CourseId+","+courseAssign.CreditToBeTaken+","+courseAssign.TotalCreditTaken+")";
        //    Command = new SqlCommand(query, connection);
        //    connection.Open();
        //    int rowAffected = Command.ExecuteNonQuery();
        //    connection.Close();
        //    if (rowAffected > 0)
        //    {
        //        return rowAffected;
        //    }
        //    return 0;
        //}

        public List<CourseAssignToTeacher> GetAllCourseAssignInfo()
        {
            string query = "SELECT * FROM CourseAssignToTeacher";
            Command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = Command.ExecuteReader();
            List<CourseAssignToTeacher> courseAssignsList = new List<CourseAssignToTeacher>();
            while (reader.Read())
            {
                CourseAssignToTeacher courseAssign = new CourseAssignToTeacher()
                {
                    DepartmentId = int.Parse(reader["DepartmentId"].ToString()),
                    TeacherId = int.Parse(reader["TeacherId"].ToString()),
                    CourseId = int.Parse(reader["CourseId"].ToString()),
                };
                courseAssignsList.Add(courseAssign);
            }
            reader.Close();
            connection.Close();
            return courseAssignsList;

        }

        public Teacher GetTeacherDetailsByTeacherId(int teacherId)
        {
            string query = "SELECT * FROM Teacher WHERE Id=" + teacherId + "";

            Command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader Reader = Command.ExecuteReader();
            Teacher aTeacher = new Teacher();
            if (Reader.Read())
            {
                aTeacher.Id = int.Parse(Reader["Id"].ToString());
                aTeacher.CreditToBeTaken = double.Parse(Reader["CreditToBeTaken"].ToString());
                aTeacher.RemainingCredit = double.Parse(Reader["RemainingCredit"].ToString());

            }
            Reader.Close();
            connection.Close();
            return aTeacher;
        }

        public int UpdateTeacherRemainingCredit(double credit, int teacherId)
        {
            string query = "UPDATE Teacher SET RemainingCredit=RemainingCredit-" + credit + " WHERE Id=" + teacherId + "";
            Command = new SqlCommand(query, connection);
            connection.Open();

            int update = Command.ExecuteNonQuery();
            connection.Close();
            return update;
        }
    }
}