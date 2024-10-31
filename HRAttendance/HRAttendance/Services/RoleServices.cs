using HRAttendance.Models.Role.Response;
using HRAttendance.Utils;
using HRDataAccess.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HRAttendance.Services
{
    public class RoleServices : BaseServices
    {
        internal ResUserRoleInfo GetRolesForUser(string username)
        {
            ResUserRoleInfo response = new ResUserRoleInfo();

            try
            {
                response.Roles = RoleDao.GetRoleForUser(username);

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