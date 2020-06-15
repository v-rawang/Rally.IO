using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rally.Framework.Core;

namespace Rally.Framework.Protocol.ModbusRtu
{
    public class ModbusRtuProtocol : IProtocol
    {
        public dynamic ConfigureSCM(dynamic Request, IDictionary<string, int[]> RequestByteMappings, IDictionary<string, int[]> ResponseByteMappings, Func<object, object> ExtensionFunction)
        {
            throw new NotImplementedException();
        }

        public TResponse ConfigureSCM<TRequest, TResponse>(TRequest Request, Func<object, object> ExtensionFunction)
            where TRequest : class
            where TResponse : class
        {
            throw new NotImplementedException();
        }

        public dynamic Diagnose(dynamic Request, IDictionary<string, int[]> RequestByteMappings, IDictionary<string, int[]> ResponseByteMappings, Func<object, object> ExtensionFunction)
        {
            throw new NotImplementedException();
        }

        public TResponse Diagnose<TRequest, TResponse>(TRequest Request, Func<object, object> ExtensionFunction)
            where TRequest : class
            where TResponse : class
        {
            throw new NotImplementedException();
        }

        public dynamic GetMeasurementConfig(dynamic Request, IDictionary<string, int[]> RequestByteMappings, IDictionary<string, int[]> ResponseByteMappings, Func<object, object> ExtensionFunction)
        {
            throw new NotImplementedException();
        }

        public TResponse GetMeasurementConfig<TRequest, TResponse>(TRequest Request, Func<object, object> ExtensionFunction)
            where TRequest : class
            where TResponse : class
        {
            throw new NotImplementedException();
        }

        public dynamic GetMeasurementData(dynamic Request, IDictionary<string, int[]> RequestByteMappings, IDictionary<string, int[]> ResponseByteMappings, Func<object, object> ExtensionFunction)
        {
            throw new NotImplementedException();
        }

        public TResponse GetMeasurementData<TRequest, TResponse>(TRequest Request, Func<object, object> ExtensionFunction)
            where TRequest : class
            where TResponse : class
        {
            throw new NotImplementedException();
        }

        public IDictionary<string, object> Info()
        {
            return new Dictionary<string, object>() {
                { "Name", "ModbusRTU"},
                { "Description", "ModbusRTU"},
                { "Model", "400"},
                { "Specification", "ModbusRTU"},
                { "Version", "4.1.0.0"}
            };
        }

        public bool Initialize(dynamic Argument)
        {
            throw new NotImplementedException();
        }

        public dynamic ManipulateSCM(dynamic Request, IDictionary<string, int[]> RequestByteMappings, IDictionary<string, int[]> ResponseByteMappings, Func<object, object> ExtensionFunction)
        {
            throw new NotImplementedException();
        }

        public TResponse ManipulateSCM<TRequest, TResponse>(TRequest Request, Func<object, object> ExtensionFunction)
            where TRequest : class
            where TResponse : class
        {
            throw new NotImplementedException();
        }

        public dynamic Register(dynamic Request, IDictionary<string, int[]> RequestByteMappings, IDictionary<string, int[]> ResponseByteMappings, Func<object, object> ExtensionFunction)
        {
            throw new NotImplementedException();
        }

        public TResponse Register<TRequest, TResponse>(TRequest Request, Func<object, object> ExtensionFunction)
            where TRequest : class
            where TResponse : class
        {
            throw new NotImplementedException();
        }

        public dynamic SetMeasurementConfig(dynamic Request, IDictionary<string, int[]> RequestByteMappings, IDictionary<string, int[]> ResponseByteMappings, Func<object, object> ExtensionFunction)
        {
            throw new NotImplementedException();
        }

        public TResponse SetMeasurementConfig<TRequest, TResponse>(TRequest Request, Func<object, object> ExtensionFunction)
            where TRequest : class
            where TResponse : class
        {
            throw new NotImplementedException();
        }

        public bool Validate(dynamic Request, out dynamic Response, IDictionary<string, int[]> RequestByteMappings, IDictionary<string, int[]> ResponseByteMappings, Func<object, object> ExtensionFunction)
        {
            throw new NotImplementedException();
        }

        public bool Validate<TRequest, TResponse>(TRequest Request, out TResponse Response, Func<object, object> ExtensionFunction)
            where TRequest : class
            where TResponse : class
        {
            throw new NotImplementedException();
        }
    }
}
