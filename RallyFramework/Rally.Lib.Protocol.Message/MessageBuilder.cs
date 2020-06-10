using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Dynamic;

namespace Rally.Lib.Protocol.Message
{
    public class MessageBuilder
    {
        /// <summary>
        /// 根据原始字节数组和数组字节位配置映射信息构建动态类型实例
        /// </summary>
        /// <param name="RawData">原始字节数组</param>
        /// <param name="FieldMappings">数组字节位配置映射信息</param>
        /// <returns>对应动态类型实例各属性的原始字节数组</returns>
        public static ExpandoObject CreateMessage(byte[] RawData, IDictionary<string, int[]> FieldMappings)
        {
            ExpandoObject message = new ExpandoObject();

            foreach (var item in FieldMappings)
            {
               AddProperty(message, RawData, item.Key, item.Value);
            }

            return message;
        }

        /// <summary>
        /// 根据动态类型实例和数组字节位配置映射信息构建字节数组
        /// </summary>
        /// <param name="Message">动态类型实例</param>
        /// <param name="FieldMappings">数组字节位配置映射信息</param>
        /// <returns>对应字节数组的动态类型实例</returns>
        public static byte[] CreateBytes(ExpandoObject Message, IDictionary<string, int[]> FieldMappings)
        {
            byte[] bytes = null;

            int byteLength = FieldMappings.Sum(kv => kv.Value.Count());

            bytes = new byte[byteLength];

            var messageDic = Message as IDictionary<string, object>;

            int[] fieldValueIndexes;

            foreach (string key in messageDic.Keys)
            {
                fieldValueIndexes = FieldMappings[key];

                if (fieldValueIndexes.Length == 1)
                {
                    bytes[fieldValueIndexes[0]] = (byte)messageDic[key];
                }
                else if (fieldValueIndexes.Length > 1 && (messageDic[key] is byte[]))
                {
                    for (int i = 0; i < fieldValueIndexes.Length; i++)
                    {
                        bytes[fieldValueIndexes[i]] = (messageDic[key] as byte[])[i];
                    }
                }
            }

            return bytes;
        }

        private static void AddProperty(ExpandoObject expando, byte[] rawData, string fieldName, int[] fieldValueIndexes)
        {
            if (!(expando as IDictionary<string, object>).ContainsKey(fieldName))
            {
                if (fieldValueIndexes.Length == 1)
                {
                    (expando as IDictionary<string, object>).Add(fieldName, rawData[fieldValueIndexes[0]]);
                }
                else if (fieldValueIndexes.Length > 1)
                {
                    List<byte> bytes = new List<byte>();

                    for (int i = 0; i < fieldValueIndexes.Length; i++)
                    {
                        bytes.Add(rawData[fieldValueIndexes[i]]);
                    }

                    (expando as IDictionary<string, object>).Add(fieldName, bytes.ToArray());
                }
            }
        }
    }
}
