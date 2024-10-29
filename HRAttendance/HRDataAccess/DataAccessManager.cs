using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRDataAccess
{
    public class DataAccessManager
    {
        private static string _EntityConnectionString = string.Empty;

        private static string _SQLConnectionString = string.Empty;

        public static void Initialize(string entityConnStr, string sqlConnStr)
        {
            _EntityConnectionString = entityConnStr;
            _SQLConnectionString = sqlConnStr;
        }

        public static string GetEntityConnectionString()
        {
            return _EntityConnectionString;
        }
    }
}
