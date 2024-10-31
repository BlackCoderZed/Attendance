using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRDataAccess.Models.AttendanceModels
{
    public class EmpEmployeeAttendanceDBInfo
    {
        public int AttendanceID { get; set; }

        public string EmployeeName { get; set; }

        public DateTime Date { get; set; }

        public byte AttendType { get; set; }

        public TimeSpan? CheckInTime { get; set; }

        public TimeSpan? CheckOutTime { get; set; }
    }
}
