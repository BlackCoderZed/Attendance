using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRDataAccess.Models.Account
{
    public class DbLoginUserInfo
    {
        public int Id { get; set; }

        public string UserID { get; set; }

        public string Password { get; set; }

        public string UserDlpName { get; set; }

        public int EmployeeID { get; set; }
    }
}
