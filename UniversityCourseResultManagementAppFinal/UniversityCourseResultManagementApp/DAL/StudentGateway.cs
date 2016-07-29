using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using UniversityCourseResultManagementApp.Manager;
using UniversityCourseResultManagementApp.Models;

namespace UniversityCourseResultManagementApp.Gateway
{
    public class StudentGateway:CommonGateway
    {
        
        //public int SaveStudent(Student aStudent)
        //{

        //    string query = "INSERT INTO Student VALUES(@Name, @Email, @ContactNo, @RegDate, @Address, @DepartmentId, @RegNo)";

        //    Command = new SqlCommand(query, connection);
        //    Command.Parameters.Clear();
        //    Command.Parameters.AddWithValue("@Name", aStudent.Name);

        //    Command.Parameters.AddWithValue("@Email", aStudent.Email);

        //    Command.Parameters.AddWithValue("@ContactNo", aStudent.ContactNo);

        //    Command.Parameters.AddWithValue("@RegDate", aStudent.RegDate);

        //    Command.Parameters.AddWithValue("@Address", aStudent.Address);

        //    Command.Parameters.AddWithValue("@DepartmentId", aStudent.DepartmentId);

        //    Command.Parameters.AddWithValue("@RegNo", aStudent.RegNo);

        //    connection.Open();
        //    int rowAffected = Command.ExecuteNonQuery();
        //    connection.Close();
        //    return rowAffected;
        //}
        public int GetNumberOfStudentByDeptId(int deptId)
        {
            int numberOfStudent = 0;
            string query = "Select count(*) as NumberOfStudent from Student where DepartmentId=" + deptId;
            Command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = Command.ExecuteReader();

            if (reader.HasRows)
            {
                reader.Read();
                numberOfStudent = int.Parse(reader["NumberOfStudent"].ToString());
            }
            connection.Close();

            return numberOfStudent;
        }

        public string GetLastAddedStudentRegistration(string regPattern)
        {
            //string query = "SELECT * FROM Student st WHERE RegNo LIKE '%" + regFirstPartPattern + "%' and Id=(select Max(Id) FROM Student st WHERE RegNo LIKE '%" + regFirstPartPattern + "%' )";
            //Command = new SqlCommand(query, connection);
            //connection.Open();

            //Student aStudent = null;
            //string regNo = null;
            //SqlDataReader Reader = Command.ExecuteReader();
            //if (Reader.Read())
            //{
            //    aStudent = new Student
            //    {
            //        Id = Convert.ToInt32(Reader["Id"].ToString()),
            //        Name = Reader["Name"].ToString(),
            //        RegNo = Reader["RegNo"].ToString(),
            //        Email = Reader["Email"].ToString(),
            //        ContactNo = Reader["ContactNo"].ToString(),

            //    };
            //    regNo = aStudent.RegNo;
            //}
            //Reader.Close();
            //connection.Close();

            //return regNo;
            string query = "SELECT TOP 1 RegNo AS RegNo FROM Student WHERE RegNo LIKE '%" + regPattern + "%' ORDER BY RegNo DESC";
            Command = new SqlCommand(query, connection);
            connection.Open();

            SqlDataReader Reader = Command.ExecuteReader();
            string lastRegiNo = "";
            if (Reader.Read())
            {
                lastRegiNo = Reader["RegNo"].ToString();
            }
            Reader.Close();
            connection.Close();
            string getInetegerValue = new string(lastRegiNo.ToCharArray().Where(c => char.IsDigit(c)).ToArray());
            string getOnlyNumberWithoutDate = "";
            if (getInetegerValue.Length > 4)
            {
                getOnlyNumberWithoutDate = getInetegerValue.Substring(4);
            }
            else
            {
                getOnlyNumberWithoutDate = "0";
            }

            int onlyRegLastNumber = int.Parse(getOnlyNumberWithoutDate) + 1;

            string finalRegNo = onlyRegLastNumber.ToString("000");

            return finalRegNo;
        }

        public IEnumerable<Student> GetAllStudents()
        {
            query = "SELECT * FROM Student";
            Command = new SqlCommand(query, connection);
            connection.Open();
            List<Student> students = new List<Student>();
            
            SqlDataReader Reader = Command.ExecuteReader();
            while (Reader.Read())
            {
                Student aStudent = new Student();

                aStudent.Id = Convert.ToInt32(Reader["Id"].ToString());
                aStudent.RegNo = Reader["RegNo"].ToString();
                aStudent.Name = Reader["Name"].ToString();
                aStudent.Email = Reader["Email"].ToString();
                aStudent.Address = Reader["Address"].ToString();
                aStudent.ContactNo = Reader["ContactNo"].ToString();
                aStudent.RegDate = Convert.ToDateTime(Reader["RegDate"].ToString());
                aStudent.DepartmentId = Convert.ToInt32(Reader["DepartmentId"].ToString());

                students.Add(aStudent);
            }
            Reader.Close();
            connection.Close();
            return students;
        }

        public ViewStudentDetails ViewStudentByID(int Id)
        {
            string query = "SELECT * FROM ViewStudentDetails WHERE StudentId=" + Id + "";

            Command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader Reader = Command.ExecuteReader();

            ViewStudentDetails student  = new ViewStudentDetails();
            if (Reader.Read())
            {
                student.Id = int.Parse(Reader["StudentId"].ToString());
                student.Name = Reader["StudentName"].ToString();
                student.Email = Reader["StudentEmail"].ToString();
                student.RegNo = Reader["StudentRegNo"].ToString();
                student.DepartmentName = Reader["DepartmentName"].ToString();
            }
            Reader.Close();
            connection.Close();
            return student;
        }
        public List<ViewStudentResult> ViewStudentResultByID(int Id)
        {
            string query = "SELECT CourseCode, CourseName, Grade FROM ViewStudentResult  WHERE StudentId =" + Id + "";

            Command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader Reader = Command.ExecuteReader();

            List<ViewStudentResult> studentLiast = new List<ViewStudentResult>();
            while (Reader.Read())
            {
                 ViewStudentResult student = new ViewStudentResult();
                if (Reader["Grade"] != DBNull.Value)
                {
                    student.Grade = Reader["Grade"].ToString();
                }
                else
                {
                    student.Grade = "<b>Not Graded Yet</b>";
                }

                student.CourseName = Reader["CourseName"].ToString();
                student.CourseCode = Reader["CourseCode"].ToString();
                studentLiast.Add(student);
            }
            Reader.Close();
            connection.Close();
            return studentLiast;
        }

        public int SaveStudent(Student aStudent)
        {
            DepartmentGateway aDepartmentGateway = new DepartmentGateway();
            DepartmentManager departmentManager = new DepartmentManager();
            Department department = departmentManager.GetAllDepartments().Single(depid => depid.Id == aStudent.DepartmentId);

            string deptCode = department.Code;

            string fullDeptName = aDepartmentGateway.GetDepartmentNameByID(aStudent.DepartmentId);
            string deptName = fullDeptName.ToUpper();
    
            int year = aStudent.RegDate.Year;
            string regFirstPartPattern = deptCode + "-" + year + "-";
            string regNoLastPattern = GetLastAddedStudentRegistration(regFirstPartPattern);
            aStudent.RegNo = regFirstPartPattern + regNoLastPattern;

     //       string query = "INSERT INTO Student VALUES('" + aStudent.Name + "','" + aStudent.Email + "','" + aStudent.ContactNo + "','" + aStudent.RegDate + "','" + aStudent.Address + "'," + aStudent.DepartmentId + ",'" + aStudent.RegNo + "')";

            string query = "INSERT INTO Student VALUES(@Name, @Email, @ContactNo, @RegDate, @Address, @DepartmentId, @RegNo)";

            Command = new SqlCommand(query,connection);
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("@Name", aStudent.Name);

            Command.Parameters.AddWithValue("@Email", aStudent.Email);

            Command.Parameters.AddWithValue("@ContactNo", aStudent.ContactNo);

            Command.Parameters.AddWithValue("@RegDate", aStudent.RegDate);

            Command.Parameters.AddWithValue("@Address", aStudent.Address);

            Command.Parameters.AddWithValue("@DepartmentId", aStudent.DepartmentId);

            Command.Parameters.AddWithValue("@RegNo", aStudent.RegNo);
            connection.Open();
            int rowAffected = Command.ExecuteNonQuery();

            connection.Close();

            return rowAffected;
        }
    }
}