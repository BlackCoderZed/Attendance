using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRDataAccess.Models.EmployeeModels
{
    public class EmployeeDBInfo
    {
        public int ID { get; set; }

        public string FullName { get; set; }

        public string NRC { get; set; }

        public byte Gender { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public DateTime RegistDateTime { get; set; }
    }
}
