using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HRAttendance.Models.Attendance
{
    public class EmpEmployeeAttendanceInfo
    {
        public int AttendanceID { get; set; }

        public string EmployeeName { get; set; }

        public string Date { get; set; }

        public string AttendType { get; set; }

        public string CheckInTime { get; set; }

        public string CheckOutTime { get; set; }
    }
}