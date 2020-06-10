using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.CodeDom;
using System.CodeDom.Compiler;

namespace Rally.Lib.Protocol.Contract
{
    public class ContractCompiler
    {
        public int CompileContract(string[] SourceFileNames, string OutputAssemblyName)
        {
            CodeDomProvider codeDomProvider = CodeDomProvider.CreateProvider("CSharp");

            CompilerParameters compilerParameters = new CompilerParameters() {
                GenerateExecutable = false,
                OutputAssembly = OutputAssemblyName,
            };

            CompilerResults compilerResults = codeDomProvider.CompileAssemblyFromFile(compilerParameters, SourceFileNames);

            return compilerResults.NativeCompilerReturnValue;
        }

        public int CompileContractBySourceCode(string[] SourceCodes, string OutputAssemblyName)
        {
            CodeDomProvider codeDomProvider = CodeDomProvider.CreateProvider("CSharp");

            CompilerParameters compilerParameters = new CompilerParameters()
            {
                GenerateExecutable = false,
                OutputAssembly = OutputAssemblyName,
            };

            CompilerResults compilerResults = codeDomProvider.CompileAssemblyFromSource(compilerParameters, SourceCodes);

            return compilerResults.NativeCompilerReturnValue;
        }
    }
}
