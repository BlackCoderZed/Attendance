using HRAttendance.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HRAttendance.Models.Attendance.Response
{
    public class ResCheckInOutInfo : ResultBase
    {
        public bool CheckInStatus { get; set; }

        public bool CheckOutStatus { get; set; }
    }
}