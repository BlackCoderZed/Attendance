using HRCommon.Exceptions;
using HRDataAccess.Models;
using HRDataAccess.Models.Common;
using HRDataAccess.Models.EmployeeModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRDataAccess.DataAccess
{
    public class EmployeeDao : BaseDao
    {
        public static List<EmployeeDBInfo> GetEmployeeInfoList(DBGridviewFilterInfo filterInfo, out int totalCount)
        {
            try
            {
                using (HRDBEntities context = GetHRDBConnection())
                {
                    IQueryable<EmployeeDBInfo> employeeQuery = context.Employees
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
    }
}
