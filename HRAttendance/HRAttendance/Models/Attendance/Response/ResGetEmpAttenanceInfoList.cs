using HRAttendance.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HRAttendance.Models.Attendance.Response
{
    public class ResGetEmpAttenanceInfoList : ResultBase
    {
        public List<EmpEmployeeAttendanceInfo> AttendanceInfos { get; set; }
    }
}