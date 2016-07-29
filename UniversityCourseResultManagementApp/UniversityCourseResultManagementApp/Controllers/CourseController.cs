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
    public class CourseController : Controller
    {
        // GET: Course
        CourseManager courseManager = new CourseManager();
        CourseAssignViewManager courseAssignViewManager = new CourseAssignViewManager();
        DepartmentManager departmentManager = new DepartmentManager();
        SemesterManager semesterManager = new SemesterManager();
        CourseEnrollManager aCourseEnrollManager = new CourseEnrollManager();
        TeacherManager teacherManager = new TeacherManager();
        
        public ActionResult Save()
        {
            List<Department> department = departmentManager.GetAllDepartments();
            ViewBag.departments = department;
            List<Semester> semester = semesterManager.GetAllSemesters();
            ViewBag.semesters = semester;
            return View();
        }
        
        [HttpPost]
        public ActionResult Save(Course course)
        {
            List<Department> department = departmentManager.GetAllDepartments();
            ViewBag.departments = department;
            List<Semester> semester = semesterManager.GetAllSemesters();
            ViewBag.semesters = semester;
            ViewBag.Message = courseManager.SaveCourse(course);
            ModelState.Clear();
            return View();
        }

        public JsonResult CheckCodeIsExists(string code)
        {
            string message = courseManager.IsCourseCodeExist(code);
            return Json(message, JsonRequestBehavior.AllowGet);
        }
        public ActionResult CourseAssign()
        {
            ViewBag.Departments = departmentManager.GetAllDepartments();
            return View();
        }


        [HttpPost]
        public ActionResult CourseAssign(CourseAssignToTeacher courseAssign)
        {
            ViewBag.Message = courseManager.AssignCourseToATeacher(courseAssign);
            ViewBag.Departments = departmentManager.GetAllDepartments();
            return View();
        }

       
        public JsonResult GetCourseNameAndCreditByCourseId(int courseId)
        {
            var data = courseManager.GetCourseByCourseId(courseId);
            return Json(data, JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetCourseCodeByDeptId(int departmentId)
        {
            var data = courseManager.GetCourseCodeByDeptId(departmentId);
            return Json(data, JsonRequestBehavior.AllowGet);
        }



        public JsonResult GetTakenCreditByDepartmentIdAndTeacherId(int deptId, int teacherId)
        {
            var data = courseAssignViewManager.GetTakenCredit(deptId, teacherId);
            return Json(data, JsonRequestBehavior.AllowGet);
        }



        [HttpGet]
        public ActionResult UnassignCourse()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UnassignCourse(string assigned)
        {
            string assigned2 = "Unassigned";
            assigned = assigned2;
            ViewBag.Message = aCourseEnrollManager.UnassignCourse(assigned);
            return View();
        }
    }
}