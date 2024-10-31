using HRAttendance.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HRAttendance.Models.Attendance.Response
{
    public class ResGetCheckInOutInfo : ResultBase
    {
        public bool IsCheckIn { get; set; }

        public bool IsCheckOut { get; set; }
    }
}