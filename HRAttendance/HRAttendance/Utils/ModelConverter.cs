using HRAttendance.Models.Common;
using HRAttendance.Models.Employee;
using HRAttendance.Models.Employee.Request;
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
        internal static EmployeeCreateDBInfo CreateDBCreateEmployeeInfo(EmployeeCreateInfo model)
        {
            EmployeeCreateDBInfo info = new EmployeeCreateDBInfo();

            if (model == null)
            {
                return info;
            }

            info.EmployeeID = model.ID;
            info.Name = model.Name;
            info.NRC = model.NRC;
            info.Gender = model.Gender;
            info.Phone = model.Phone;
            info.Email = model.Email;
            info.Address = model.Address;
            info.UserIDVal = model.UserIDVal;
            info.UserID = model.UserID;
            info.Password = model.Password;
            info.IsUpdate = model.IsUpdate;

            info.Permissions = new List<int>();

            if (model.IsManagementPermission)
            {
                info.Permissions.Add(Constants.ManagementPermissionID);
            }

            return info;
        }

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

        internal static EmployeeCreateInfo CreateUIEmployeeInfo(EmployeeCreateDBInfo employeeDbInfo)
        {
            EmployeeCreateInfo employeeInfo = new EmployeeCreateInfo();

            if (employeeDbInfo == null)
            {
                return employeeInfo;
            }

            employeeInfo.ID = employeeDbInfo.EmployeeID;
            employeeInfo.Name = employeeDbInfo.Name;
            employeeInfo.NRC = employeeDbInfo.NRC;
            employeeInfo.Phone = employeeDbInfo.Phone;
            employeeInfo.Email = employeeDbInfo.Email;
            employeeInfo.Gender = employeeDbInfo.Gender;
            employeeInfo.Address = employeeDbInfo.Address;
            employeeInfo.UserIDVal = employeeDbInfo.UserIDVal;
            employeeInfo.UserID = employeeDbInfo.UserID;
            employeeInfo.Password = Constants.DEFAULT_PASSWORD;

            if (employeeDbInfo.Permissions == null)
            {
                return employeeInfo;
            }

            if (employeeDbInfo.Permissions.Contains(Constants.ManagementPermissionID))
            {
                employeeInfo.IsManagementPermission = true;
            }

            return employeeInfo;
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