using HRAttendance.Models.Common;
using HRAttendance.Models.Employee;
using HRAttendance.Models.Employee.Request;
using HRAttendance.Models.Employee.Response;
using HRAttendance.Services;
using HRAttendance.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HRAttendance.Controllers
{
    [Authorize(Roles = Constants.ManagementPermissionName)]
    public class EmployeeController : BaseController
    {
        // GET: Employee
        public ActionResult Index()
        {
            List<EmployeeInfo> lstEmployee = new List<EmployeeInfo>();
            return View(lstEmployee);
        }

        [HttpPost]
        public ActionResult GetEmployeeInfoList()
        {
            GridviewFilterInfo gridViewModel = CreateGridviewViewModel(Request.Form, typeof(eEmployeeListView));

            EmployeeServices services = new EmployeeServices();
            ResGetEmployeeInfoList model = services.GetEmployeeInfoList(gridViewModel);

            GridviewJsonResponse response = CreateGridviewJsonResponse(gridViewModel.Draw, model);

            return Json(response);
        }

        [HttpGet]
        public ActionResult Register(bool isUpdate, int employeeId)
        {
            EmployeeCreateInfo model = new EmployeeCreateInfo();

            if (isUpdate)
            {
                EmployeeServices services = new EmployeeServices();
                ResGetEmployeeInfocs resEmployeeInfo = services.GetEmployeeInfo(employeeId);

                if (resEmployeeInfo.Result.ResultCode == Constants.NACK_RESULT)
                {
                    return Json(new JsonResponse() { Success = false, Message = resEmployeeInfo.Result.ResultMessage }, JsonRequestBehavior.AllowGet);
                }

                model = resEmployeeInfo.EmployeeInfo;
                model.IsUpdate = isUpdate;
            }

            string partialView = this.RenderPartialView("~/Views/Employee/EmployeeRegisterDlg.cshtml", model);

            return Json(new JsonResponse() { Success = true, View = partialView }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Register(EmployeeCreateInfo model)
        {
            EmployeeServices services = new EmployeeServices();
            ResCreateEmployeeInfo result = services.CreateEmployeeInfo(model, LoginUserInfo);

            return Json(new JsonResponse() { Success = result.Result.ResultCode == Constants.ACK_RESULT, 
                Message = result.Result.ResultMessage });
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            EmployeeServices services = new EmployeeServices();
            ResDeleteEmployeeInfo response = services.DeleteEmployeeInfo(id, LoginUserInfo);

            return Json(new JsonResponse()
            {
                Success = response.Result.ResultCode == Constants.ACK_RESULT,
                Message = response.Result.ResultMessage
            });
        }
    }
}