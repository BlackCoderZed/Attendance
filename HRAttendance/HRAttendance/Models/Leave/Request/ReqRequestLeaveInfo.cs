using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HRAttendance.Models.Leave.Request
{
    public class ReqRequestLeaveInfo
    {
        public int ID { get; set; }

        public DateTime StartDate { get; set; } = DateTime.Now;

        public DateTime EndDate { get; set; } = DateTime.Now;

        public int LeveTypeID { get; set; }

        public List<LeaveInfo> LeveInfos { get; set; }

        public string Description { get; set; }
    }
}