using HRAttendance.Utils;
using System.Web;
using System.Web.Mvc;

namespace HRAttendance
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new SecurityHandler());
        }
    }
}
