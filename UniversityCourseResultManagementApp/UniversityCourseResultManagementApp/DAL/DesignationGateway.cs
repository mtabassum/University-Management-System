using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using UniversityCourseResultManagementApp.Models;

namespace UniversityCourseResultManagementApp.Gateway
{
    public class DesignationGateway:CommonGateway
    {
        
        public List<Designation> GetAllDesignations()
        {
            string query = "SELECT * FROM Designation";
            Command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = Command.ExecuteReader();
            List<Designation> designationList = new List<Designation>();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Designation designation = new Designation();
                    designation.Id = int.Parse(reader["Id"].ToString());
                    designation.Name = reader["Name"].ToString();
                    designationList.Add(designation);
                }
                reader.Close();
            }
            connection.Close();
            return designationList;
        }
    }
}