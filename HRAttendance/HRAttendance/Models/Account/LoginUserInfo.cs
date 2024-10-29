using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HRAttendance.Models.Account
{
    public class LoginUserInfo
    {
        public int LoginID { get; set; }

        public string UserDlpName { get; set; } // user dlp name

        public string UserName { get; set; }

        public string Password { get; set; }
    }
}