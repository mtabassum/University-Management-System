using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityCourseResultManagementApp.Gateway;
using UniversityCourseResultManagementApp.Models;

namespace UniversityCourseResultManagementApp.Manager
{
    public class DepartmentManager
    {
        public string Save(Department department)
        {
            DepartmentGateway departmentGateway = new DepartmentGateway();
            if (IsDepartmentCodeExist(department.Code))
            {
                return "Department Code Already Exists!";
            }
            else
            {
                if (DepartmentCodeValidation(department.Code) == false)
                {
                    return "Code Must Be Two to Seven Characters Long";
                }

                else if (IsDepartmentNameExist(department.Name))
                {
                    return "Department Name Already Exists!";
                }
                if (DepartmentCodeValidation(department.Code) == true)
                {
                    int rowAffected = departmentGateway.Save(department);
                    if (rowAffected > 0)
                    {
                        return "Department Saved Successfully...";
                    }
                    else
                    {
                        return "Department Saved Failed !";
                    }
                }
            }
            
            return departmentGateway.Save(department).ToString();
        }
        bool IsDepartmentCodeExist(string code)
        {
            DepartmentGateway departmentGateway = new DepartmentGateway();
            return departmentGateway.IsDepartmentCodeExist(code);
        }
        bool IsDepartmentNameExist(string name)
        {
            DepartmentGateway departmentGateway = new DepartmentGateway();
            return departmentGateway.IsDepartmentNameExist(name);
        }
        bool DepartmentCodeValidation(string deptCode)
        {
            if (deptCode.Length < 2 || deptCode.Length > 7)
            {
                return false;
            }
            return true;
        }

        public List<Department> GetAllDepartments()
        {
            DepartmentGateway departmentGateway = new DepartmentGateway();
            return departmentGateway.GetAllDepartments();
        }

        public Department GetDeptNameById(int deptId)
        {
            DepartmentGateway departmentGateway = new DepartmentGateway();
            return departmentGateway.GetDeptNameById(deptId);
        }
        public string GetDepartmentNameByID(int deptId)
        {
            DepartmentGateway departmentGateway = new DepartmentGateway();
            return departmentGateway.GetDepartmentNameByID(deptId);
        }

    }
}