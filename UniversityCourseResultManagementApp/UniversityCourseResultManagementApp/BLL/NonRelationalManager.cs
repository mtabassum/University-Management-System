using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityCourseResultManagementApp.DAL;
using UniversityCourseResultManagementApp.Models;

namespace UniversityCourseResultManagementApp.BLL
{
    public class NonRelationalManager
    {
        GatewayForNonRelationalTables aNonRelation = new GatewayForNonRelationalTables();
        public List<Day> GetAllDay()
        {
            return aNonRelation.GetAllDay();
        }
        public List<Room> GetAllRoom()
        {
            return aNonRelation.GetAllRoom();
        }
    }
}