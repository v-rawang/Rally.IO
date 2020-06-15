using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rally.Lib.Utility.Common;
using Rally.Framework.Core;
using Rally.Framework.Core.DomainModel;

namespace Rally.Framework.Protocol
{
    public class ProtocolManager : IProtocolManager
    {
        public static IProtocolManager NewInstance()
        {
            return new ProtocolManager();
        }

        public T CreateProtocol<T>(ProtocolMeta Meta)
        {
            if (Meta == null)
            {
                return default(T);
            }

            T instance = CommonUtility.EmitObjectFromFile<T>(Meta.AssemblyFilePath, Meta.ClassName);

            return instance;
        }

        public IList<ProtocolMeta> LoadProtocolMeta(string Repository)
        {
            List<ProtocolMeta> metas = null;

            var assemblies = CommonUtility.ScanAssemblyFromDirectory<IProtocol>(Repository, "Rally.Framework.Protocol.*.dll");

            if (assemblies != null && assemblies.Length > 0)
            {
                metas = new List<ProtocolMeta>();

                ProtocolMeta meta = null;

                foreach (var assembly in assemblies)
                {
                    meta = new ProtocolMeta()
                    {
                        AssemblyFilePath = assembly.Location,
                        AssemblyName = assembly.FullName
                    };

                    var types = assembly.GetExportedTypes();

                    if (types != null && types.Length > 0)
                    {
                        foreach (var type in types)
                        {
                            if (type.GetInterface(typeof(IProtocol).FullName) == typeof(IProtocol))
                            {
                                meta.ClassName = type.FullName;
                                break;
                            }
                        }
                    }

                    var instance = CommonUtility.EmitObjectFromFile<IProtocol>(meta.AssemblyFilePath, meta.ClassName);

                    if (instance != null)
                    {
                        var info = instance.Info();

                        if (info != null)
                        {                    
                            meta.Protocol = (string)info["Name"];
                            meta.Description = (string)info["Description"];
                            meta.Version = (string)info["Version"];

                            meta.DynamicProperties = new Dictionary<string, object>() {
                                { "Model", info["Model"] },
                                { "Specification", info["Specification"] }
                            };
                        }
                    }

                    metas.Add(meta);
                }
            }

            return metas;
        }
    }
}
