using HRCommon.Exceptions;
using HRCommon.Utils;
using HRDataAccess.Models;
using HRDataAccess.Models.Common;
using HRDataAccess.Models.EmployeeModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using static System.Data.Entity.Infrastructure.Design.Executor;

namespace HRDataAccess.DataAccess
{
    public class EmployeeDao : BaseDao
    {
        public static void CreateUpdateEmployeeInfo(EmployeeCreateDBInfo dbInfo, int loginID)
        {
            try
            {
                using (HRDBEntities context = GetHRDBConnection())
                {
                    using (TransactionScope transaction = GetReadCommitmentTransactionScope())
                    {
                        Employee employee = new Employee();

                        int empId = CreateUpdateEmployeeInfo(context, dbInfo, loginID);
                        int userId = dbInfo.UserIDVal;

                        if (dbInfo.IsUpdate == false)
                        {
                            userId = UserDao.CreateUserLoginAccount(context, empId, dbInfo);
                        }

                        // update user permission
                        UserDao.UpdateInsertUserPermissions(context, userId, dbInfo.Permissions);
                        
                        transaction.Complete();
                    }
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
        /// Create/Update Employee Info
        /// </summary>
        /// <param name="context"></param>
        /// <param name="dbInfo"></param>
        /// <returns></returns>
        private static int CreateUpdateEmployeeInfo(HRDBEntities context, EmployeeCreateDBInfo dbInfo, 
            int userID)
        {
            try
            {
                DateTime datetime = DateTime.Now;

                Employee employee = new Employee();

                if (dbInfo.IsUpdate)
                {
                    employee = context.Employees.Where(W => W.ID == dbInfo.EmployeeID).FirstOrDefault();

                    if (employee == null)
                    {
                        throw new AppException(AppException.MSG_NOT_EXIST);
                    }

                    employee.LastUpdateUserID = userID;
                    employee.LastUpdateDateTime = datetime;
                }

                employee.FullName = dbInfo.Name;
                employee.NRC = dbInfo.NRC;
                employee.PhoneNumber = dbInfo.Phone;
                employee.Email = dbInfo.Email;
                employee.Gender = dbInfo.Gender;
                employee.Address = dbInfo.Address;
                employee.ShiftID = dbInfo.ShiftID;

                if (!dbInfo.IsUpdate)
                {
                    employee.RegistDateTime = datetime;
                    context.Employees.Add(employee);
                }

                context.SaveChanges();

                return employee.ID;
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

        public static List<EmployeeDBInfo> GetEmployeeInfoList(DBGridviewFilterInfo filterInfo, out int totalCount)
        {
            try
            {
                using (HRDBEntities context = GetHRDBConnection())
                {
                    IQueryable<EmployeeDBInfo> employeeQuery = context.Employees
                        .Where(W => W.DelFlg == null)
                        .Select(S => new EmployeeDBInfo()
                        {
                            ID = S.ID,
                            FullName = S.FullName,
                            NRC = S.NRC,
                            Gender = S.Gender,
                            PhoneNumber = S.PhoneNumber,
                            Email = S.Email,
                            Address = S.Address,
                            RegistDateTime = S.RegistDateTime
                        }).AsQueryable();

                    totalCount = employeeQuery.Count();

                    if (filterInfo != null)
                    {
                        employeeQuery = ApplyDBFilterInfo(employeeQuery, filterInfo, out totalCount);
                    }

                    return employeeQuery.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new AppException(AppException.MSG_READ_FAIL);
            }
        }

        private static IQueryable<EmployeeDBInfo> ApplyDBFilterInfo(IQueryable<EmployeeDBInfo> query, 
            DBGridviewFilterInfo filterInfo, out int filteredCount)
        {
            filteredCount = query.Count();

            if (filterInfo == null)
            {
                return query;
            }

            if (!string.IsNullOrEmpty(filterInfo.SearchValue))
            {
                query = query.Where(W => W.FullName.Contains(filterInfo.SearchValue) 
                    || W.Address.Contains(filterInfo.SearchValue)
                    || W.PhoneNumber.Contains(filterInfo.SearchValue)
                    || W.Email.Contains(filterInfo.SearchValue));
            }

            filteredCount = query.Count();
            bool isDesc = filterInfo.SortOrder == "asc" ? false : true;

            query = query.OrderByDynamic(filterInfo.SortCol, isDesc);

            query = query.Skip(filterInfo.Start).Take(filterInfo.Length);

            return query;
        }

        public static void CheckEmployeeExist(int iD)
        {
            try
            {
                using (HRDBEntities context = GetHRDBConnection())
                {
                    var emp = context.Employees.Where(W => W.ID == iD).Where(W => W.DelFlg == null).FirstOrDefault();

                    if (emp == null)
                    {
                        throw new AppException(string.Format(AppException.MSG_NOT_EXIST, typeof(User).Name));
                    }
                }
            }
            catch (AppException)
            {
                throw;
            }
            catch (Exception)
            {
                throw new AppException(AppException.MSG_READ_FAIL);
            }
        }

        public static void CheckExistNRC(bool isUpdate, int iD, string nRC)
        {
            try
            {
                using (HRDBEntities context = GetHRDBConnection())
                {
                    var query = context.Employees.Where(W => W.ID == iD)
                        .Where(W => W.NRC == nRC)
                        .Where(W => W.DelFlg == null);

                    if (isUpdate)
                    {
                        query = query.Where(W => W.ID != iD);
                    }

                    if (query.Count() > 0)
                    {
                        throw new AppException(string.Format(AppException.MSG_Exist, Enum.GetName(typeof(eEmployeeCol), eEmployeeCol.NRC)));
                    }
                }
            }
            catch (AppException)
            {
                throw;
            }
            catch (Exception)
            {
                throw new AppException(AppException.MSG_READ_FAIL);
            }
        }

        public static void CheckUserIDIsAvailable(string userID)
        {
            try
            {
                using (HRDBEntities context = GetHRDBConnection())
                {
                    var query = context.Users.Where(W => W.UserID == userID)
                        .Where(W => W.DelFlg == null);

                    if (query.Count() > 0)
                    {
                        throw new AppException(string.Format(AppException.MSG_Exist, Enum.GetName(typeof(eEmployeeCol), eEmployeeCol.UserID)));
                    }
                }
            }
            catch (AppException)
            {
                throw;
            }
            catch (Exception)
            {
                throw new AppException(AppException.MSG_READ_FAIL);
            }
        }

        /// <summary>
        /// DeleteEmployeeInfo
        /// </summary>
        /// <param name="id"></param>
        /// <param name="loginID"></param>
        /// <exception cref="AppException"></exception>
        public static void DeleteEmployeeInfo(int id, int loginID)
        {
            try
            {
                using (HRDBEntities context = GetHRDBConnection())
                {
                    using (TransactionScope transaction = GetReadCommitmentTransactionScope())
                    {
                        // Delete Employee Info
                        DeleteEmployeeInfo(context, id, loginID);

                        // Delete User 
                        UserDao.DeleteUserInfo(context, id, loginID);

                        transaction.Complete();
                    }
                }
            }
            catch (AppException)
            {
                throw;
            }
            catch (Exception)
            {
                throw new AppException(AppException.MSG_FAIL);
            }
        }

        /// <summary>
        /// DeleteEmployeeInfo
        /// </summary>
        /// <param name="context"></param>
        /// <param name="id"></param>
        /// <param name="loginID"></param>
        private static void DeleteEmployeeInfo(HRDBEntities context, int id, int loginID)
        {
            try
            {
                Employee employee = context.Employees.Where(W => W.ID == id).FirstOrDefault();

                if (employee == null)
                {
                    throw new AppException(string.Format(AppException.MSG_NOT_EXIST, typeof(Employee).Name));
                }

                employee.DeleteDateTime = DateTime.Now;
                employee.DelFlg = employee.ID;
                employee.LastUpdateUserID = loginID;

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

        public static EmployeeCreateDBInfo GetEmployeeInfo(int employeeId)
        {
            try
            {
                using (HRDBEntities context = GetHRDBConnection())
                {
                    EmployeeCreateDBInfo employeeDBInfo = context.Employees
                        .Join(context.Users, e => e.ID, u => u.EmployeeID, (e, u) => new { e, u })
                        .Where(W => W.e.ID == employeeId)
                        .Where(W => W.e.DelFlg == null)
                        .Select(S => new EmployeeCreateDBInfo()
                        {
                            EmployeeID = S.e.ID,
                            Name = S.e.FullName,
                            NRC = S.e.NRC,
                            Gender = S.e.Gender,
                            Phone = S.e.PhoneNumber,
                            Email = S.e.Email,
                            Address = S.e.Address,
                            UserID = S.u.UserID,
                            UserIDVal = S.u.ID,
                            Password = S.u.Password,
                            ShiftID = S.e.ShiftID
                        }).FirstOrDefault();

                    if (employeeDBInfo == null)
                    {
                        throw new AppException(string.Format(AppException.MSG_NOT_EXIST, typeof(User).Name));
                    }

                    // fill permission
                    employeeDBInfo.Permissions = context.UsersPermissions
                        .Where(W => W.UserID == employeeDBInfo.UserIDVal)
                        .Select(S => S.PermissionID).ToList();

                    return employeeDBInfo;
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
