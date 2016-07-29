using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityCourseResultManagementApp.DAL;
using UniversityCourseResultManagementApp.Models;

namespace UniversityCourseResultManagementApp.BLL
{
    public class CourseAssignViewManager
    {
       CourseAssignViewGateway courseAssignViewGateway = new CourseAssignViewGateway();
        

        public double GetTakenCredit(int dId, int tId)
        {
            return courseAssignViewGateway.GetTakenCredit(dId, tId);
        }


        public List<ViewCourseHistory> ViewCourseInformation(int departmentId)
        {
            return courseAssignViewGateway.ViewCourseInformation(departmentId);
        }
    }
}