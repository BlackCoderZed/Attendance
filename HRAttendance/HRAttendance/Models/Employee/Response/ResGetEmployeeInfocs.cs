using HRAttendance.Models.Common;
using HRAttendance.Models.Employee.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HRAttendance.Models.Employee.Response
{
    public class ResGetEmployeeInfocs : ResultBase
    {
        public EmployeeCreateInfo EmployeeInfo { get; set; }
    }
}