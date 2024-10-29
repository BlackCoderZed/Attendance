using HRDataAccess.Models;
using System;
using System.Transactions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRDataAccess.DataAccess
{
    public class BaseDao
    {
        public static HRDBEntities GetHRDBConnection()
        {
            HRDBEntities connection = new HRDBEntities(DataAccessManager.GetEntityConnectionString());
            return connection;
        }

        public static TransactionScope GetReadCommitmentTransactionScope()
        {
            TransactionOptions options = new TransactionOptions()
            {
                IsolationLevel = IsolationLevel.ReadUncommitted,
                Timeout = TimeSpan.FromMinutes(1)
            };


            TransactionScope transaction = new TransactionScope(TransactionScopeOption.Required, options);
            return transaction;
        }
    }
}
