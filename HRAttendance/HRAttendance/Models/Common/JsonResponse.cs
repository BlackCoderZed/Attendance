using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HRAttendance.Models.Common
{
    public class JsonResponse
    {
        public bool Success { get; set; }

        public string Message { get; set; }

        public string View { get; set; }

        public bool RedirectToLogin { get; set; }
    }
}