using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRCommon.Utils
{
    public enum eAttendance
    {
        Attend = 1,
        Late = 2,
        Absent = 3,
        HalfDay = 4,
        OnLeave = 5
    }

    public enum eCheckInOutStatus
    {
        CheckIn,
        CheckOut,
    }

    public enum eEmployeeCol
    {
        ID,
        Name,
        NRC,
        Phone,
        Email,
        Address,
        Gender,
        UserID,
        Password
    }
}
