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
    public class ResultController : Controller
    {
        StudentManager studentManager = new StudentManager();
        CourseEnrollManager courseEnrollManager = new CourseEnrollManager();
        ResultManager ResultManager = new ResultManager();
        
        //
        // GET: /StudentResultSave/
        public ActionResult SaveResult()
        {
            ViewBag.listOfStudentRegNo = studentManager.GetAllStudents.ToList();
            ViewBag.listOfGrade = getAllGradeList();
            return View();
        }

        [HttpPost]
        public ActionResult SaveResult(StudentResultSave studentResultSave)
        {
            ViewBag.listOfStudentRegNo = studentManager.GetAllStudents.ToList();
            ViewBag.listOfGrade = getAllGradeList();
            ViewBag.message = ResultManager.SaveStudentResult(studentResultSave);
            ModelState.Clear();
            return View();
        }

        public JsonResult GetNameEmailDepartmentByStudentId(int studentId)
        {
            var aStudent = courseEnrollManager.GetAllStudentDetails(studentId);
            var studentList = aStudent.Where(student => student.Id == studentId).ToList();
            return Json(studentList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetEnrollCourseByStudentId(int studentId)
        {
            var aStudent = ResultManager.GetAllEnrollCourseFromStudentId(studentId);
            var courseList = aStudent.Where(student => student.StudentId == studentId).ToList();
            return Json(courseList, JsonRequestBehavior.AllowGet);
        }


        private List<SelectListItem> getAllGradeList()
        {
            List<SelectListItem> grade = new List<SelectListItem>
            {
                new SelectListItem {Value = "", Text = "---Select---"},
                new SelectListItem {Value = "A+", Text = "A+"},
                new SelectListItem {Value = "A", Text = "A"},
                new SelectListItem {Value = "A-", Text = "A-"},
                new SelectListItem {Value = "B+", Text = "B+"},
                new SelectListItem {Value = "B", Text = "B"},
                new SelectListItem {Value = "B-", Text = "B-"},
                new SelectListItem {Value = "C+", Text = "C+"},
                new SelectListItem {Value = "C", Text = "C"},
                new SelectListItem {Value = "C-", Text = "C-"},
                new SelectListItem {Value = "D+", Text = "D+"},
                new SelectListItem {Value = "D", Text = "D"},
                new SelectListItem {Value = "D-", Text = "D-"},
                new SelectListItem {Value = "F", Text = "F"},

            };
            return grade;
        }
        [HttpGet]
        public ActionResult ViewResult()
        {
            ViewBag.listOfStudentRegNo = studentManager.GetAllStudents.ToList();
            return View();
        }

        public JsonResult ViewStudentDetails(int StudentId)
        {
            var studentDetails = studentManager.ViewStudentByID(StudentId);
            return Json(studentDetails, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ViewStudentResultByID(int StudentId)
        {
            List<ViewStudentResult> result = studentManager.ViewStudentResultByID(StudentId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

    }

}