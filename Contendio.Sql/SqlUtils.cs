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
            const string LookForString = "Initial Catalog=";

            int initCatalogEnd = connectionString.IndexOf(LookForString) + LookForString.Length;
            int endIndex = connectionString.IndexOf(";", initCatalogEnd);

            return connectionString.Substring(initCatalogEnd, endIndex - initCatalogEnd);
        }
    }
}
