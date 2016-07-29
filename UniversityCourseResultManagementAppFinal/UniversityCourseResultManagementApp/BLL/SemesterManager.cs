using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityCourseResultManagementApp.Gateway;
using UniversityCourseResultManagementApp.Models;

namespace UniversityCourseResultManagementApp.Manager
{
    public class SemesterManager
    {
        public List<Semester> GetAllSemesters()
        {
            SemesterGateway semesterGateway = new SemesterGateway();
            return semesterGateway.GetAllSemesters();
        }
    }
}