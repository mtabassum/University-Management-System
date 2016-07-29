using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using UniversityCourseResultManagementApp.Gateway;
using UniversityCourseResultManagementApp.Models;

namespace UniversityCourseResultManagementApp.DAL
{
    public class AllocateClassRoomGateway : CommonGateway
    {
        public int AllocateClassRoom(AllocateClassRoom aAllocateRoom)
        {
            string query ="INSERT INTO AllocateClassRoom VALUES(@deptId,@courseId,@roomId,@dayId,@startTime,@endTime,@allocate)";

            Command = new SqlCommand(query, connection);
            connection.Open();

            Command.Parameters.Clear();

            Command.Parameters.Add("deptId", SqlDbType.Int);
            Command.Parameters["deptId"].Value = aAllocateRoom.DeptId;

            Command.Parameters.Add("courseId", SqlDbType.Int);
            Command.Parameters["courseId"].Value = aAllocateRoom.CourseId;

            Command.Parameters.Add("roomId", SqlDbType.Int);
            Command.Parameters["roomId"].Value = aAllocateRoom.RoomId;

            Command.Parameters.Add("dayId", SqlDbType.Int);
            Command.Parameters["dayId"].Value = aAllocateRoom.DayId;

            Command.Parameters.Add("startTime", SqlDbType.VarChar);
            Command.Parameters["startTime"].Value = aAllocateRoom.StartTime.ToShortTimeString();

            Command.Parameters.Add("endTime", SqlDbType.VarChar);
            Command.Parameters["endTime"].Value = aAllocateRoom.EndTime.ToShortTimeString();


            Command.Parameters.Add("allocate", SqlDbType.Bit);
            Command.Parameters["allocate"].Value = 1;

            int rowAffected = Command.ExecuteNonQuery();

            connection.Close();

            return rowAffected;
        }

        public List<AllocateClassRoom> GetClassSchedulByStartAndEndingTime(int roomId, int dayId, DateTime startTime,
            DateTime endTime)
        {

            string query = "Select * from AllocateClassRoom Where DayId=" + dayId + " AND RoomId=" + roomId +
                           " AND AllocationStatus=" + 1;
            List<AllocateClassRoom> tempClassSchedules = new List<AllocateClassRoom>();
            Command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader Reader = Reader = Command.ExecuteReader();
            while (Reader.Read())
            {
                AllocateClassRoom temp = new AllocateClassRoom
                {
                    Id = Convert.ToInt32(Reader["Id"].ToString()),
                    DeptId = Convert.ToInt32(Reader["DeptId"].ToString()),
                    CourseId = Convert.ToInt32(Reader["CourseId"].ToString()),
                    RoomId = Convert.ToInt32(Reader["RoomId"].ToString()),
                    DayId = Convert.ToInt32(Reader["DayId"].ToString()),
                    StartTime = Convert.ToDateTime(Reader["StartTime"].ToString()),
                    EndTime = Convert.ToDateTime(Reader["EndTime"].ToString())

                };
                tempClassSchedules.Add(temp);
            }
            Reader.Close();
            connection.Close();
            return tempClassSchedules;

        }

        public int UnAllocateClassRoom(int data)
        {
            try
            {
                string query = "UPDATE AllocateClassRoom SET AllocationStatus=" + data + "";
                Command = new SqlCommand(query, connection);
                connection.Open();
               
                int rowAffected = Command.ExecuteNonQuery();


                return rowAffected;
            }
            catch (Exception exception)
            {
                throw new Exception("Failed to Unallocate Class Room", exception);
            }
            finally
            {
                connection.Close();
            }

        }

        public IEnumerable<ViewClassScheduleInfo> GetAllClassSchedulesByDeparmentId(int departmentId, int courseId)
        {
            try
            {
                List<ViewClassScheduleInfo> scheduleList = new List<ViewClassScheduleInfo>();
                string query = "SELECT * FROM ViewClassScheduleInfo WHERE DepartmentId=" + departmentId + " AND CourseId=" +
                        courseId + " AND AllocationStatus=" + 1 + "";

                Command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader Reader = Reader = Command.ExecuteReader();
                while (Reader.Read())
                {
                    ViewClassScheduleInfo schedule = new ViewClassScheduleInfo
                    {

                        DepartmentId = Convert.ToInt32(Reader["DepartmentId"].ToString()),
                        CourseCode = Reader["CourseCode"].ToString(),
                        CourseName = Reader["CourseName"].ToString(),
                        RoomName = Reader["RoomName"].ToString(),
                        DayName = Reader["DayName"].ToString(),
                        StartTime = Convert.ToDateTime(Reader["StartTime"].ToString()),
                        EndTime = Convert.ToDateTime(Reader["EndTime"].ToString()),
                        AllocationStatus = Convert.ToBoolean(Reader["AllocationStatus"])
                    };

                    scheduleList.Add(schedule);
                }

                Reader.Close();
                return scheduleList;
            }
            catch (Exception exception)
            {
                throw new Exception("Unable To Collect Class Schedule...", exception);
            }
            finally
            {
                connection.Close();
            }
        }
    }
}