using HRAttendance.Models.Common;
using HRAttendance.Models.Employee;
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

        
    }
}