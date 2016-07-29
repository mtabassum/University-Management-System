using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using UniversityCourseResultManagementApp.Models;

namespace UniversityCourseResultManagementApp.Gateway
{
    public class DepartmentGateway:CommonGateway
    {
        public int Save(Department department)
        {
            string query = "INSERT INTO Department VALUES(@Code,@Name)";          
            Command = new SqlCommand(query, connection);
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("@Code", department.Code);
            Command.Parameters.AddWithValue("@Name", department.Name);

            connection.Open();
            int rowsAffected = Command.ExecuteNonQuery();
            connection.Close();
            return rowsAffected;
        }

        public bool IsDepartmentCodeExist(string code)
        {
            string query = "SELECT *FROM Department WHERE Code = '" + code + "' ";
            Command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = Command.ExecuteReader();
            bool departmentCodeExist = false;
            if (reader.HasRows)
            {
                departmentCodeExist = true;
            }
            reader.Close();
            connection.Close();
            return departmentCodeExist;
        }

        public bool IsDepartmentNameExist(string name)
        {
            string query = "SELECT *FROM Department WHERE Name = '" + name + "' ";
            Command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = Command.ExecuteReader();
            bool departmentNameExist = false;
            if (reader.HasRows)
            {
                departmentNameExist = true;
            }
            reader.Close();
            connection.Close();
            return departmentNameExist;
        }

        public List<Department> GetAllDepartments()
        {
            string query = "SELECT * FROM Department";
            Command = new SqlCommand(query,connection);
            connection.Open();
            SqlDataReader reader = Command.ExecuteReader();
             List<Department> departmentList = new List<Department>();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Department department = new Department();
                    department.Id = int.Parse(reader["Id"].ToString());
                    department.Code = reader["Code"].ToString();
                    department.Name = reader["Name"].ToString();
                    departmentList.Add(department);
                }
                reader.Close();
            }
            connection.Close();
            return departmentList;
        }


        public Department GetDeptNameById(int deptId)
        {
            string query = "SELECT  Name FROM Department WHERE Id='" + deptId + "'";
            Command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = Command.ExecuteReader();
            Department department = new Department();
            if (reader.HasRows)
            {
                reader.Read();                
             //   department.Id = int.Parse(reader["Id"].ToString());
                department.Name = reader["Name"].ToString();
                reader.Close();
            }
            connection.Close();
            return department;

        }
        public string GetDepartmentNameByID(int deptId)
        {
            string query = "SELECT Name FROM Department WHERE Id=" + deptId + "";
            Command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader Reader = Command.ExecuteReader();
            // Department aDepartment = new Department();
            string name = "";
            if (Reader.Read())
            {
                name = Reader["Name"].ToString();
            }
            Reader.Close();
            connection.Close();
            return name;

        }
    }
}