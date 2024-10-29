using HRAttendance.Models.Common;

namespace HRAttendance.Services
{
    public class BaseServices
    {
        protected Result CreateResult(string code, string message = null)
        {
            Result result = new Result();

            result.ResultCode = code;
            result.ResultMessage = message;
            return result;
        }
    }
}