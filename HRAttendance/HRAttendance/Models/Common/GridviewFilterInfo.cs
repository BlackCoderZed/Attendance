using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HRAttendance.Models.Common
{
    public class GridviewFilterInfo
    {
        public int Draw { get; set; }

        public int Start { get; set; }

        public int Length { get; set; }

        public string SearchValue { get; set; }

        public string SortColumn { get; set; }

        public string SortOrder { get; set; }
    }
}