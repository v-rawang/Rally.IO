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

        public static string RebuildPostgreSQLSelectStatementForOriginalColumnNameCase(string SelectStatement) 
        {
            string statement = SelectStatement;

            string resultStatement = "";

            if (statement.ToLower().StartsWith("select ") && statement.ToLower().Contains(" from"))
            {
                statement = statement.Substring(7);

                int index = statement.ToLower().IndexOf(" from");

                statement = statement.Substring(0, index);

                resultStatement = SelectStatement.Replace(statement, "{0}");

                string[] columnNames = statement.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

                if (columnNames != null && columnNames.Length >0)
                {
                    for (int i = 0; i < columnNames.Length; i++)
                    {
                        //columnNames[i] = $"\"{columnNames[i]}\"";

                        //if (columnNames[i].ToLower().Contains(" as "))
                        //{
                        //    columnNames[i] = columnNames[i].Insert(columnNames[i].IndexOf(" "), "\"");
                        //    columnNames[i] = columnNames[i].Insert(columnNames[i].LastIndexOf(" ") + 1, "\"");
                        //}

                        if (columnNames[i].ToLower().Contains(" as "))
                        {
                            columnNames[i] = $"{columnNames[i]}\"";
                            columnNames[i] = columnNames[i].Insert(columnNames[i].LastIndexOf(" ") + 1, "\"");
                        }
                        else
                        {
                            columnNames[i] = $"{columnNames[i]} as \"{columnNames[i]}\"";
                        }
                    }

                    string columns = "";

                    for (int i = 0; i < columnNames.Length; i++)
                    {
                        columns += columnNames[i];

                        if (i != (columnNames.Length - 1))
                        {
                            columns += ",";
                        }
                    }

                    resultStatement = resultStatement.Replace("{0}", columns);
                }
            }

            return resultStatement;
        }

        public static string RebuildPostgreSQLSelectStatementForOriginalColumnNameCase(string SelectStatement, string ReplacementChar)
        {
            string statement = SelectStatement;

            string resultStatement = "";

            if (statement.ToLower().StartsWith("select ") && statement.ToLower().Contains(" from"))
            {
                statement = statement.Substring(7);

                int index = statement.ToLower().IndexOf(" from");

                statement = statement.Substring(0, index);

                resultStatement = SelectStatement.Replace(statement, "{0}");

                string[] columnNames = statement.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

                if (columnNames != null && columnNames.Length > 0)
                {
                    for (int i = 0; i < columnNames.Length; i++)
                    {
                        if (columnNames[i].ToLower().Contains(" as "))
                        {
                            columnNames[i] = $"{columnNames[i]}{ReplacementChar}";
                            //columnNames[i] = columnNames[i].Insert(columnNames[i].IndexOf(" "), ReplacementChar);
                            columnNames[i] = columnNames[i].Insert(columnNames[i].LastIndexOf(" ") + 1, ReplacementChar);
                        }
                        else
                        {
                            columnNames[i] = $"{columnNames[i]} as {ReplacementChar}{columnNames[i]}{ReplacementChar}";
                        }
                    }

                    string columns = "";

                    for (int i = 0; i < columnNames.Length; i++)
                    {
                        columns += columnNames[i];

                        if (i != (columnNames.Length - 1))
                        {
                            columns += ",";
                        }
                    }

                    resultStatement = resultStatement.Replace("{0}", columns);
                }
            }

            return resultStatement;
        }
    }
}
