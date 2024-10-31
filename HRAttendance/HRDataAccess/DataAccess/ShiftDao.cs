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
