using HRAttendance.Models.Attendance.Request;
using HRAttendance.Models.Attendance.Response;
using HRAttendance.Models.Common;
using HRAttendance.Services;
using HRAttendance.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HRAttendance.Controllers
{
    [Authorize]
    public class AttendanceController : BaseController
    {
        // GET: Attendance
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CheckInOut(ReqAttendanceSetInfo model)
        {
            AttendanceServices services = new AttendanceServices();
            ResCheckInOutInfo response = services.SetCheckInOutInfo(model, LoginUserInfo);

            return Json(new JsonResponse() { Success = response.Result.ResultCode == Constants.ACK_RESULT, 
                Message = response.Result.ResultMessage });
        }

        [HttpGet]
        public ActionResult GetCheckInOutInfo()
        {
            AttendanceServices services = new AttendanceServices();
            ResGetCheckInOutInfo response = services.GetCheckInOutInfo(LoginUserInfo);

            return Json(new {Success = response.Result.ResultCode == Constants.ACK_RESULT, 
                Message = response.Result.ResultMessage, IsCheckIn = response.IsCheckIn, 
                IsCheckOut = response.IsCheckOut }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetEmpAttendanceInfoList()
        {
            AttendanceServices services = new AttendanceServices();
            ResGetEmpAttenanceInfoList response = services.GetEmpAttendanceInfoList(LoginUserInfo);

            return Json(new GridviewJsonResponse() { Success = response.Result.ResultCode == Constants.ACK_RESULT,
                Message = response.Result.ResultMessage, data = response.AttendanceInfos }, JsonRequestBehavior.AllowGet);
        }
    }
}