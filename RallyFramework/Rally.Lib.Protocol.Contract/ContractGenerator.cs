using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Xamasoft.JsonClassGenerator;
using Xamasoft.JsonClassGenerator.CodeWriters;

namespace Rally.Lib.Protocol.Contract
{
    public class ContractGenerator
    {
        public void GenerateContract(string Namespace, string ClassName, string Json, string OutputDirectory = ".")
        {
            JsonClassGenerator jsonClassGenerator = new JsonClassGenerator(){
                Example = Json,
                Namespace = Namespace,
                MainClass = ClassName,
                CodeWriter = new CSharpCodeWriter(),
                UsePascalCase = true,
                SingleFile = true
            };

            string classCodeContent = "";

            using (StringWriter stringWriter = new StringWriter())
            {
                jsonClassGenerator.OutputStream = stringWriter;
                jsonClassGenerator.GenerateClasses();
                stringWriter.Flush();

                classCodeContent = stringWriter.ToString();
            }

            string fileName = $"{ClassName}.cs";

            string outputPath = $"{OutputDirectory}\\{fileName}";

            using (FileStream fileStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write, FileShare.Write))
            {
                using (StreamWriter streamWriter = new StreamWriter(fileStream))
                {
                    streamWriter.Write(classCodeContent);
                }
            }
        }

        public string GenerateContract(string Namespace, string ClassName, string Json)
        {
            JsonClassGenerator jsonClassGenerator = new JsonClassGenerator()
            {
                Example = Json,
                Namespace = Namespace,
                MainClass = ClassName,
                CodeWriter = new CSharpCodeWriter(),
                UsePascalCase = true,
                SingleFile = true
            };

            string classCodeContent = "";

            using (StringWriter stringWriter = new StringWriter())
            {
                jsonClassGenerator.OutputStream = stringWriter;
                jsonClassGenerator.GenerateClasses();
                stringWriter.Flush();

                classCodeContent = stringWriter.ToString();
            }

            return classCodeContent;
        }
    }
}
