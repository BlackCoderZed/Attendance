using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRDataAccess.Models.EmployeeModels
{
    public class EmployeeCreateDBInfo
    {
        public bool IsUpdate { get; set; }

        public int EmployeeID { get; set; }

        public string Name { get; set; }

        public string NRC { get; set; }

        public byte Gender { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public string UserID { get; set; }

        public string Password { get; set; }

        public int UserIDVal { get; set; }

        public int ShiftID { get; set; }

        public List<int> Permissions { get; set; }
    }
}
