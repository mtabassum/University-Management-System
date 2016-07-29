using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityCourseResultManagementApp.DAL;
using UniversityCourseResultManagementApp.Models;

namespace UniversityCourseResultManagementApp.BLL
{
    public class ResultManager
    {
        ResultGateway resultGateway = new ResultGateway();

        public List<ViewEnrollCourseByAStudentId> GetAllEnrollCourseFromStudentId(int studentId)
        {
            return resultGateway.GetAllEnrollCourseFromStudentId(studentId);
        }

        public string SaveStudentResult(StudentResultSave studentResultSave)
        {
            if (resultGateway.FindSameCourseForAStudentWhichAssignAGrade(studentResultSave) == null)
            {

                if (resultGateway.SaveStudentResult(studentResultSave) > 0)
                {
                    return "Save Student Result Successfully.";
                }
                else
                {
                    return "Sorry! Student Result not Save";
                }

            }

            else
            {
                return resultGateway.FindSameCourseForAStudentWhichAssignAGrade(studentResultSave);
            }
        }
    }
}