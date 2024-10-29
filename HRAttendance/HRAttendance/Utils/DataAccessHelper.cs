using HRAttendance.Models.Common;
using HRDataAccess;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace HRAttendance.Utils
{
    public class DataAccessHelper
    {
        internal static string EntityConnectionString = string.Empty;

        internal static string SQLConnectionString = string.Empty;

        public static void Initialize()
        {
            string xmlPath = GetFilePath(Constants.DB_CONFIG_PATH);
            DBConfiguration DBInfo = new DBConfiguration();

            using (var reader = new StreamReader(xmlPath))
            {
                DBInfo = (DBConfiguration)new XmlSerializer(typeof(DBConfiguration)).Deserialize(reader);
            }

            // Entities Connection String
            CreateConnectionString(DBInfo);
            CreateSqlConnectionString(DBInfo);

            DataAccessManager.Initialize(EntityConnectionString, SQLConnectionString);
        }


        private static void CreateConnectionString(DBConfiguration dbConfig)
        {
            try
            {
                // Connection string builder
                SqlConnectionStringBuilder sqlBuilder = new SqlConnectionStringBuilder();

                // Connection Properties
                sqlBuilder.MultipleActiveResultSets = true;
                sqlBuilder.ApplicationName = "EntityFramework";

                sqlBuilder.DataSource = dbConfig.Server;
                sqlBuilder.InitialCatalog = dbConfig.Database;
                sqlBuilder.UserID = dbConfig.Username;
                sqlBuilder.Password = dbConfig.Password;

                string providerString = sqlBuilder.ToString();

                EntityConnectionStringBuilder entityBuilder = new EntityConnectionStringBuilder();

                entityBuilder.Provider = "System.Data.SqlClient";
                entityBuilder.ProviderConnectionString = providerString;
                entityBuilder.Metadata = "res://*/";

                EntityConnectionString = entityBuilder.ToString();

            }
            catch (Exception)
            {
                throw;
            }
        }

        private static void CreateSqlConnectionString(DBConfiguration dbConfig)
        {
            try
            {
                // Connection string builder
                SqlConnectionStringBuilder sqlBuilder = new SqlConnectionStringBuilder();

                // Connection Properties
                sqlBuilder.MultipleActiveResultSets = true;
                sqlBuilder.ApplicationName = "EntityFramework";

                sqlBuilder.DataSource = dbConfig.Server;
                sqlBuilder.InitialCatalog = dbConfig.Database;
                sqlBuilder.UserID = dbConfig.Username;
                sqlBuilder.Password = dbConfig.Password;

                string providerString = sqlBuilder.ToString();

                //EntityConnectionStringBuilder entityBuilder = new EntityConnectionStringBuilder();

                //entityBuilder.Provider = "System.Data.SqlClient";
                //entityBuilder.ProviderConnectionString = providerString;
                //entityBuilder.Metadata = "res://*/";

                SQLConnectionString = providerString;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static string GetFilePath(string sFile)
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, sFile);

        }
    }
}