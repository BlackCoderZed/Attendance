//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HRDataAccess.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Attendance
    {
        public int ID { get; set; }
        public int EmployeeID { get; set; }
        public System.DateTime Date { get; set; }
        public Nullable<System.TimeSpan> CheckInTime { get; set; }
        public Nullable<System.TimeSpan> CheckOutTime { get; set; }
        public Nullable<byte> Status { get; set; }
    
        public virtual Employee Employee { get; set; }
    }
}