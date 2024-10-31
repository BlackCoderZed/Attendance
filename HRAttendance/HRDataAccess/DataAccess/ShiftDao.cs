using HRCommon.Exceptions;
using HRDataAccess.Models;
using HRDataAccess.Models.ShiftModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRDataAccess.DataAccess
{
    public class ShiftDao : BaseDao
    {
        public static void GetEmployeeShift(int employeeId, out TimeSpan checkInTime, out TimeSpan checkOutTime)
        {
            try
            {
                checkInTime = TimeSpan.Zero;
                checkOutTime = TimeSpan.Zero;

                using (HRDBEntities context = GetHRDBConnection())
                {
                    Shift shift = context.Shifts
                        .Join(context.Employees, s => s.ID, e => e.ShiftID, (s , e) => new {s, e})
                        .Where(W => W.e.ID == employeeId)
                        .Select(S => S.s).FirstOrDefault();

                    if (shift != null)
                    {
                        checkInTime = shift.StartTime;
                        checkOutTime = shift.EndTime;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new AppException(AppException.MSG_READ_FAIL);
            }
        }

        public static List<ShiftDbInfo> GetShiftDbInfos()
        {
            try
            {
                using (HRDBEntities context = GetHRDBConnection())
                {
                    List<ShiftDbInfo> shiftDbInfos = context.Shifts
                        .Select(S => new ShiftDbInfo()
                        {
                            ShiftId = S.ID,
                            ShiftName = S.ShiftName,
                            StartTime = S.StartTime,
                            EndTime = S.EndTime,
                        }).ToList();

                    return shiftDbInfos;
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
    }
}
