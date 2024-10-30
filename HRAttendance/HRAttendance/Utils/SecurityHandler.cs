using HRAttendance.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HRAttendance.Utils
{
    public class SecurityHandler : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // log 
            //ExectionLog(filterContext);

            string controller = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            //string action = filterContext.ActionDescriptor.ActionName;
            if (filterContext.HttpContext.Session[Constants.SESSION_WEB_LOGIN_INFO] == null && controller != "Account")
            {
                if (filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    JsonResult jsonResult = new JsonResult();
                    jsonResult.Data = new JsonResponse() { Success = false, Message = "Session expired", RedirectToLogin = true };
                    jsonResult.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                    filterContext.Result = jsonResult;
                }
                else
                {
                    filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary
                    {
                        { "controller", "Account" },
                        { "action", "Login" }
                    });
                }
            }
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            //ExecutedLog(filterContext);

            Application_PreSendRequestHeaders();
        }

        //private void ExectionLog(ActionExecutingContext filterContext)
        //{
        //    string logMsg = string.Format("[Execution]\t[{0}]\tURL : {1}\tIP Address : {2}\tHostname : {3}\t", filterContext.HttpContext.Request.RequestType,
        //            filterContext.HttpContext.Request.RawUrl,
        //            filterContext.HttpContext.Request.UserHostAddress,
        //            filterContext.HttpContext.Request.UserHostName);
        //    logMsg += string.Format("Browser : {0}\tMobile Device : {1}\tBrowser Agent : {2}",
        //        filterContext.HttpContext.Request.Browser.Browser,
        //        filterContext.HttpContext.Request.Browser.IsMobileDevice,
        //        filterContext.HttpContext.Request.UserAgent);

        //    LogUIHelper.Develop(null, logMsg);
        //}

        //private void ExecutedLog(ActionExecutedContext filterContext)
        //{
        //    string logMsg = string.Format("[Executed]\t[{0}]\tURL : {1}\tIP Address : {2}\tHostname : {3}\t", filterContext.HttpContext.Request.RequestType,
        //            filterContext.HttpContext.Request.RawUrl,
        //            filterContext.HttpContext.Request.UserHostAddress,
        //            filterContext.HttpContext.Request.UserHostName);
        //    logMsg += string.Format("Browser : {0}\tMobile Device : {1}\tBrowser Agent : {2}",
        //        filterContext.HttpContext.Request.Browser.Browser,
        //        filterContext.HttpContext.Request.Browser.IsMobileDevice,
        //        filterContext.HttpContext.Request.UserAgent);

        //    LogUIHelper.Develop(null, logMsg);
        //}

        #region Server Header 

        protected void Application_PreSendRequestHeaders()
        {
            if (HttpContext.Current != null)
            {
                HttpContext.Current.Response.Headers.Remove("Server");
            }
        }

        #endregion
    }
}