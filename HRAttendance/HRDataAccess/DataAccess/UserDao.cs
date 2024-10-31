using HRDataAccess.Models.Account;
using HRDataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRCommon.Utils;
using HRCommon.Exceptions;
using HRDataAccess.Models.EmployeeModels;

namespace HRDataAccess.DataAccess
{
    public class UserDao : BaseDao
    {
        public static DbLoginUserInfo Login(string userName, string password)
        {
            try
            {
                using (HRDBEntities context = GetHRDBConnection())
                {
                    //string encryptPassword = Security.EncryptPassword(password);
                    string encryptPassword = CypherAndHashManager.Encrypt(password);
                    var user = context.Users
                        .Join(context.Employees, u => u.EmployeeID, e => e.ID, (u, e) => new {u, e})
                        .Where(W => W.u.UserID == userName)
                        .Where(W => W.u.Password == encryptPassword)
                        .Where(W => W.u.DelFlg == null)
                        .FirstOrDefault();

                    if (user == null)
                    {
                        throw new AppException(AppException.MSG_INVALID_LOGIN);
                    }


                    user.u.LastLoginDateTime = DateTime.Now;
                    context.SaveChanges();

                    DbLoginUserInfo dbLoginInfo = new DbLoginUserInfo();
                    dbLoginInfo.Id = user.u.ID;
                    dbLoginInfo.UserID = user.u.UserID;
                    dbLoginInfo.UserDlpName = user.e.FullName;
                    dbLoginInfo.Password = password;
                    dbLoginInfo.EmployeeID = user.u.EmployeeID;

                    return dbLoginInfo;
                }
            }
            catch (AppException)
            {
                throw;
            }
            catch (Exception ex)
            {
                // write log
                throw new AppException(AppException.MSG_READ_FAIL);
            }
        }

        /// <summary>
        /// Create Login User Account
        /// </summary>
        /// <param name="context"></param>
        /// <param name="empId"></param>
        /// <param name="dbInfo"></param>
        /// <exception cref="NotImplementedException"></exception>
        internal static int CreateUserLoginAccount(HRDBEntities context, int empId, EmployeeCreateDBInfo dbInfo)
        {
            try
            {
                User user = context.Users.Where(W => W.EmployeeID == empId).Where(W => W.DelFlg == null).FirstOrDefault();

                if (user != null)
                {
                    throw new AppException(string.Format(AppException.MSG_Exist, typeof(User).Name));
                }

                user = new User();
                user.EmployeeID = empId;
                user.UserID = dbInfo.UserID;
                user.Password = CypherAndHashManager.Encrypt(dbInfo.Password);

                context.Users.Add(user);
                context.SaveChanges();

                return user.ID;
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

        /// <summary>
        /// DeleteUserInfo
        /// </summary>
        /// <param name="context"></param>
        /// <param name="id"></param>
        /// <param name="loginID"></param>
        internal static void DeleteUserInfo(HRDBEntities context, int id, int loginID)
        {
            try
            {
                User user = context.Users.Where(W => W.EmployeeID == id).FirstOrDefault();

                if (user == null)
                {
                    throw new AppException(string.Format(AppException.MSG_NOT_EXIST, typeof(User).Name));
                }

                user.DeleteDateTime = DateTime.Now;
                user.DelFlg = user.ID;
                user.DeleteUserID = loginID;

                // remove permissions
                List<UsersPermission> permissions = context.UsersPermissions.Where(W => W.UserID == user.ID).ToList();

                if (permissions.Count > 0)
                {
                    context.UsersPermissions.RemoveRange(permissions);
                }

                context.SaveChanges();
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

        /// <summary>
        /// Update User Permission
        /// </summary>
        /// <param name="context"></param>
        /// <param name="userId"></param>
        /// <param name="permissions"></param>
        internal static void UpdateInsertUserPermissions(HRDBEntities context, int userId, List<int> permissions)
        {
            try
            {
                // delete all permission
                List<UsersPermission> lstUsrPermission = context.UsersPermissions
                    .Where(W => W.UserID ==  userId).ToList();

                context.UsersPermissions.RemoveRange(lstUsrPermission);

                foreach (int permissionID in permissions)
                {
                    UsersPermission usersPermission = new UsersPermission();
                    usersPermission.UserID = userId;
                    usersPermission.PermissionID = permissionID;

                    context.UsersPermissions.Add(usersPermission);
                }

                context.SaveChanges();
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
