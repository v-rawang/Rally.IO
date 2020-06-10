using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;
using Newtonsoft.Json;
using CsvHelper;

namespace Rally.Lib.Protocol.Message
{
    public class MessageConfig
    {
        public static IDictionary<string,int[]> FromJson(string FileName)
        {
            IDictionary<string, int[]> config = null;

            string json = readContent(FileName);

            JsonReader jsonReader = new JsonTextReader(new StringReader(json));

            JsonSerializer jsonSerializer = new JsonSerializer();
            config = jsonSerializer.Deserialize<Dictionary<string, int[]>>(jsonReader);

            return config;
        }

        public static IDictionary<string, int[]> FromCsv(string FileName)
        {
            IDictionary<string, int[]> config = null;

            string csv = readContent(FileName);

            string configName, configValue;
            string[] valueStringArray;
            List<int> values;
            int value;

            config = new Dictionary<string, int[]>();

            using (CsvReader csvReader = new CsvReader(new StringReader(csv), CultureInfo.CurrentCulture))
            {
                while (csvReader.Read())
                {
                    configName = csvReader.GetField(0);
                    configValue = csvReader.GetField(1);
                    valueStringArray = configValue.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);

                    values = new List<int>();

                    for (int i = 0; i < valueStringArray.Length; i++)
                    {
                        if (int.TryParse(valueStringArray[i], out value))
                        {
                            values.Add(value);
                        }
                    }

                    if (!config.ContainsKey(configName))
                    {
                        config.Add(configName, values.ToArray());
                    }
                }
            }

            return config;
        }

        private static string readContent(string FileName)
        {
            string content = "";

            using (FileStream fileStream = new FileStream(FileName, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (StreamReader streamReader = new StreamReader(fileStream))
                {
                    content = streamReader.ReadToEnd();
                }
            }

            return content;
        }
    }
}
