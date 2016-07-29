using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace UniversityCourseResultManagementApp.Gateway
{
    public class CommonGateway
    {
        string connectionString = WebConfigurationManager.ConnectionStrings["UniversityApp"].ConnectionString;
        public SqlConnection connection { get; set; }
        public SqlCommand Command { get; set; }
        public  string query { get; set; }

        public CommonGateway()
        {
            connection = new SqlConnection(connectionString);
        }
    }
}