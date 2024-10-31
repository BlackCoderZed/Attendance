using HRAttendance.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HRAttendance.Models.Role.Response
{
    public class ResUserRoleInfo : ResultBase
    {
        public int UserID { get; set; }

        public string UserName { get; set; }

        public string[] Roles { get; set; }
    }
}