using System.Collections.Generic;
using System.Text;
using Contendio.Exceptions;
using System.Data.SqlClient;
using System.Reflection;
using System.IO;

namespace Contendio.Sql
{
    public class SqlRepositorySetup : IRepositorySetup
    {
        public string Workspace { get; private set; }
        public string DatabaseSchema { get; private set; }

        #region Private Members

        private string connectionString;

        #endregion

        public SqlRepositorySetup(string workspace, string connectionString)
        {
            this.DatabaseSchema = SqlUtils.SchemaNameFromConnectionString(connectionString);
            this.Workspace = workspace;
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
                using (var command = new SqlCommand("SELECT COUNT(*) as num FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = '" + Workspace + "_Node'", connection))
                {
                    var count = (int)command.ExecuteScalar();
                    return count > 0;
                }
            }
        }

        public void Uninstall()
        {
            var batches = new List<string>
                              {
                                  "USE [" + DatabaseSchema + "]",
                                  "DROP TABLE [dbo].[" + Workspace + "_NodeValue]",
                                  "DROP TABLE [dbo].[" + Workspace + "_Node]",
                                  "DROP TABLE [dbo].[" + Workspace + "_NodeType]"
                              };

            ExecuteBatches(batches);
        }

        private void ExecuteBatches(IEnumerable<string> batches)
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

        private IEnumerable<string> GetBatches()
        {
            var batches = new List<string>();
            batches.Add("USE [" + DatabaseSchema + "]");

            var script = GetInstallSqlQuery();
            var sb = new StringBuilder();
            char prevChar = '0';
            foreach (var scriptChar in script)
            {
                if (scriptChar == 'O' && prevChar == 'G')
                {
                    // Remove the previous character from the string buffer
                    sb.Remove(sb.Length - 1, 1);
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
            if (resourceStream == null)
                throw new ContendioBaseException("Error, could not find resource 'DatabaseScripts.sql' which is used when installing a workspace");

            using (var textStreamReader = new StreamReader(resourceStream))
            {
                string scripts = textStreamReader.ReadToEnd();
                return scripts.Replace("replaceme_", Workspace + "_");
            }
        }
    }
}
