using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using System.Runtime.Remoting;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Diagnostics;

namespace Rally.Lib.Utility.Common
{
    public class CommonUtility
    {
        public static object EnitObject(String AssemblyName, String TypeName)
        {
            ObjectHandle objectHandle = Activator.CreateInstance(AssemblyName, TypeName);

            return objectHandle.Unwrap();
        }

        public static T EmitObject<T>(String AssemblyName, String TypeName)
        {
            ObjectHandle objectHandle = Activator.CreateInstance(AssemblyName, TypeName);

            return (T)(objectHandle.Unwrap());
        }

        public static object EnitObject(String AssemblyName, String TypeName, object[] arguments)
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

        public static Assembly[] ScanAssembly<T>()
        {
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

            Type[] assemblyTypes = null;

            List<Assembly> implementingAssemblies = new List<Assembly>();

            for (int i = 0; i < assemblies.Length; i++)
            {
                try
                {
                    assemblyTypes = assemblies[i].DefinedTypes.ToArray();

                    foreach (var assemblyType in assemblyTypes)
                    {
                        if (assemblyType.GetInterface(typeof(T).FullName) == typeof(T))
                        {
                            implementingAssemblies.Add(assemblies[i]);
                            break;
                        }
                    }
                }
                catch (Exception ex)
                {

                    //throw;
                }        
            }

            return implementingAssemblies.Count > 0 ? implementingAssemblies.ToArray() : null;
        }

        public static Assembly[] ScanAssemblyFromDirectory<T>(string AssemblyDirectory)
        {
            List<Assembly> implementingAssemblies = new List<Assembly>();

            string[] assemblyFiles = Directory.GetFiles(AssemblyDirectory, "*.dll", SearchOption.TopDirectoryOnly);

            Assembly assembly = null;

            Type[] assemblyTypes = null;

            if (assemblyFiles != null && assemblyFiles.Length > 0)
            {
                for (int i = 0; i < assemblyFiles.Length; i++)
                {
                    assembly = Assembly.LoadFile(assemblyFiles[i]);

                    if (assembly != null)
                    {
                        try
                        {
                            assemblyTypes = assembly.GetExportedTypes();

                            foreach (var assemblyType in assemblyTypes)
                            {
                                if (assemblyType.GetInterface(typeof(T).FullName) == typeof(T))
                                {
                                    implementingAssemblies.Add(assembly);
                                    break;
                                }
                            }
                        }
                        catch (Exception ex)
                        {

                            //throw;
                        }
                    }
                }
            }

            return implementingAssemblies.Count > 0 ? implementingAssemblies.ToArray() : null;
        }

        public static Assembly[] ScanAssemblyFromDirectory<T>(string AssemblyDirectory, string SearchPattern = "*.dll")
        {
            List<Assembly> implementingAssemblies = new List<Assembly>();

            string[] assemblyFiles = Directory.GetFiles(AssemblyDirectory, SearchPattern, SearchOption.TopDirectoryOnly);

            Assembly assembly = null;

            Type[] assemblyTypes = null;

            if (assemblyFiles != null && assemblyFiles.Length > 0)
            {
                for (int i = 0; i < assemblyFiles.Length; i++)
                {
                    assembly = Assembly.LoadFile(assemblyFiles[i]);

                    if (assembly != null)
                    {
                        try
                        {
                            assemblyTypes = assembly.GetExportedTypes();

                            foreach (var assemblyType in assemblyTypes)
                            {
                                if (assemblyType.GetInterface(typeof(T).FullName) == typeof(T))
                                {
                                    implementingAssemblies.Add(assembly);
                                    break;
                                }
                            }
                        }
                        catch (Exception ex)
                        {

                            //throw;
                        }
                    }
                }
            }

            return implementingAssemblies.Count > 0 ? implementingAssemblies.ToArray() : null;
        }

        public static byte[] BinarySerialize(object objectToSerialize)
        {
            byte[] returnValue = null;

            IFormatter formater = new BinaryFormatter();

            using (MemoryStream stream = new MemoryStream())
            {
                formater.Serialize(stream, objectToSerialize);

                returnValue = stream.GetBuffer();
            }

            return returnValue;
        }

        public static object BinaryDeserialize(byte[] objectBytes)
        {
            object returnValue = null;

            IFormatter formater = new BinaryFormatter();

            using (MemoryStream stream = new MemoryStream(objectBytes))
            {
                returnValue = formater.Deserialize(stream);
            }

            return returnValue;
        }

        /// <summary>
        /// Converts the current date time value to a millisecond value with the reference starting date specified 
        /// </summary>
        /// <param name="refDateTimeString">The string representation of the reference date time value(The default value is: 1970-01-01 00:00:00)</param>
        /// <returns>The milliseconds between the current date time value and the reference date time value</returns>
        public static string GetMillisecondsOfCurrentDateTime(string refDateTimeString)
        {
            string returnValue = String.Empty;

            if (String.IsNullOrEmpty(refDateTimeString))
            {
                refDateTimeString = "1970-01-01 00:00:00";
            }

            if (!String.IsNullOrEmpty(refDateTimeString))
            {
                DateTime start = DateTime.Now;
                DateTime end = DateTime.Now;

                if ((DateTime.TryParse(refDateTimeString, out start)))
                {
                    TimeSpan span = end.Subtract(start);

                    returnValue = span.TotalMilliseconds.ToString();
                }
            }

            return returnValue;
        }

        /// <summary>
        /// Converts the current date time value to a millisecond value with the reference starting date specified 
        /// </summary>
        /// <param name="refDateTimeString">The string representation of the reference date time value(The default value is: 1970-01-01 00:00:00)</param>
        /// <returns>The milliseconds between the current date time value and the reference date time value</returns>
        public static string GetMillisecondsOfCurrentDateTime(string refDateTimeString, bool shouldRoundToEven)
        {
            string returnValue = String.Empty;

            if (String.IsNullOrEmpty(refDateTimeString))
            {
                refDateTimeString = "1970-01-01 00:00:00";
            }

            if (!String.IsNullOrEmpty(refDateTimeString))
            {
                DateTime start = DateTime.Now;
                DateTime end = DateTime.Now;

                if ((DateTime.TryParse(refDateTimeString, out start)))
                {
                    TimeSpan span = end.Subtract(start);

                    returnValue = span.TotalMilliseconds.ToString();

                    if (shouldRoundToEven)
                    {
                        returnValue = decimal.Round(decimal.Parse(returnValue), MidpointRounding.ToEven).ToString();
                    }
                }
            }

            return returnValue;
        }

        /// <summary>
        /// Converts a date time value to a millisecond value with the reference starting date specified 
        /// </summary>
        /// <param name="dateTimeEnd">The date time value to be converted</param>
        /// <param name="refDateTimeString">The string representation of the reference date time value(The default value is: 1970-01-01 00:00:00)</param>
        /// <returns>The milliseconds between the date time value and the reference date time value</returns>
        public static string GetMillisecondsByDateTime(DateTime dateTimeEnd, string refDateTimeString)
        {
            string returnValue = String.Empty;

            if (String.IsNullOrEmpty(refDateTimeString))
            {
                refDateTimeString = "1970-01-01 00:00:00";
            }

            if (!String.IsNullOrEmpty(refDateTimeString))
            {
                DateTime start = DateTime.Now;

                if (DateTime.TryParse(refDateTimeString, out start))
                {
                    TimeSpan span = dateTimeEnd.Subtract(start);

                    returnValue = span.TotalMilliseconds.ToString();
                }
            }

            return returnValue;
        }

        /// <summary>
        /// Converts a date time value to a millisecond value with the reference starting date specified 
        /// </summary>
        /// <param name="dateTimeEnd">The date time value to be converted</param>
        /// <param name="refDateTimeString">The string representation of the reference date time value(The default value is: 1970-01-01 00:00:00)</param>
        /// <returns>The milliseconds between the date time value and the reference date time value</returns>
        public static string GetMillisecondsByDateTime(DateTime dateTimeEnd, string refDateTimeString, bool shouldRoundToEven)
        {
            string returnValue = String.Empty;

            if (String.IsNullOrEmpty(refDateTimeString))
            {
                refDateTimeString = "1970-01-01 00:00:00";
            }

            if (!String.IsNullOrEmpty(refDateTimeString))
            {
                DateTime start = DateTime.Now;

                if (DateTime.TryParse(refDateTimeString, out start))
                {
                    TimeSpan span = dateTimeEnd.Subtract(start);

                    returnValue = span.TotalMilliseconds.ToString();

                    if (shouldRoundToEven)
                    {
                        returnValue = decimal.Round(decimal.Parse(returnValue), MidpointRounding.ToEven).ToString();
                    }
                }
            }

            return returnValue;
        }

        /// <summary>
        /// Determines if a sequence takes a character according to the mask code specified
        /// </summary>
        /// <param name="sequenceString">The string representation of the sequence</param>
        /// <param name="seperatorString">The seperator in the string representation</param>
        /// <param name="maskCode">The mask code that describes and summarizes the charater of a sequence</param>
        /// <returns>Result of computing(true: the squence matches the character that the mask code describes; false: the squence does NOT matche the character that the mask code describes)</returns>
        public static bool IsSequenceMask(string sequenceString, string seperatorString, string maskCode)
        {
            bool returnValue = false;

            int maskCount = 0;

            string[] seperatorStringArray = new string[] { seperatorString };
            string[] sequenceStringArray = sequenceString.Split(seperatorStringArray, StringSplitOptions.RemoveEmptyEntries);

            if ((sequenceStringArray != null) && (sequenceStringArray.Length > 0))
            {
                for (int i = 0; i < sequenceStringArray.Length; i++)
                {
                    if (sequenceStringArray[i] == maskCode)
                    {
                        maskCount++;
                    }
                }

                if ((maskCount > 0) && (maskCount == sequenceStringArray.Length))
                {
                    returnValue = true;
                }
            }

            return returnValue;
        }

        public static void CopyDirectory(string Source, string Destination)
        {
            String[] Files;

            if (Destination[Destination.Length - 1] != Path.DirectorySeparatorChar)
            {
                Destination += Path.DirectorySeparatorChar;
            }

            if (!Directory.Exists(Destination))
            {
                Directory.CreateDirectory(Destination);
            }

            Files = Directory.GetFileSystemEntries(Source);

            foreach (string Element in Files)
            {
                // Sub directories
                if (Directory.Exists(Element))
                {
                    CopyDirectory(Element, Destination + Path.GetFileName(Element));
                }
                // Files in directory
                else
                {
                    File.Copy(Element, Destination + Path.GetFileName(Element), true);
                }
            }
        }

        public static string StartProcess(string AppPath, string AppParams, bool IsCreatingNewWindow, bool IsUsingShellExecute)
        {
            Process process = new Process();

            process.StartInfo.FileName = AppPath;
            process.StartInfo.Arguments = AppParams;
            process.StartInfo.UseShellExecute = IsUsingShellExecute;
            process.StartInfo.RedirectStandardError = !IsUsingShellExecute;
            process.StartInfo.RedirectStandardOutput = !IsUsingShellExecute;
            //process.StartInfo.CreateNoWindow = !IsCreatingNewWindow;

            process.Start();

            process.WaitForExit();

            string result = "";

            if (!IsUsingShellExecute)
            {
                using (process.StandardOutput)
                {
                    result = process.StandardOutput.ReadToEnd();
                }
            }

            return result;
        }
    }
}
