using HRAttendance.Models.Account;
using HRAttendance.Models.Attendance.Request;
using HRAttendance.Models.Attendance.Response;
using HRAttendance.Resources.Common;
using HRAttendance.Utils;
using HRCommon.Exceptions;
using HRCommon.Utils;
using HRDataAccess.DataAccess;
using HRDataAccess.Models.AttendanceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HRAttendance.Services
{
    public class AttendanceServices : BaseServices
    {
        public ResCheckInOutInfo SetCheckInOutInfo(ReqAttendanceSetInfo model, LoginUserInfo userInfo)
        {
            ResCheckInOutInfo response = new ResCheckInOutInfo();

            try
            {
                ModelValidator.ValidateCheckInOutInfo(model);

                // check employee is active
                EmployeeDao.CheckEmployeeExist(userInfo.EmployeeID);

                bool isAlreadyCheckIn = AttendanceDao.CheckEmployeeAlreadyCheckedIn(userInfo.EmployeeID);

                if (model.CheckInOutStatus == eCheckInOutStatus.CheckIn && isAlreadyCheckIn)
                {
                    response.Result = CreateResult(Constants.ACK_RESULT, CommonMessage.MSG_Success);
                    return response;
                }

                if (model.CheckInOutStatus == eCheckInOutStatus.CheckOut && !isAlreadyCheckIn)
                {
                    throw new AppException(AppException.MSG_NOT_CHECKEDIN);
                }

                AttendanceDao.SetCheckInOutStatus(userInfo.EmployeeID, (byte)model.CheckInOutStatus);

                response.Result = CreateResult(Constants.ACK_RESULT, CommonMessage.MSG_Success);
            }
            catch (Exception ex)
            {
                response.Result = CreateResult(Constants.NACK_RESULT, ex.Message);
            }

            return response;
        }

        internal ResGetCheckInOutInfo GetCheckInOutInfo(LoginUserInfo loginUserInfo)
        {
            ResGetCheckInOutInfo response = new ResGetCheckInOutInfo();

            try
            {
                EmployeeDao.CheckEmployeeExist(loginUserInfo.EmployeeID);

                AttendanceDao.GetCheckInOutInfo(loginUserInfo.EmployeeID, out bool isCheckIn, out bool isCheckOut);

                response.IsCheckIn = isCheckIn;
                response.IsCheckOut = isCheckOut;

                response.Result = CreateResult(Constants.ACK_RESULT);
            }
            catch (Exception ex)
            {
                response.Result = CreateResult(Constants.NACK_RESULT, ex.Message);
            }

            return response;
        }

        internal ResGetEmpAttenanceInfoList GetEmpAttendanceInfoList(LoginUserInfo loginUserInfo)
        {
            ResGetEmpAttenanceInfoList response = new ResGetEmpAttenanceInfoList();

            try
            {
                EmployeeDao.CheckEmployeeExist(loginUserInfo.EmployeeID);

                List<EmpEmployeeAttendanceDBInfo> dbInfoList = AttendanceDao.GetEmpAttendanceInfoList(loginUserInfo.EmployeeID);

                response.AttendanceInfos = ModelConverter.ConvertUIEmpAttendanceInfoList(dbInfoList);

                response.Result = CreateResult(Constants.ACK_RESULT);
            }
            catch (Exception ex)
            {
                response.Result = CreateResult(Constants.NACK_RESULT, ex.Message);
            }

            return response;
        }
    }
}