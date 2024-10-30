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

        public const string EmailRegex = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

        public const string PhoneRegex1 = @"^0\d{10}$";

        public const string PhoneRegex2 = @"^\+^0\d{12}$";

        public const int MIN_INPUT_LENGHT = 0;

        public const int MAX_NAME_LENGHT = 50;

        public const int MAX_NRC_LENGHT = 50;

        public const int MAX_PHONE_LENGTH = 15;

        public const int MAX_EMAIL_LENGHT = 50;

        public const int MAX_USERID_LENGHT = 20;

        public const string DEFAULT_PASSWORD = "1234567890";
    }
}