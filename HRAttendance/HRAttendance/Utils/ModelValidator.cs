using HRAttendance.Models.Account;
using HRAttendance.Models.Employee.Request;
using HRAttendance.Resources.Common;
using HRCommon.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace HRAttendance.Utils
{
    public class ModelValidator
    {
        /// <summary>
        /// Validate Employee Info
        /// </summary>
        /// <param name="model"></param>
        internal static void ValidateCreateEmployeeInfo(EmployeeCreateInfo model)
        {
            try
            {
                if (model == null)
                {
                    throw new AppException(CommonMessage.MSG_InvalidValue);
                }

                if (string.IsNullOrEmpty(model.Name) || string.IsNullOrWhiteSpace(model.Name))
                {
                    throw new AppException(string.Format(CommonMessage.MSG_ValueEmpty, nameof(model.Name)));
                }

                if (string.IsNullOrEmpty(model.NRC) || string.IsNullOrWhiteSpace(model.NRC))
                {
                    throw new AppException(string.Format(CommonMessage.MSG_ValueEmpty, nameof(model.NRC)));
                }

                if (string.IsNullOrEmpty(model.Phone) || string.IsNullOrWhiteSpace(model.Phone))
                {
                    throw new AppException(string.Format(CommonMessage.MSG_ValueEmpty, nameof(model.Phone)));
                }

                if (string.IsNullOrEmpty(model.Email) || string.IsNullOrWhiteSpace(model.Email))
                {
                    throw new AppException(string.Format(CommonMessage.MSG_ValueEmpty, nameof(model.Email)));
                }

                if (string.IsNullOrEmpty(model.Address) || string.IsNullOrWhiteSpace(model.Address))
                {
                    throw new AppException(string.Format(CommonMessage.MSG_ValueEmpty, nameof(model.Address)));
                }

                if (string.IsNullOrEmpty(model.UserID) || string.IsNullOrWhiteSpace(model.UserID))
                {
                    throw new AppException(string.Format(CommonMessage.MSG_ValueEmpty, nameof(model.UserID)));
                }

                if (model.IsUpdate == false && (string.IsNullOrEmpty(model.Password) || string.IsNullOrWhiteSpace(model.Password)))
                {
                    throw new AppException(string.Format(CommonMessage.MSG_ValueEmpty, nameof(model.Password)));
                }

                // regex check
                if (!Regex.IsMatch(model.Email, Constants.EmailRegex))
                {
                    throw new AppException(string.Format(CommonMessage.MSG_InvalidEnterValue, nameof(model.Email)));
                }

                // regex check
                if (!Regex.IsMatch(model.Phone, Constants.PhoneRegex1) && !Regex.IsMatch(model.Phone, Constants.PhoneRegex2))
                {
                    throw new AppException(string.Format(CommonMessage.MSG_InvalidEnterValue, nameof(model.Phone)));
                }

                // length check
                if (model.Name.Length <= Constants.MIN_INPUT_LENGHT || model.Name.Length > Constants.MAX_NAME_LENGHT)
                {
                    throw new AppException(string.Format(CommonMessage.MSG_InvalidCharCount, nameof(model.Name)));
                }

                if (model.NRC.Length <= Constants.MIN_INPUT_LENGHT || model.NRC.Length > Constants.MAX_NRC_LENGHT)
                {
                    throw new AppException(string.Format(CommonMessage.MSG_InvalidCharCount, nameof(model.NRC)));
                }

                if (model.Phone.Length <= Constants.MIN_INPUT_LENGHT || model.Phone.Length > Constants.MAX_PHONE_LENGTH)
                {
                    throw new AppException(string.Format(CommonMessage.MSG_InvalidCharCount, nameof(model.Phone)));
                }

                if (model.Email.Length <= Constants.MIN_INPUT_LENGHT || model.Email.Length > Constants.MAX_EMAIL_LENGHT)
                {
                    throw new AppException(string.Format(CommonMessage.MSG_InvalidCharCount, nameof(model.Email)));
                }

                if (model.UserID.Length <= Constants.MIN_INPUT_LENGHT || model.UserID.Length > Constants.MAX_USERID_LENGHT)
                {
                    throw new AppException(string.Format(CommonMessage.MSG_InvalidCharCount, nameof(model.UserID)));
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

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