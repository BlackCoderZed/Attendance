using HRAttendance.Resources.Employee;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HRAttendance.Models.Employee.Request
{
    public class EmployeeCreateInfo
    {
        [Display(ResourceType = typeof(EmployeeRex), Name = nameof(EmployeeRex.LB_ID))]
        public int ID { get; set; }

        [Display(ResourceType = typeof(EmployeeRex), Name = nameof(EmployeeRex.LB_Name))]
        public string Name { get; set; }

        [Display(ResourceType = typeof(EmployeeRex), Name = nameof(EmployeeRex.LB_NRC))]
        public string NRC { get; set; }

        [Display(ResourceType = typeof(EmployeeRex), Name = nameof(EmployeeRex.LB_Gender))]
        public byte Gender { get; set; }

        [Display(ResourceType = typeof(EmployeeRex), Name = nameof(EmployeeRex.LB_Phone))]
        public string Phone { get; set; }

        [Display(ResourceType = typeof(EmployeeRex), Name = nameof(EmployeeRex.LB_Email))]
        public string Email { get; set; }

        [Display(ResourceType = typeof(EmployeeRex), Name = nameof(EmployeeRex.LB_Address))]
        public string Address { get; set; }

        [Display(ResourceType = typeof(EmployeeRex), Name = nameof(EmployeeRex.LB_UserID))]
        public string UserID { get; set; }

        [Display(ResourceType = typeof(EmployeeRex), Name = nameof(EmployeeRex.LB_Password))]
        public string Password { get; set; }

        [Display(ResourceType = typeof(EmployeeRex), Name = nameof(EmployeeRex.LB_ManagementPermission))]
        public bool IsManagementPermission { get; set; }

        public bool IsUpdate { get; set; }

        public int UserIDVal { get; set; }
    }
}