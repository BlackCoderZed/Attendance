using HRAttendance.Models.Account;
using HRAttendance.Models.Account.Response;
using HRAttendance.Services;
using HRAttendance.Utils;
using HRCommon.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace HRAttendance.Controllers
{
    public class AccountController : BaseController
    {
        new public LoginUserInfo LoginUserInfo
        {
            get { return GetLoginUserInfo(); }
            set
            {
                Session[Constants.SESSION_WEB_LOGIN_INFO] = value;
            }
        }

        private LoginUserInfo GetLoginUserInfo()
        {
            if (Session[Constants.SESSION_WEB_LOGIN_INFO] == null)
            {
                throw new AppException("Invalid login");
            }
            return (LoginUserInfo)Session[Constants.SESSION_WEB_LOGIN_INFO];
        }

        // GET: Account
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginUserInfo model)
        {
            // login
            AccountServices services = new AccountServices();
            ResUserLoginInfo response = services.Login(model);

            // redirect
            if (response.Result.ResultCode == Constants.NACK_RESULT)
            {
                TempData["InvalidLogin"] = response.Result.ResultMessage;
                TempData.Keep();
                return RedirectToAction("Login", "Account");
            }

            // web login
            if (User.Identity.IsAuthenticated)
            {
                FormsAuthentication.SignOut();
            }

            FormsAuthentication.SetAuthCookie(response.LoginUserInfo.UserName.ToString(), true);
            LoginUserInfo = response.LoginUserInfo;
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public ActionResult Logout()
        {
            if (User.Identity.IsAuthenticated)
            {
                FormsAuthentication.SignOut();
            }

            if (GetLoginUserInfo() != null)
            {
                // clear session
                Session.Clear();
                Session.Abandon();
            }

            return RedirectToAction("Login", "Account");
        }
    }
}