using HRCommon.Utils;
using HRDataAccess.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HRAttendance.Utils
{
    public class AttendanceHelper
    {
        public static byte GetAttendanceStatus(int employeeId, eCheckInOutStatus status)
        {
            try
            {
                DateTime dateTime = DateTime.Now;
                TimeSpan currentTime = dateTime.TimeOfDay;

                // get employee's shift time
                ShiftDao.GetEmployeeShift(employeeId, out TimeSpan checkInTime, out TimeSpan checkOutTime);

                if (status == eCheckInOutStatus.CheckIn)
                {
                    return CalculateCheckInStatus(checkInTime, checkOutTime, currentTime);
                }
                else
                {
                    TimeSpan empCheckInTime = AttendanceDao.GetEmployeeCheckInTime(employeeId);

                    return CalculateCheckOutStatus(checkInTime, checkOutTime, empCheckInTime, currentTime);
                }
                
            }
            catch(Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Calculate Status on checkout
        /// </summary>
        /// <param name="shiftCheckInTime"></param>
        /// <param name="shiftCheckOutTime"></param>
        /// <param name="empCheckInTime"></param>
        /// <param name="currentTime"></param>
        /// <returns></returns>
        private static byte CalculateCheckOutStatus(TimeSpan shiftCheckInTime, TimeSpan shiftCheckOutTime, TimeSpan empCheckInTime,
            TimeSpan currentTime)
        {
            double empWorkingHour = (currentTime - shiftCheckInTime).TotalHours;
            double totalWorkingHour = (shiftCheckOutTime - shiftCheckInTime).TotalHours;

            if (empWorkingHour < totalWorkingHour / 4)
            {
                return (byte)eAttendance.Absent;
            }
            else if (empWorkingHour < totalWorkingHour / 2)
            {
                return (byte)eAttendance.HalfDay;
            }
            else if (empWorkingHour >= totalWorkingHour)
            {
                return (byte)eAttendance.Attend;
            }
            else if (currentTime < shiftCheckOutTime)
            {
                return (byte)eAttendance.HalfDay;
            }

            return (byte)eAttendance.Attend;
        }

        /// <summary>
        /// CalculateCheckInStatus
        /// </summary>
        /// <param name="checkInTime"></param>
        /// <param name="currentTime"></param>
        /// <returns></returns>
        private static byte CalculateCheckInStatus(TimeSpan checkInTime, TimeSpan checkOutTime, TimeSpan currentTime)
        {
            if (checkInTime >= currentTime)
            {
                return (byte)eAttendance.Attend;
            }
            else
            {
                double timeDifference = (currentTime - checkInTime).TotalHours;
                double totalWorkingHours = (checkOutTime - currentTime).TotalHours;

                if (timeDifference > totalWorkingHours/2)
                {
                    return (byte)eAttendance.Absent;
                }
                else if (timeDifference >= totalWorkingHours/4)
                {
                    return (byte)eAttendance.HalfDay;
                }

                return (byte)eAttendance.Late;
            }
        }
    }
}