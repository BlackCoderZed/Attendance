using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRDataAccess.Models.Common
{
    public class DBGridviewFilterInfo
    {
        public int Start { get; set; }

        public int Length { get; set; }

        public string SearchValue { get; set; }

        public string SortCol { get; set; }

        public string SortOrder { get; set; }
    }
}
