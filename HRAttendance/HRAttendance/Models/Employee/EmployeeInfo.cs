using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HRAttendance.Models.Employee
{
    public class EmployeeInfo
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string NRC { get; set; }

        public string Gender { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public string RegistDateTime { get; set; }
    }
}