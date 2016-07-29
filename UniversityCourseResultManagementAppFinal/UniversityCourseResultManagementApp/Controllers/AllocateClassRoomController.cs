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
    public class AllocateClassRoomController : Controller
    {
        DepartmentManager aDepartmentManger = new DepartmentManager();
        CourseManager aCourseManager = new CourseManager();
        NonRelationalManager aNonManager = new NonRelationalManager();
        AllocateClassRoomManager aClassRoomManager = new AllocateClassRoomManager();

      
        [HttpGet]
        public ActionResult AllocateClassRoom()
        {
            ViewBag.Departments = aDepartmentManger.GetAllDepartments();
            ViewBag.Days = aNonManager.GetAllDay();
            ViewBag.Rooms = aNonManager.GetAllRoom();
            return View();
        }
        [HttpPost]
        public ActionResult AllocateClassRoom(AllocateClassRoom aAllocateRoom)
        {
            ViewBag.Departments = aDepartmentManger.GetAllDepartments();
            ViewBag.Days = aNonManager.GetAllDay();
            ViewBag.Rooms = aNonManager.GetAllRoom();
            ViewBag.Message = aClassRoomManager.AllocateClassRoom(aAllocateRoom);
            return View();
        }
        public JsonResult GetCoursesByDeptId(int DeptId)
        {
            var aCourse = aCourseManager.GetCourseCodeByDeptId(DeptId);
            return Json(aCourse, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetClassScheduleInfo()
        {
            ViewBag.Departments = aDepartmentManger.GetAllDepartments();
            return View();
        }
        public JsonResult GetClassScheduleByDepartment(int DeptId)
        {
            var courses = aCourseManager.GetAllCoursesByDeptId(DeptId);

            List<object> clsSches = new List<object>();

            foreach (var course in courses)
            {
                var scheduleInfo = aClassRoomManager.GetAllClassSchedulesByDeparmentId(DeptId, course.Id);
                if (scheduleInfo == "")
                {
                    scheduleInfo = "Not scheduled Yet";
                }


                var clsSch = new
                {
                    CourseCode = course.Code,
                    CourseName = course.Name,
                    ScheduleInfo = scheduleInfo
                };
                clsSches.Add(clsSch);
            }
            return Json(clsSches, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult UnAllocateClass()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UnAllocateClass(int data = 0)
        {
            ViewBag.Message = aClassRoomManager.UnAllocateClassRoom(data);
            return View();
        }
    }
}