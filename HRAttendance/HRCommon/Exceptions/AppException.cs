using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRCommon.Exceptions
{
    public class AppException : Exception
    {
        public const string CODE_NOT_EXIST = "E3202";
        public const string MSG_NOT_EXIST = "{0} information does not exist.";
        public const string CODE_EXIST = "E3201";
        public const string MSG_Exist = "{0} information already existed";

        public const string MSG_FAIL = "Failed to create/update data.";

        public const string MSG_READ_FAIL = "Failed to read data.";

        public const string MSG_INVALID_LOGIN = "Invalid user id or password.";

        public const string MSG_INVALID_Password = "Invalid password.";

        public AppException(string message) : base(message) { }
    }
}
