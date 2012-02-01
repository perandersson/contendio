using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Contendio.Sql
{
    public static class SqlUtils
    {
        public static string SchemaNameFromConnectionString(string connectionString)
        {
            const string lookForString = "Initial Catalog=";

            int initCatalogEnd = connectionString.IndexOf(lookForString, System.StringComparison.Ordinal) + lookForString.Length;
            int endIndex = connectionString.IndexOf(";", initCatalogEnd, System.StringComparison.Ordinal);

            return connectionString.Substring(initCatalogEnd, endIndex - initCatalogEnd);
        }
    }
}
