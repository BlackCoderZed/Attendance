using HRDataAccess.Models.Account;
using HRDataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRCommon.Utils;
using HRCommon.Exceptions;

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
    }
}
