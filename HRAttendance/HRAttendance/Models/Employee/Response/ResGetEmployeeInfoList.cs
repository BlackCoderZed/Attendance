using HRAttendance.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HRAttendance.Models.Employee.Response
{
    public class ResGetEmployeeInfoList : ResultBase
    {
        public int TotalRecordCount { get; set; }
        public List<EmployeeInfo> EmployeeInfoList { get; set; }
    }
}