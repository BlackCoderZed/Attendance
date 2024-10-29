using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HRAttendance.Utils
{
    public class Constants
    {
        public const string DB_CONFIG_PATH = @"App_Data\DBConfig.xml";

        public const string ACK_RESULT = "1";

        public const string NACK_RESULT = "2";

        public const string SESSION_WEB_LOGIN_INFO = "SESSION_WEB_LOGIN_INFO";

        public const int ManagementPermissionID = 9999;

        public const string ManagementPermissionName = "Management";
    }
}