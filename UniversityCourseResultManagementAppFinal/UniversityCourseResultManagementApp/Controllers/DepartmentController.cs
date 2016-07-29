using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityCourseResultManagementApp.Manager;
using UniversityCourseResultManagementApp.Models;

namespace UniversityCourseResultManagementApp.Controllers
{
    public class DepartmentController : Controller
    {
        DepartmentManager departmentManager = new DepartmentManager();
        // GET: Department

        public ActionResult Save()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Save(Department department)
        {
            ViewBag.Message = departmentManager.Save(department);
            ModelState.Clear();
            return View();
        }

        public ActionResult GetAllDepartments()
        {
            ViewBag.Departments = departmentManager.GetAllDepartments();
            return View();
        }
    }
}