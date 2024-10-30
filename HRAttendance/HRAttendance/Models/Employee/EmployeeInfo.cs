using HRAttendance.Resources.Employee;
using System.ComponentModel.DataAnnotations;

namespace HRAttendance.Models.Employee
{
    public class EmployeeInfo
    {
        [Display(ResourceType = typeof(EmployeeRex), Name = nameof(EmployeeRex.LB_ID))]
        public int ID { get; set; }

        [Display(ResourceType = typeof(EmployeeRex), Name = nameof(EmployeeRex.LB_Name))]
        public string Name { get; set; }

        [Display(ResourceType = typeof(EmployeeRex), Name = nameof(EmployeeRex.LB_NRC))]
        public string NRC { get; set; }

        [Display(ResourceType = typeof(EmployeeRex), Name = nameof(EmployeeRex.LB_Gender))]
        public string Gender { get; set; }

        [Display(ResourceType = typeof(EmployeeRex), Name = nameof(EmployeeRex.LB_Phone))]
        public string PhoneNumber { get; set; }

        [Display(ResourceType = typeof(EmployeeRex), Name = nameof(EmployeeRex.LB_Email))]
        public string Email { get; set; }

        [Display(ResourceType = typeof(EmployeeRex), Name = nameof(EmployeeRex.LB_Address))]
        public string Address { get; set; }

        [Display(ResourceType = typeof(EmployeeRex), Name = nameof(EmployeeRex.LB_RegistDate))]
        public string RegistDateTime { get; set; }
    }
}