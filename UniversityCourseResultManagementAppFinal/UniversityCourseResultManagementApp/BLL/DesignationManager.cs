using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityCourseResultManagementApp.Gateway;
using UniversityCourseResultManagementApp.Models;

namespace UniversityCourseResultManagementApp.Manager
{
    public class DesignationManager
    {
        public List<Designation> GetAllDesignations()
        {
            DesignationGateway designationGateway = new DesignationGateway();
            return designationGateway.GetAllDesignations();
        }
    }
}