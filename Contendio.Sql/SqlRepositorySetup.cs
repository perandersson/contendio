using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using Contendio.Model;
using System.Data.SqlClient;
using System.Resources;
using System.Reflection;
using System.IO;
using System.Data.Linq;

namespace Contendio.Sql
{
    public class SqlRepositorySetup : IRepositorySetup
    {
        public string Repository { get; private set; }
        public string DatabaseSchema { get; private set; }

        #region Private Members

        private string connectionString;

        #endregion

        public SqlRepositorySetup(string schema, string repository, string connectionString)
        {
            this.DatabaseSchema = SqlUtils.SchemaNameFromConnectionString(connectionString);
            this.Repository = repository;
            this.connectionString = connectionString;
        }

        public void Install()
        {
            ExecuteBatches(GetBatches());
        }

        public bool IsInstalled()
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("SELECT COUNT(*) as num FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = '" + Repository + "_Node'", connection))
                {
                    var count = (int)command.ExecuteScalar();
                    return count > 0;
                }
            }
        }

        public void Uninstall()
        {
            List<string> batches = new List<string>();
            batches.Add("USE [" + DatabaseSchema + "]");
            batches.Add("DROP TABLE [dbo].[" + Repository + "_NodeValue]");
            batches.Add("DROP TABLE [dbo].[" + Repository + "_StringValue]");
            batches.Add("DROP TABLE [dbo].[" + Repository + "_BinaryValue]");
            batches.Add("DROP TABLE [dbo].[" + Repository + "_DateValue]");
            batches.Add("DROP TABLE [dbo].[" + Repository + "_Node]");
            batches.Add("DROP TABLE [dbo].[" + Repository + "_NodeType]");
            ExecuteBatches(batches);
        }

        private void ExecuteBatches(List<string> batches)
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    foreach (var batch in batches)
                    {
                        using (var command = new SqlCommand(batch, connection, transaction))
                        {
                            command.ExecuteNonQuery();
                        }
                    }

                    transaction.Commit();
                }
            }
        }

        private List<string> GetBatches()
        {
            List<string> batches = new List<string>();
            batches.Add("USE [" + DatabaseSchema + "]");

            var script = GetInstallSqlQuery();
            StringBuilder sb = new StringBuilder();
            char prevChar = '0';
            foreach (var scriptChar in script)
            {
                if (scriptChar == 'O' && prevChar == 'G')
                {
                    // Remove the previous character from the string buffer
                    sb.Remove(sb.Length - 1, 1);
                    //batches.Add(new SqlInstallBatch() { Script = sb.ToString() });
                    var str = sb.ToString();
                    if (str.Length > 0)
                        batches.Add(str);

                    sb = new StringBuilder();
                }
                else
                {
                    sb.Append(scriptChar);
                }
                prevChar = scriptChar;
            }
            return batches;
        }

        private string GetInstallSqlQuery()
        {
            string[] names = Assembly.GetExecutingAssembly().GetManifestResourceNames();
            var resourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Contendio.Sql.Resources.DatabaseScripts.sql");

            using (var textStreamReader = new StreamReader(resourceStream))
            {
                string scripts = textStreamReader.ReadToEnd();
                return scripts.Replace("replaceme_", Repository + "_");
            }
        }
    }
}
