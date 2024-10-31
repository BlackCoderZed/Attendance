using HRCommon.Utils;

namespace HRAttendance.Models.Attendance.Request
{
    public class ReqAttendanceSetInfo
    {
        public eCheckInOutStatus CheckInOutStatus { get; set; } // 0 : CheckIn, 1 : CheckOut
    }
}