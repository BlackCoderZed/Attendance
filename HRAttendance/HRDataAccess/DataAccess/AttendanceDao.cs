using HRCommon.Exceptions;
using HRCommon.Utils;
using HRDataAccess.Models;
using HRDataAccess.Models.AttendanceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace HRDataAccess.DataAccess
{
    public class AttendanceDao : BaseDao
    {
        /// <summary>
        /// Check Employee Checked In for today
        /// </summary>
        /// <param name="employeeID"></param>
        /// <returns></returns>
        /// <exception cref="AppException"></exception>
        public static bool CheckEmployeeAlreadyCheckedIn(int employeeID)
        {
            bool isCheckedIn = false;
            try
            {
                using (HRDBEntities context = GetHRDBConnection())
                {
                    DateTime dateTime = DateTime.Now.Date;
                    Attendance attendance = context.Attendances
                        .Where(W => W.EmployeeID == employeeID)
                        .Where(W => W.Date == dateTime)
                        .FirstOrDefault();

                    if (attendance != null)
                    {
                        isCheckedIn = true;
                    }
                }

                return isCheckedIn;
            }
            catch (AppException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new AppException(AppException.MSG_READ_FAIL);
            }
        }

        /// <summary>
        /// GetCheckInOutInfo
        /// </summary>
        /// <param name="employeeID"></param>
        /// <param name="isCheckIn"></param>
        /// <param name="isCheckOut"></param>
        /// <exception cref="NotImplementedException"></exception>
        public static void GetCheckInOutInfo(int employeeID, out bool isCheckIn, out bool isCheckOut)
        {
            try
            {
                isCheckIn = false;
                isCheckOut = false;

                using (HRDBEntities context = GetHRDBConnection())
                {
                    DateTime date = DateTime.Now.Date;

                    Attendance attendance = context.Attendances
                        .Where(W => W.EmployeeID == employeeID)
                        .Where(W => W.Date == date)
                        .FirstOrDefault();

                    if (attendance != null)
                    {
                        isCheckIn = true;
                        isCheckOut = attendance.CheckOutTime.HasValue? true : false;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new AppException(AppException.MSG_READ_FAIL);
            }
        }

        /// <summary>
        /// Get Employee Attendance Info (10 records)
        /// </summary>
        /// <param name="employeeID"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public static List<EmpEmployeeAttendanceDBInfo> GetEmpAttendanceInfoList(int employeeID)
        {
            try
            {
                using (HRDBEntities context = GetHRDBConnection())
                {
                    List<EmpEmployeeAttendanceDBInfo> infoList = context.Attendances
                        .Where(W => W.EmployeeID == employeeID)
                        .Join(context.Employees, a => a.EmployeeID, e => e.ID, (a, e) => new {a, e})
                        .OrderByDescending(O => O.a.ID)
                        .Take(10)
                        .Select(S => new EmpEmployeeAttendanceDBInfo()
                        {
                            AttendanceID = S.a.ID,
                            EmployeeName = S.e.FullName,
                            Date = S.a.Date,
                            AttendType = (byte)S.a.Status,
                            CheckInTime = S.a.CheckInTime,
                            CheckOutTime = S.a.CheckOutTime
                        }).ToList();

                    return infoList;
                }
            }
            catch (AppException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new AppException(AppException.MSG_READ_FAIL);
            }
        }

        public static TimeSpan GetEmployeeCheckInTime(int employeeId)
        {
            try
            {
                TimeSpan checkIn = TimeSpan.Zero;

                using (HRDBEntities context = GetHRDBConnection())
                {
                    DateTime date = DateTime.Now.Date;

                    Attendance attendance = context.Attendances
                        .Where(W => W.EmployeeID == employeeId)
                        .Where(W => W.Date == date)
                        .FirstOrDefault();

                    if (attendance != null)
                    {
                        checkIn = (TimeSpan)attendance.CheckInTime;
                    }
                }

                return checkIn;
            }
            catch (Exception)
            {
                throw new AppException(AppException.MSG_READ_FAIL);
            }
        }

        /// <summary>
        /// SetCheckInOutStatus
        /// </summary>
        /// <param name="employeeID"></param>
        /// <param name="checkInOutStatus"></param>
        /// <exception cref="AppException"></exception>
        public static void SetCheckInOutStatus(int employeeID, byte checkInOutStatus, byte attendanceType)
        {
            try
            {
                using (HRDBEntities context = GetHRDBConnection())
                {
                    DateTime date = DateTime.Now.Date;
                    TimeSpan reqTime = DateTime.Now.TimeOfDay;

                    Attendance attendance = new Attendance();

                    if (checkInOutStatus == (byte)eCheckInOutStatus.CheckOut)
                    {
                        attendance = GetAttendanceInfo(context, employeeID, date);

                        attendance.CheckOutTime = reqTime;

                        if (attendance.Status < attendanceType) 
                        { 
                            attendance.Status = attendanceType; 
                        }
                    }
                    else
                    {
                        attendance.EmployeeID = employeeID;
                        attendance.Date = date;
                        attendance.CheckInTime = reqTime;
                        attendance.Status = attendanceType;

                        context.Attendances.Add(attendance);
                    }

                    context.SaveChanges();
                }
            }
            catch (AppException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new AppException(AppException.MSG_FAIL);
            }
        }

        /// <summary>
        /// Get Attenance Inf
        /// </summary>
        /// <param name="context"></param>
        /// <param name="employeeID"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        private static Attendance GetAttendanceInfo(HRDBEntities context, int employeeID, DateTime date)
        {
            try
            {
                Attendance attendance = context.Attendances.Where(W => W.EmployeeID == employeeID)
                                                .Where(W => W.Date == date)
                                                .FirstOrDefault();

                if (attendance == null)
                {
                    throw new AppException(AppException.MSG_NOT_CHECKEDIN);
                }

                return attendance;
            }
            catch (AppException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
