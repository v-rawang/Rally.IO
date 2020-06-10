using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Rally.Lib.Utility.Common
{
    public class DBUtility
    {
        public static void ParseConnectionString(string ConnectionString, out string ServerName, out string PortNumber, out string DatabaseName, out string UserName, out string Password)
        {
            string[] fields = ConnectionString.Split(new string[] { ";" }, StringSplitOptions.None);

            ServerName = null;
            PortNumber = null;
            DatabaseName = null;
            UserName = null;
            Password = null;

            if ((fields != null) && (fields.Length > 0))
            {
                string[] pair = null;

                for (int i = 0; i < fields.Length; i++)
                {
                    pair = fields[i].Split(new string[] { "=" }, StringSplitOptions.None);

                    if ((pair != null) && (pair.Length >= 2))
                    {
                        switch (pair[0].ToLower())
                        {
                            case "data source":
                                {
                                    ServerName = pair[1];
                                    break;
                                }
                            case "server":
                                {
                                    ServerName = pair[1];
                                    break;
                                }
                            case "initial catalog":
                                {
                                    DatabaseName = pair[1];
                                    break;
                                }
                            case "database":
                                {
                                    DatabaseName = pair[1];
                                    break;
                                }
                            case "user id":
                                {
                                    UserName = pair[1];
                                    break;
                                }
                            case "password":
                                {
                                    Password = pair[1];
                                    break;
                                }
                            case "port":
                                {
                                    PortNumber = pair[1];
                                    break;
                                }
                        }
                    }
                }
            }
        }

        public static string BuildConnectionString(string ServerName, string PortNumber, string DatabaseName, string UserName, string Password)
        {
            return String.Format("Data Source={0};Port={1};Initial Catalog={2};User ID={3};Password={4}", ServerName, PortNumber, DatabaseName, UserName, Password);
        }

        public static IList<IDictionary<string, object>> ConvertDataTableToList(DataTable table)
        {
            IList<IDictionary<string, object>> returnValue = null;

            if ((table != null) && (table.Rows.Count > 0))
            {
                returnValue = new List<IDictionary<string, object>>();

                Dictionary<string, object> dataEntry = null;

                foreach (DataRow row in table.Rows)
                {
                    if (row != null)
                    {
                        dataEntry = new Dictionary<string, object>();

                        foreach (DataColumn column in table.Columns)
                        {
                            dataEntry.Add(column.ColumnName, row[column.ColumnName]);
                        }

                        returnValue.Add(dataEntry);
                    }
                }
            }

            return returnValue;
        }
    }
}
