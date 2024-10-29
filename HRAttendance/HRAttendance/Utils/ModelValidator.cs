using HRAttendance.Models.Account;
using HRAttendance.Resources.Common;
using HRCommon.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HRAttendance.Utils
{
    public class ModelValidator
    {
        internal static void ValidateLoginUserInfo(LoginUserInfo userInfo)
        {
            try
            {
                if (userInfo == null)
                {
                    throw new AppException(CommonMessage.MSG_InvalidValue);
                }

                if (string.IsNullOrEmpty(userInfo.UserName) || string.IsNullOrWhiteSpace(userInfo.UserName))
                {
                    throw new AppException(string.Format(CommonMessage.MSG_ValueEmpty, nameof(userInfo.UserName)));
                }

                if (string.IsNullOrEmpty(userInfo.Password) || string.IsNullOrWhiteSpace(userInfo.Password))
                {
                    throw new AppException(string.Format(CommonMessage.MSG_ValueEmpty, nameof(userInfo.Password)));
                }

                if (userInfo.UserName.Length <= 0 && userInfo.UserName.Length > 50)
                {
                    throw new AppException(string.Format(CommonMessage.MSG_InvalidCharCount, nameof(userInfo.UserName)));
                }

                if (userInfo.Password.Length <= 0 && userInfo.Password.Length > 50)
                {
                    throw new AppException(string.Format(CommonMessage.MSG_InvalidCharCount, nameof(userInfo.Password)));
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}