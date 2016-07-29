using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityCourseResultManagementApp.Manager;
using UniversityCourseResultManagementApp.Models;

namespace UniversityCourseResultManagementApp.Controllers
{
    public class StudentController : Controller
    {
        StudentManager studentManager = new StudentManager();
        DepartmentManager departmentManager = new DepartmentManager();
        // GET: Student
        public ActionResult Register()
        {
            ViewBag.listOfDepartments = departmentManager.GetAllDepartments();
            return View();
        }

        [HttpPost]

        public ActionResult Register(Student student)
        {
            //string message = studentManager.SaveStudent(student);
            ViewBag.listOfDepartments = studentManager.GetAllDepartments();
            ViewBag.Message = studentManager.SaveStudent(student);
            ViewBag.RegNo = student;
            ViewBag.DeptName = departmentManager.GetDepartmentNameByID(student.DepartmentId);
            //ViewBag.Message = message;
            ModelState.Clear();
            return View();

        }

    }
}