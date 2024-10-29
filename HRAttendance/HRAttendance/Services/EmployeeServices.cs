using HRAttendance.Models.Common;
using HRAttendance.Models.Employee.Response;
using HRAttendance.Utils;
using HRDataAccess.DataAccess;
using HRDataAccess.Models.Common;
using HRDataAccess.Models.EmployeeModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HRAttendance.Services
{
    public class EmployeeServices : BaseServices
    {
        internal ResGetEmployeeInfoList GetEmployeeInfoList(GridviewFilterInfo gridViewModel)
        {
            ResGetEmployeeInfoList response = new ResGetEmployeeInfoList();

            try
            {
                DBGridviewFilterInfo filterInfo = ModelConverter.CreateDBGridviewFilter(gridViewModel);
                List<EmployeeDBInfo> dbEmployeeList = EmployeeDao.GetEmployeeInfoList(filterInfo, out int totalCount);

                response.TotalRecordCount = totalCount;
                response.EmployeeInfoList = ModelConverter.CreateUIEmployeeInfoList(dbEmployeeList);

                response.Result = CreateResult(Constants.ACK_RESULT);
            }
            catch(Exception ex)
            {
                response.Result = CreateResult(Constants.NACK_RESULT, ex.Message);
            }

            return response;
        }
    }
}