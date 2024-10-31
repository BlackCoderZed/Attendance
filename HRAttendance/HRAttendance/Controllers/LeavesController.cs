using HRAttendance.Models.Leave.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HRAttendance.Controllers
{
    public class LeavesController : Controller
    {
        // GET: Leaves
        public ActionResult Index()
        {
            ReqRequestLeaveInfo reqInfo = new ReqRequestLeaveInfo();
            reqInfo.LeveInfos = new List<Models.Leave.LeaveInfo>();
            return View(reqInfo);
        }
    }
}