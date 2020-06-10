using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.Xsl;
using System.Xml.Schema;

namespace Rally.Lib.Utility.Xml
{
    /// <summary>
    /// 提供XML序列化/反序列化，XSLT转换，XML Schema检查和XML节点读写操作有关的工具方法
    /// </summary>
    public class XmlUtility
    {
        public static SchemaValidationListener NewSchemaValidationListener()
        {
            return new SchemaValidationListener();
        }

        /// <summary>
        /// 给定XML文档内容字符串、XML Schema文档路径，以及System.Xml.Schema.ValidationEventHandler类型的事件代理程序进行XML文档格式合法性检查
        /// </summary>
        /// <param name="xml">XML文档内容字符串</param>
        /// <param name="schemaUri">XML Schema文档路径</param>
        /// <param name="schemaValidationEventHandler">当在模式检查过程中，发现被检查的XML文档与模式所定义的规范不符而引发错误或警告时的事件代理程序（System.Xml.Schema.ValidationEventHandler类型）</param>
        /// <returns>检查是否顺利完成（并不表示被检查的XML文档是否符合Schema规范）</returns>
        public static bool IsXmlValid(string xml, string schemaUri, ValidationEventHandler schemaValidationEventHandler)
        {
            bool returnValue = false;

            if ((!String.IsNullOrEmpty(schemaUri)) && (!String.IsNullOrEmpty(xml)))
            {
                XmlReader xmlSchemaReader = XmlReader.Create(schemaUri);

                if (xmlSchemaReader != null)
                {
                    XmlReaderSettings settings = new XmlReaderSettings();
                    settings.ValidationType = ValidationType.Schema;
                    settings.ConformanceLevel = ConformanceLevel.Auto;
                    settings.ValidationFlags |= XmlSchemaValidationFlags.ReportValidationWarnings;
                    settings.ValidationFlags |= XmlSchemaValidationFlags.ProcessIdentityConstraints;

                    if (schemaValidationEventHandler != null)
                    {
                        settings.ValidationEventHandler += schemaValidationEventHandler;
                    }

                    XmlSchemaSet xmlSchemaSet = new XmlSchemaSet();

                    xmlSchemaSet.Add(null, xmlSchemaReader);

                    xmlSchemaSet.Compile();

                    settings.Schemas = xmlSchemaSet;

                    StringReader xmlDocumentStringReader = new StringReader(xml);

                    XmlReader xmlDocumentReader = XmlReader.Create(xmlDocumentStringReader, settings);

                    while (xmlDocumentReader.Read())
                    {
                    }

                    returnValue = true;
                }
            }

            return returnValue;
        }

        /// <summary>
        /// 给定XML文档内容字符串、XML Schema文档内容字符串进行XML文档格式合法性检查
        /// </summary>
        /// <param name="xml">XML文档内容字符串</param>
        /// <param name="schema">XML Schema文档内容字符串</param>
        /// <returns>检查结果报告信息</returns>
        public static string ValidateXML(string xml, string schema)
        {
            string returnValue = String.Empty;

            if ((!String.IsNullOrEmpty(xml)) && (!String.IsNullOrEmpty(schema)))
            {
                StringReader stringReader = new StringReader(schema);

                XmlReader xmlSchemaReader = XmlReader.Create(stringReader);

                if (xmlSchemaReader != null)
                {
                    XmlReaderSettings settings = new XmlReaderSettings();
                    settings.ValidationType = ValidationType.Schema;
                    settings.ConformanceLevel = ConformanceLevel.Auto;
                    settings.ValidationFlags |= XmlSchemaValidationFlags.ReportValidationWarnings;
                    settings.ValidationFlags |= XmlSchemaValidationFlags.ProcessIdentityConstraints;

                    SchemaValidationListener listener = new SchemaValidationListener();

                    settings.ValidationEventHandler += listener.OnSchemaValidating;

                    XmlSchemaSet xmlSchemaSet = new XmlSchemaSet();

                    xmlSchemaSet.Add(null, xmlSchemaReader);

                    xmlSchemaSet.Compile();

                    settings.Schemas = xmlSchemaSet;

                    StringReader xmlDocumentStringReader = new StringReader(xml);

                    XmlReader xmlDocumentReader = XmlReader.Create(xmlDocumentStringReader, settings);

                    while (xmlDocumentReader.Read())
                    {
                    }

                    returnValue = String.Format("Error count:{0};\r\n Error messages:\r\n{1}", listener.ErrorCount, listener.ErrorMessage);
                }
            }

            return returnValue;
        }

        /// <summary>
        /// 给定XML文档内容字符串、XML Schema文档内容字符串，以及检查结果输出报文格式类型进行XML文档格式合法性检查
        /// </summary>
        /// <param name="xml">XML文档内容字符串</param>
        /// <param name="schema">XML Schema文档内容字符串</param>
        /// <param name="outputFormat">检查结果输出报文格式类型（xml, json, text）</param>
        /// <returns>检查结果报告信息</returns>
        public static string ValidateXML(string xml, string schema, string outputFormat)
        {
            string returnValue = String.Empty;

            if ((!String.IsNullOrEmpty(xml)) && (!String.IsNullOrEmpty(schema)))
            {
                StringReader stringReader = new StringReader(schema);

                XmlReader xmlSchemaReader = XmlReader.Create(stringReader);

                if (xmlSchemaReader != null)
                {
                    XmlReaderSettings settings = new XmlReaderSettings();
                    settings.ValidationType = ValidationType.Schema;
                    settings.ConformanceLevel = ConformanceLevel.Auto;
                    settings.ValidationFlags |= XmlSchemaValidationFlags.ReportValidationWarnings;
                    settings.ValidationFlags |= XmlSchemaValidationFlags.ProcessIdentityConstraints;

                    SchemaValidationListener listener = new SchemaValidationListener();

                    settings.ValidationEventHandler += listener.OnSchemaValidating;

                    XmlSchemaSet xmlSchemaSet = new XmlSchemaSet();

                    xmlSchemaSet.Add(null, xmlSchemaReader);

                    xmlSchemaSet.Compile();

                    settings.Schemas = xmlSchemaSet;

                    StringReader xmlDocumentStringReader = new StringReader(xml);

                    XmlReader xmlDocumentReader = XmlReader.Create(xmlDocumentStringReader, settings);

                    while (xmlDocumentReader.Read())
                    {
                    }

                    string outputFormatTemplate = "Error count:{0};\r\n Error messages:\r\n{1}";

                    switch (outputFormat)
                    {
                        case "xml":
                            outputFormatTemplate = "<validationResult><errorCount>{0}</errorCount><errorMessage>{1}</errorMessage></validationResult>";
                            break;
                        case "json":
                            outputFormatTemplate = "{\"errorCount\":\"{0}\", \"errorMessage\":\"{1}\"}";
                            break;
                        case "text":
                            outputFormatTemplate = "Error count:{0};\r\n Error messages:\r\n{1}";
                            break;
                        default:
                            outputFormatTemplate = "<validationResult><errorCount>{0}</errorCount><errorMessage>{1}</errorMessage></validationResult>";
                            break;
                    }

                    returnValue = String.Format(outputFormatTemplate, listener.ErrorCount, listener.ErrorMessage);
                }
            }

            return returnValue;
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

        /// <summary>
        /// 给定XML文档内容字符串、目标对象类型、目标对象的额外类型，以及XML文档字符集编码页名称，反序列化出目标类型所对应的对象实例
        /// </summary>
        /// <param name="xml">XML文档内容字符串</param>
        /// <param name="type">目标对象类型</param>
        /// <param name="extraTypes">目标对象的额外类型</param>
        /// <param name="inputEncodingName">输入XML文档的字符集编码页名称。所有字符集编码页名称和信息的列表可以参考 :  http://msdn.microsoft.com/en-us/library/system.text.encoding.aspx </param>
        /// <returns>反序列化目标类型所对应的对象实例</returns>
        public static object XmlDeserialize(string xml, Type type, Type[] extraTypes, string inputEncodingName)
        {
            object returnValue = null;

            XmlSerializer serializer = new XmlSerializer(type, extraTypes);

            Encoding inputEncoding = String.IsNullOrEmpty(inputEncodingName) ? Encoding.Default : Encoding.GetEncoding(inputEncodingName);

            using (MemoryStream stream = new MemoryStream(inputEncoding.GetBytes(xml)))
            {
                returnValue = serializer.Deserialize(stream);
            }

            return returnValue;
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
        /// 给定XML文档内容字符串、XSLT文件路径、字符集编码页名称,进行XSLT转换
        /// </summary>
        /// <param name="xml">原始XML文档内容字符串</param>
        /// <param name="xsltUri">XSLT文件路径</param>
        /// <param name="outputEncodingName">转换输出字符集编码页名称。所有字符集编码页名称和信息的列表可以参考 :  http://msdn.microsoft.com/en-us/library/system.text.encoding.aspx </param>
        /// <returns>XSLT转换结果，转换后的XML文档内容字符串</returns>
        public static string XmlTransform(string xml, string xsltUri, string outputEncodingName)
        {
            string returnValue = String.Empty;

            XslCompiledTransform transform = new XslCompiledTransform();

            transform.Load(xsltUri);

            XmlDocument document = new XmlDocument();
            document.LoadXml(xml);

            MemoryStream stream = new MemoryStream();

            transform.Transform(document, null, stream);

            byte[] bytes = new byte[stream.Length];
            stream.Seek(0, SeekOrigin.Begin);
            stream.Read(bytes, 0, bytes.Length);

            Encoding outputEncoding = String.IsNullOrEmpty(outputEncodingName) ? Encoding.Default : Encoding.GetEncoding(outputEncodingName);

            returnValue = outputEncoding.GetString(bytes);

            stream.Close();

            return returnValue;
        }

        /// <summary>
        /// 给定XML文档内容字符串、XSLT文件路径、XSLT转换参数、XSLT扩展对象、输出字符集编码页名称，进行XSLT转换
        /// </summary>
        /// <param name="xmlString">原始XML文档内容字符串</param>
        /// <param name="xsltFilePath">XSLT文件路径</param>
        /// <param name="parameters">XSLT转换参数（泛型字典，每个键对应一个参数和参数值，可选）</param>
        /// <param name="extensionObjects">XSLT扩展对象（泛型字典，每个键对应一个扩展对象实例，可选）</param>
        /// <param name="outputEncodingName">转换输出字符集编码页名称。所有字符集编码页名称和信息的列表可以参考 :  http://msdn.microsoft.com/en-us/library/system.text.encoding.aspx </param>
        /// <returns>XSLT转换结果，转换后的XML文档内容字符串</returns>
        public static string GetTransformedXmlStringByXsltDocument(string xmlString, string xsltFilePath, IDictionary<string, object> parameters, IDictionary<string, object> extensionObjects, string outputEncodingName)
        {
            string returnValue = xmlString;

            XmlDocument document = new XmlDocument();
            document.LoadXml(xmlString);

            XslCompiledTransform transform = new XslCompiledTransform();
            transform.Load(xsltFilePath);

            XsltArgumentList xsltArgumentList = null;

            if (parameters != null)
            {
                if (parameters.Count > 0)
                {
                    if (xsltArgumentList == null)
                    {
                        xsltArgumentList = new XsltArgumentList();
                    }

                    foreach (string key in parameters.Keys)
                    {
                        xsltArgumentList.AddParam(key, String.Empty, parameters[key]);
                    }
                }
            }

            if (extensionObjects != null)
            {
                if (extensionObjects.Count > 0)
                {
                    if (xsltArgumentList == null)
                    {
                        xsltArgumentList = new XsltArgumentList();
                    }

                    foreach (string key in extensionObjects.Keys)
                    {
                        xsltArgumentList.AddExtensionObject(key, extensionObjects[key]);
                    }
                }
            }

            MemoryStream stream = new MemoryStream();

            transform.Transform(document, xsltArgumentList, stream);

            stream.Flush();

            byte[] bytes = new byte[stream.Length];
            stream.Seek(0, SeekOrigin.Begin);
            stream.Read(bytes, 0, bytes.Length);

            Encoding outputEncoding = String.IsNullOrEmpty(outputEncodingName) ? Encoding.Default : Encoding.GetEncoding(outputEncodingName);

            returnValue = outputEncoding.GetString(bytes);

            stream.Close();

            return returnValue;
        }

        /// <summary>
        /// 获取所给定XPath节点下的子节点的串联值
        /// </summary>
        /// <param name="filePath">XML文件全路径</param>
        /// <param name="xPath">XPath节点路径。XPath的规格全面遵循W3C XPath 1.0标准规范：https://www.w3.org/TR/1999/REC-xpath-19991116/ </param>
        /// 使用示列:
        /// ReadNodeText("MyXml.xml", "/Nodes/Node/@Name")
        /// ReadNodeText("MyXml.xml", "/Nodes/Node[@Name='MyExample']/text()")
        public static string ReadNodeText(string filePath, string xPath)
        {
            string value = "";
            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);
            XmlNode node = doc.SelectSingleNode(xPath);
            value = node.InnerText;

            return value;
        }

        /// <summary>
        /// 获取所给定XPath节点下的子节点（元素/属性）值
        /// </summary>
        /// <param name="filePath">XML文件全路径</param>
        /// <param name="xPath">XPath节点路径。XPath的规格全面遵循W3C XPath 1.0标准规范：https://www.w3.org/TR/1999/REC-xpath-19991116/ </param>
        /// 使用示列:
        /// ReadNodeValue("MyXml.xml", "/Nodes/Node/ChildNode/text()")
        /// ReadNodeValue("MyXml.xml", "/Nodes/Node[@Name='MyExample02']/ChildNode/@Name")
        public static string ReadNodeValue(string filePath, string xPath)
        {
            string value = "";
            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);
            XmlNode node = doc.SelectSingleNode(xPath);
            value = node.Value;

            return value;
        }

        /// <summary>
        /// 获取所给定XPath节点下的所有子节点对象
        /// </summary>
        /// <param name="filePath">XML文件全路径</param>
        /// <param name="xPath">XPath节点路径。XPath的规格全面遵循W3C XPath 1.0标准规范：https://www.w3.org/TR/1999/REC-xpath-19991116/ </param>
        /// 使用示列:
        /// ReadAllChildNodes("MyXml.xml", "/Nodes/Node")
        /// ReadAllChildNodes("MyXml.xml", "/Nodes/Node[@Name='MyExample']")
        public static XmlNodeList ReadAllChildNodes(string filePath, string xPath)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);
            XmlNodeList nodelist = doc.SelectNodes(xPath);  //得到指定XPath对应的节点及其所有子节点

            return nodelist;
        }

        /// <summary>
        /// 在对应XPath节点路径的位置下插入新节点和节点值，以及子节点（如果有）
        /// </summary>
        /// <param name="filePath">XML文件全路径</param>
        /// <param name="xPath">XPath节点路径。XPath的规格全面遵循W3C XPath 1.0标准规范：https://www.w3.org/TR/1999/REC-xpath-19991116/ </param>
        /// <param name="newNodeName">新节点名，如果不以“@”开头则插入新元素，否则在其父节点中插入属性</param>
        /// <param name="newNodeValue">新节点值</param>
        /// <param name="childNodes">由XML节点名和值组成的泛型字典</param>
        /// 使用示列:
        ///InsertNode("MyXml.xml", "/Nodes/Node/ChildNode", "Info", "My Test", new Dictionary<string, string>() {
        ///        { "Type", "1" },
        ///        { "Remark", "Test" },
        ///        { "@Enabled", "true"},
        ///        { "Date", DateTime.Now.ToString() }
        ///    });
        public static void InsertNode(string filePath, string xPath, string newNodeName, string newNodeValue, IDictionary<string, string> childNodes)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);
            XmlNode node = doc.SelectSingleNode(xPath);

            XmlElement newElement = null, childElement = null;

            if (String.IsNullOrEmpty(newNodeName))
            {
                node.InnerText += newNodeValue;
            }
            else if (newNodeName.StartsWith("@"))
            {
                XmlAttribute newAttribute = doc.CreateAttribute(newNodeName.Substring(1));
                newAttribute.Value = newNodeValue;
                //xn.AppendChild(newAttribute);
                node.Attributes.Append(newAttribute);

                if (childNodes != null)
                {
                    foreach (string nodeName in childNodes.Keys)
                    {
                        if (!nodeName.StartsWith("@"))
                        {
                            childElement = doc.CreateElement(nodeName);
                            childElement.InnerText = childNodes[nodeName];
                            node.AppendChild(childElement);
                        }
                        else //if (nodeName != newNodeName)//同一个节点不应有名称相同的属性
                        {
                            newAttribute = doc.CreateAttribute(nodeName.Substring(1));
                            newAttribute.Value = childNodes[nodeName];
                            //xn.AppendChild(newAttribute);
                            node.Attributes.Append(newAttribute);
                        }
                    }
                }
            }
            else
            {
                newElement = doc.CreateElement(newNodeName);

                if (!String.IsNullOrEmpty(newNodeValue))
                {
                    newElement.InnerText = newNodeValue; ;
                }

                if (childNodes != null)
                {
                    foreach (string nodeName in childNodes.Keys)
                    {
                        if (!nodeName.StartsWith("@"))
                        {
                            childElement = doc.CreateElement(nodeName);
                            childElement.InnerText = childNodes[nodeName];
                            newElement.AppendChild(childElement);
                        }
                        else
                        {
                            newElement.SetAttribute(nodeName.Substring(1), childNodes[nodeName]);
                        }
                    }
                }

                node.AppendChild(newElement);
            }  

            //xn.AppendChild(newElement);

            doc.Save(filePath);
        }

        /// <summary>
        /// 修改指定XPath下的XML节点的元素文本值
        /// </summary>
        /// <param name="filePath">XML文件全路径</param>
        /// <param name="xPath">XPath节点路径。XPath的规格全面遵循W3C XPath 1.0标准规范：https://www.w3.org/TR/1999/REC-xpath-19991116/ </param>
        /// <param name="text">节点文本值</param>
        /// 使用示列:
        /// UpdateNodeText("MyXml.xml", "/Nodes/Node/ChildNode", "MyNewTest-001")
        /// UpdateNodeText("MyXml.xml", "/Nodes/Node[@Name='MyExample02']/ChildNode", "MyTest-MyTest")
        public static void UpdateNodeText(string filePath, string xPath, string text)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);
            XmlNode node = doc.SelectSingleNode(xPath);
            node.InnerText = text;
            doc.Save(filePath);
        }

        /// <summary>
        /// 修改指定XPath下的XML节点（元素/属性）的值
        /// </summary>
        /// <param name="filePath">XML文件全路径</param>
        /// <param name="xPath">XPath节点路径。XPath的规格全面遵循W3C XPath 1.0标准规范：https://www.w3.org/TR/1999/REC-xpath-19991116/ </param>
        /// <param name="value">节点值</param>
        /// 使用示列:
        /// UpdateNodeValue("MyXml.xml", "/Nodes/Node/ChildNode/text()", "MyNewTest-001")
        /// UpdateNodeValue("MyXml.xml", "/Nodes/Node[@Name='MyExample02']/ChildNode/@Name", "MyTest")
        public static void UpdateNodeValue(string filePath, string xPath, string value)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);
            XmlNode node = doc.SelectSingleNode(xPath);
            node.Value = value;
            doc.Save(filePath);
        }

        /// <summary>
        /// 删除指定XPath下的XML节点（元素/属性）
        /// </summary>
        /// <param name="filePath">XML文件全路径</param>
        /// <param name="xPath">XPath节点路径。XPath的规格全面遵循W3C XPath 1.0标准规范：https://www.w3.org/TR/1999/REC-xpath-19991116/ </param>
        /// 使用示列:
        /// DeleteNode("MyXml.xml", "/Nodes/Node/ChildNode")
        /// DeleteNode("MyXml.xml", "/Nodes/Node[@Name='MyExample02']/ChildNode/@Name")
        public static void DeleteNode(string filePath, string xPath)
        {
            XmlDocument doc = doc = new XmlDocument();
            doc.Load(filePath);

            XmlNode node = doc.SelectSingleNode(xPath);
            
            if (node.NodeType == XmlNodeType.Attribute)//(xPath.Substring((xPath.LastIndexOf("/") + 1), 1) == "@")
            {
                //(xn as XmlAttribute).ParentNode.Attributes.Remove(xn as XmlAttribute);
                XmlNode paranetNode = doc.SelectSingleNode(xPath.Substring(0, xPath.LastIndexOf("/")));
                paranetNode.Attributes.Remove(node as XmlAttribute);
            }
            else
            {
                node.ParentNode.RemoveChild(node);
            }        
            
            doc.Save(filePath);
        }
    }

    public class SchemaValidationListener
    {
        public SchemaValidationListener()
        {
            
        }

        private int schemaValidationErrorCount;
        private string schemaValidationErrorMessage;

        public int ErrorCount
        {
            get
            {
                return this.schemaValidationErrorCount;
            }
        }

        public string ErrorMessage
        {
            get
            {
                return this.schemaValidationErrorMessage;
            }
        }

        public void OnSchemaValidating(object sender, ValidationEventArgs e)
        {
            this.schemaValidationErrorCount++;
            this.schemaValidationErrorMessage += e.Message;
            this.schemaValidationErrorMessage += "\r\n";
        }
    }
}
