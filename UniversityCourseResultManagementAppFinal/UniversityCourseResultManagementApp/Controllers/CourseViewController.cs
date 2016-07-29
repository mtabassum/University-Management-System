using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityCourseResultManagementApp.BLL;
using UniversityCourseResultManagementApp.Manager;
using UniversityCourseResultManagementApp.Models;

namespace UniversityCourseResultManagementApp.Controllers
{
    public class CourseViewController : Controller
    {
        // GET: CourseView
        DepartmentManager departmentManager = new DepartmentManager();
        CourseManager courseManager = new CourseManager();
        CourseAssignViewManager courseAssignViewManager = new CourseAssignViewManager();
      

      
        public ActionResult CourseView()
        {
       
            ViewBag.departments = departmentManager.GetAllDepartments();
            return View();
        }
      
        public JsonResult GetCourseView(int departmentId)
        {
    
            
            List<ViewCourseHistory> courseHistoryList = courseAssignViewManager.ViewCourseInformation(departmentId);
            return Json(courseHistoryList, JsonRequestBehavior.AllowGet);


            //foreach (var course in courseList)
            //{
            //    if (course.AssignedTo.Length < 1)
            //    {
            //        course.AssignedTo = "Not Assigned Yet";
            //    }
            //}
            //return Json(courseList, JsonRequestBehavior.AllowGet);  
        }
    }
}