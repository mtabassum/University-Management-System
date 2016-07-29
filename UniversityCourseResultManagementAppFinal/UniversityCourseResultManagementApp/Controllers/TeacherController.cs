using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityCourseResultManagementApp.Manager;
using UniversityCourseResultManagementApp.Models;

namespace UniversityCourseResultManagementApp.Controllers
{
    public class TeacherController : Controller
    {
        // GET: Teacher
        DepartmentManager departmentManager = new DepartmentManager();
        DesignationManager designationManager = new DesignationManager();
        TeacherManager teacherManager = new TeacherManager();
        public ActionResult Save()
        {
            ViewBag.Departments = departmentManager.GetAllDepartments();
            ViewBag.Designations = designationManager.GetAllDesignations();
            return View();
        }

        [HttpPost]
        public ActionResult Save(Teacher teacher)
        {
            ViewBag.Message = teacherManager.Save(teacher);
            ViewBag.Departments = departmentManager.GetAllDepartments();
            ViewBag.Designations = designationManager.GetAllDesignations();
            ModelState.Clear();
            return View();
        }

        [HttpPost]
        public JsonResult GetTeacherByTeacherId(int id)
        {
            var data = teacherManager.GetTeacherByTeacherId(id);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        //public JsonResult GetCreditInfoByTeacherId(int teacherId)
        //{
        //    var data = teacherManager.GetCreditInfoByTeacherId(teacherId);
        //    return Json(data, JsonRequestBehavior.AllowGet);
        //}
        public JsonResult GetTeacherDetailsByTeacherId(int teacherId)
        {
            Teacher aTeacher = teacherManager.GetTeacherDetailsByTeacherId(teacherId);
            return Json(aTeacher, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetTeacherByDeptId(int departmentId)
        {
            var data = teacherManager.GetTeacherByDeptId(departmentId);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

    }
}