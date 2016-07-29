using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityCourseResultManagementApp.DAL;
using UniversityCourseResultManagementApp.Models;

namespace UniversityCourseResultManagementApp.BLL
{
    public class AllocateClassRoomManager
    {
        AllocateClassRoomGateway aClassRoomGateway = new AllocateClassRoomGateway();
        public string AllocateClassRoom(AllocateClassRoom aClassRoom)
        {

            if (aClassRoom.StartTime >= aClassRoom.EndTime)
            {
                return "End Time Must Be Grater Than Start Time!";
            }
            else
            {
                bool isTimeScheduleValid = IsTimeScheduleValid(aClassRoom.RoomId, aClassRoom.DayId, aClassRoom.StartTime, aClassRoom.EndTime);

                if (isTimeScheduleValid != true)
                {

                    if (aClassRoomGateway.AllocateClassRoom(aClassRoom) > 0)
                    {
                        return "Class Allocated Sucessfully!";
                    }
                    else
                    {
                        return "Class Allocation Failed... Please Try Again";
                    }
                }
                else
                {
                    return "Class Overlapping Is Invalid";
                }
            }
        }

        private bool IsTimeScheduleValid(int roomId, int dayId, DateTime startTime, DateTime endTime)
        {
            List<AllocateClassRoom> schedule = aClassRoomGateway.GetClassSchedulByStartAndEndingTime(roomId, dayId, startTime, endTime);
            foreach (var sd in schedule)
            {
                if ((sd.DayId == dayId && roomId == sd.RoomId) &&
                                 (startTime < sd.StartTime && endTime > sd.StartTime)
                                 || (startTime < sd.StartTime && endTime > sd.StartTime) ||
                                 (startTime == sd.StartTime) || (sd.StartTime < startTime && sd.EndTime > startTime)
                                 )
                {
                    return true;
                }

            }
            return false;
        }

        public string GetAllClassSchedulesByDeparmentId(int departmentId, int courseId)
        {
            IEnumerable<ViewClassScheduleInfo> classSchedules = aClassRoomGateway.GetAllClassSchedulesByDeparmentId(departmentId, courseId);

            string output = "";

            foreach (var acls in classSchedules)
            {

                if (acls.RoomName.StartsWith("R"))
                {
                    output += acls.RoomName + ", " + acls.DayName + ", " + acls.StartTime.ToShortTimeString() + " - " + acls.EndTime.ToShortTimeString() + ";<br />";
                }

                else if (acls.RoomName.StartsWith("N"))
                {
                    output = acls.RoomName;

                }


            }

            return output;
        }

        public String UnAllocateClassRoom(int data)
        {
            if (aClassRoomGateway.UnAllocateClassRoom(data) > 0)
            {
                return "All Class Unallocated Sucessfully...";
            }
            return "Failed to Unallocate Classes... You May Try Again.";
        }

    }
}