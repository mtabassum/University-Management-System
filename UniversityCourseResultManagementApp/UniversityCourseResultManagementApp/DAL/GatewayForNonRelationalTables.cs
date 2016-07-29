using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using UniversityCourseResultManagementApp.Gateway;
using UniversityCourseResultManagementApp.Models;

namespace UniversityCourseResultManagementApp.DAL
{
    public class GatewayForNonRelationalTables:CommonGateway
    {
        public List<Day> GetAllDay()
        {
            string query = "SELECT * FROM [Days]";
            Command = new SqlCommand(query,connection);
            connection.Open();
            SqlDataReader Reader = Command.ExecuteReader();
            List<Day> dayList = new List<Day>();
            while (Reader.Read())
            {
                Day aDay = new Day();
                aDay.Id = int.Parse(Reader["Id"].ToString());
                aDay.Name = Reader["Name"].ToString();
                dayList.Add(aDay);
            }
            Reader.Close();
            connection.Close();

            return dayList;
        }

        public List<Room> GetAllRoom()
        {
            string query = "SELECT * FROM Room";
            Command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader Reader = Command.ExecuteReader();
            List<Room> roomList = new List<Room>();
            while (Reader.Read())
            {
                Room aRoom = new Room();
                aRoom.Id = int.Parse(Reader["Id"].ToString());
                aRoom.Name = Reader["Name"].ToString();
                roomList.Add(aRoom);
            }
            Reader.Close();
            connection.Close();

            return roomList;
        }
    }
}