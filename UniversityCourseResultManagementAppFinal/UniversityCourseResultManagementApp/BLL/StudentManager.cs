using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityCourseResultManagementApp.Gateway;
using UniversityCourseResultManagementApp.Models;

namespace UniversityCourseResultManagementApp.Manager
{
    public class StudentManager
    {
        StudentGateway studentGateway = new StudentGateway();
        DepartmentManager departmentManager = new DepartmentManager();
        DepartmentGateway departmentGateway = new DepartmentGateway();

        public int GetNumberOfStudentByDeptId(int deptId)
        {
            return studentGateway.GetNumberOfStudentByDeptId(deptId);
        }

        public List<Department> GetAllDepartments()
        {
            return departmentManager.GetAllDepartments();
        }

        public string GetLastAddedStudentRegistration(string searchKey)
        {
            return studentGateway.GetLastAddedStudentRegistration(searchKey);

        }

        public IEnumerable<Student> GetAllStudents
        {
            get { return studentGateway.GetAllStudents(); }
        }
        public string SaveStudent(Student aStudent)
        {
            //    int counter;
            //    Department department = departmentManager.GetAllDepartments().Single(depid => depid.Id == aStudent.DepartmentId);

            //    string searchKey = department.Code + "-" + aStudent.RegDate.Year + "-";
            //    string lastAddedRegistrationNo = GetLastAddedStudentRegistration(searchKey);
            //    if (lastAddedRegistrationNo == null)
            //    {
            //        aStudent.RegNo = searchKey + "001";

            //    }

            //    if (lastAddedRegistrationNo != null)
            //    {
            //        string tempId = lastAddedRegistrationNo.Substring((lastAddedRegistrationNo.Length - 3), 3);
            //        counter = Convert.ToInt32(tempId);
            //        string studentSl = (counter + 1).ToString("000");


            //        if (studentSl.Length == 1)
            //        {

            //            aStudent.RegNo = searchKey + "00" + studentSl;

            //        }
            //        else if (studentSl.Count() == 2)
            //        {

            //            aStudent.RegNo = searchKey + "0" + studentSl;
            //        }
            //        else
            //        {

            //            aStudent.RegNo = searchKey + studentSl;
            //        }

            //    }
            //    var listOfEmailAddress = from student in GetAllStudents select student.Email;
            //    string tempEmail = listOfEmailAddress.ToList().Find(email => email.Contains(aStudent.Email));

            //    if (tempEmail != null)
            //    {
            //        return "Email address must be unique";
            //    }

            //    if (studentGateway.SaveStudent(aStudent) > 0)
            //    {
            //        return "Saved Successfully!;Registration No:" + aStudent.RegNo + ";Name:" + aStudent.Name + ";Email:" +
            //               aStudent.Email + ";Contact N0.:" + aStudent.ContactNo + ";Address:" + aStudent.Address +
            //               ";DepartmentId:" + aStudent.DepartmentId;
            //    }

            //    return "Failed to save";

            var listOfEmailAddress = from student in GetAllStudents select student.Email;
        string tempEmail = listOfEmailAddress.ToList().Find(email => email.Contains(aStudent.Email));
        string message = "";
            if (tempEmail != null)
            {
                return "Email address must be unique!";
            }
            if (tempEmail == null)
            {
                string fullDeptName = departmentGateway.GetDepartmentNameByID(aStudent.DepartmentId);
                string deptName = fullDeptName;

                int rowAffected = studentGateway.SaveStudent(aStudent);                
                if (rowAffected > 0)
                {
                    message = "Student Registration Successfully Completed!;Registration No:" + aStudent.RegNo + ";Name:" + aStudent.Name + ";Email:" +
                           aStudent.Email + ";Contact No.:" + aStudent.ContactNo + ";Address:" + aStudent.Address +
                           ";DepartmentId:" + deptName;

                }
                else
                {
                    message = "Student Registration Failed";
                }
            }
            else
            {
                message = "Email already Exists. Please Try With New One";
            }

           
            return message;
        }

        public List<ViewStudentResult> ViewStudentResultByID(int Id)
        {
            return studentGateway.ViewStudentResultByID(Id);
        }

        public ViewStudentDetails ViewStudentByID(int id)
        {
            return studentGateway.ViewStudentByID(id);
        }
    }
}