using HRAttendance.Models.Account;
using HRAttendance.Models.Account.Response;
using HRAttendance.Utils;
using HRDataAccess.DataAccess;
using HRDataAccess.Models.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HRAttendance.Services
{
    public class AccountServices : BaseServices
    {
        public ResUserLoginInfo Login(LoginUserInfo userInfo)
        {
            ResUserLoginInfo response = new ResUserLoginInfo();

            try
            {
                // validate input
                ModelValidator.ValidateLoginUserInfo(userInfo);

                // login
                DbLoginUserInfo dbLoginInfo = UserDao.Login(userInfo.UserName, userInfo.Password);

                // create login cred
                response.LoginUserInfo = CrateLoginUserInfo(dbLoginInfo);

                response.Result = CreateResult(Constants.ACK_RESULT);
            }
            catch (Exception ex)
            {
                response.Result = CreateResult(Constants.NACK_RESULT, ex.Message);
            }

            return response;
        }

        private LoginUserInfo CrateLoginUserInfo(DbLoginUserInfo userInfo)
        {
            LoginUserInfo loginInfo = new LoginUserInfo();
            loginInfo.UserName = userInfo.UserID;
            loginInfo.Password = userInfo.Password;
            loginInfo.UserDlpName = userInfo.UserDlpName;
            loginInfo.LoginID = userInfo.Id;
            loginInfo.EmployeeID = userInfo.EmployeeID;

            return loginInfo;
        }
    }
}