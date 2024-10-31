using HRCommon.Exceptions;
using HRDataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRDataAccess.DataAccess
{
    public class RoleDao : BaseDao
    {
        public static string[] GetRoleForUser(string username)
        {
            try
            {
                using (HRDBEntities db = GetHRDBConnection())
                {
                    var roles = (from user in db.Users
                                 join userRole in db.UsersPermissions
                                 on user.ID equals userRole.UserID
                                 join role in db.Permissions
                                 on userRole.PermissionID equals role.ID
                                 where user.UserID == username
                                 where user.DelFlg == null
                                 select role.Permissions).ToList();

                    return roles.ToArray();
                }
            }
            catch (Exception ex)
            {
                //LogHelper.Error(null, ex);
                throw new AppException(AppException.MSG_READ_FAIL);
            }
        }
    }
}
