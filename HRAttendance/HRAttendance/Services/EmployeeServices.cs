using HRAttendance.Models.Account;
using HRAttendance.Models.Common;
using HRAttendance.Models.Employee.Request;
using HRAttendance.Models.Employee.Response;
using HRAttendance.Resources.Common;
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
        internal ResCreateEmployeeInfo CreateEmployeeInfo(EmployeeCreateInfo model, LoginUserInfo userInfo)
        {
            ResCreateEmployeeInfo response = new ResCreateEmployeeInfo();

            try
            {
                ModelValidator.ValidateCreateEmployeeInfo(model);

                if (model.IsUpdate)
                {
                    EmployeeDao.CheckEmployeeExist(model.ID);
                }

                EmployeeDao.CheckExistNRC(model.IsUpdate, model.ID, model.NRC);

                if (!model.IsUpdate) { EmployeeDao.CheckUserIDIsAvailable(model.UserID); }

                EmployeeCreateDBInfo dbInfo = ModelConverter.CreateDBCreateEmployeeInfo(model);

                EmployeeDao.CreateUpdateEmployeeInfo(dbInfo, userInfo.LoginID);

                response.Result = CreateResult(Constants.ACK_RESULT, CommonMessage.MSG_Success);
            }
            catch (Exception ex)
            {
                response.Result = CreateResult(Constants.NACK_RESULT, ex.Message);
            }

            return response;
        }

        internal ResDeleteEmployeeInfo DeleteEmployeeInfo(int id, LoginUserInfo loginUserInfo)
        {
            ResDeleteEmployeeInfo response = new ResDeleteEmployeeInfo();

            try
            {
                EmployeeDao.CheckEmployeeExist(id);

                EmployeeDao.DeleteEmployeeInfo(id, loginUserInfo.LoginID);

                response.Result = CreateResult(Constants.ACK_RESULT, CommonMessage.MSG_Deleted);
            }
            catch (Exception ex)
            {
                response.Result = CreateResult(Constants.NACK_RESULT, ex.Message);
            }

            return response;
        }

        internal ResGetEmployeeInfocs GetEmployeeInfo(int employeeId)
        {
            ResGetEmployeeInfocs response = new ResGetEmployeeInfocs();

            try
            {
                EmployeeDao.CheckEmployeeExist(employeeId);

                EmployeeCreateDBInfo employeeDbInfo = EmployeeDao.GetEmployeeInfo(employeeId);

                response.EmployeeInfo = ModelConverter.CreateUIEmployeeInfo(employeeDbInfo);

                response.Result = CreateResult(Constants.ACK_RESULT);
            }
            catch (Exception ex)
            {
                response.Result = CreateResult(Constants.NACK_RESULT, ex.Message);
            }

            return response;
        }

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