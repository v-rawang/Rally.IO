using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Runtime.Remoting;
using System.IO;
using System.Xml.Serialization;

namespace Rally.Lib.Camera.Facade
{
    class Utility
    {
        public static object EmitObject(String AssemblyName, String TypeName)
        {
            ObjectHandle objectHandle = Activator.CreateInstance(AssemblyName, TypeName);

            return objectHandle.Unwrap();
        }

        public static T EmitObject<T>(String AssemblyName, String TypeName)
        {
            ObjectHandle objectHandle = Activator.CreateInstance(AssemblyName, TypeName);

            return (T)(objectHandle.Unwrap());
        }

        public static object EmitObject(String AssemblyName, String TypeName, object[] arguments)
        {
            Assembly assembly = Assembly.Load(AssemblyName);

            Type type = assembly.GetType(TypeName);

            return Activator.CreateInstance(type, arguments);
        }

        public static T EmitObject<T>(String AssemblyName, String TypeName, object[] arguments)
        {
            Assembly assembly = Assembly.Load(AssemblyName);

            Type type = assembly.GetType(TypeName);

            return (T)Activator.CreateInstance(type, arguments);
        }

        public static object EmitObjectFromFile(String AssemblyPath, String TypeName)
        {
            Assembly assembly = Assembly.LoadFile(AssemblyPath);

            Type type = assembly.GetType(TypeName);

            return Activator.CreateInstance(type);
        }

        public static T EmitObjectFromFile<T>(String AssemblyPath, String TypeName)
        {
            Assembly assembly = Assembly.LoadFile(AssemblyPath);

            Type type = assembly.GetType(TypeName);

            return (T)Activator.CreateInstance(type);
        }

        /// <summary>
        /// 给定XML文档内容字符串、目标对象类型参数、目标对象的额外类型，以及XML文档字符集编码页名称，反序列化出目标类型所对应的对象实例
        /// </summary>
        /// <typeparam name="T">目标对象类型模板参数</typeparam>
        /// <param name="xml">XML文档内容字符串</param>
        /// <param name="extraTypes">目标对象的额外类型</param>
        /// <param name="inputEncodingName">输入XML文档的字符集编码页名称。所有字符集编码页名称和信息的列表可以参考 :  http://msdn.microsoft.com/en-us/library/system.text.encoding.aspx </param>
        /// <returns>反序列化目标类型所对应的对象实例</returns>
        public static T XmlDeserialize<T>(string xml, Type[] extraTypes, string inputEncodingName)
        {
            object returnValue = null;

            XmlSerializer serializer = new XmlSerializer(typeof(T), extraTypes);

            Encoding inputEncoding = String.IsNullOrEmpty(inputEncodingName) ? Encoding.Default : Encoding.GetEncoding(inputEncodingName);

            using (MemoryStream stream = new MemoryStream(inputEncoding.GetBytes(xml)))
            {
                returnValue = serializer.Deserialize(stream);
            }

            return (T)(returnValue);
        }

        /// <summary>
        /// 将给定对象实例进行XML序列化
        /// </summary>
        /// <param name="objectToSerialize">对象实例</param>
        /// <param name="extraTypes">对应对象实例的额外类型</param>
        /// <param name="outputEncodingName">输出XML文档的字符集编码页名称。所有字符集编码页名称和信息的列表可以参考 :  http://msdn.microsoft.com/en-us/library/system.text.encoding.aspx </param>
        /// <returns>XML文档输出</returns>
        public static string XmlSerialize(object objectToSerialize, Type[] extraTypes, string outputEncodingName)
        {
            string returnValue = String.Empty;

            XmlSerializer serializer = new XmlSerializer(objectToSerialize.GetType(), extraTypes);

            using (MemoryStream stream = new MemoryStream())
            {
                serializer.Serialize(stream, objectToSerialize);

                byte[] bytes = new byte[stream.Length];
                stream.Seek(0, SeekOrigin.Begin);
                stream.Read(bytes, 0, bytes.Length);

                Encoding outputEncoding = String.IsNullOrEmpty(outputEncodingName) ? Encoding.Default : Encoding.GetEncoding(outputEncodingName);

                returnValue = outputEncoding.GetString(bytes);
            }

            return returnValue;
        }
    }
}
