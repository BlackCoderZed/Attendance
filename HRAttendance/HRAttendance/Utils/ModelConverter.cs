using HRAttendance.Models.Common;
using HRAttendance.Models.Employee;
using HRDataAccess.Models.Common;
using HRDataAccess.Models.EmployeeModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HRAttendance.Utils
{
    public class ModelConverter
    {
        internal static DBGridviewFilterInfo CreateDBGridviewFilter(GridviewFilterInfo filterInfo)
        {
            if (filterInfo == null)
            {
                return null;
            }

            DBGridviewFilterInfo filter = new DBGridviewFilterInfo();
            filter.Start = filterInfo.Start;
            filter.Length = filterInfo.Length;
            filter.SearchValue = filterInfo.SearchValue;
            filter.SortOrder = filterInfo.SortOrder;
            filter.SortCol = filterInfo.SortColumn;

            return filter;
        }

        internal static List<EmployeeInfo> CreateUIEmployeeInfoList(List<EmployeeDBInfo> dbEmployeeList)
        {
            List<EmployeeInfo> infoList = new List<EmployeeInfo>();

            foreach(EmployeeDBInfo dbEmpInfo in dbEmployeeList)
            {
                EmployeeInfo empInfo = new EmployeeInfo();
                empInfo.ID = dbEmpInfo.ID;
                empInfo.Name = dbEmpInfo.FullName;
                empInfo.NRC = dbEmpInfo.NRC;
                empInfo.Email = dbEmpInfo.Email;
                empInfo.PhoneNumber = dbEmpInfo.PhoneNumber;
                empInfo.Address = dbEmpInfo.Address;
                empInfo.RegistDateTime = dbEmpInfo.RegistDateTime.ToString("yyyy-MM-ddTHH:mm:ss");
                empInfo.Gender = Enum.GetName(typeof(eGender), dbEmpInfo.Gender);

                infoList.Add(empInfo);
            }

            return infoList;
        }
    }
}