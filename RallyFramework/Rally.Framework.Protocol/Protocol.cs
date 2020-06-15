using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Dynamic;
using Newford.Lib.Signal.Core;
using Newford.Lib.Protocol.Message;
using Newford.Nuclide.Core;

namespace Newford.Nuclide.Protocol
{
    public class Protocol : IProtocol
    {
        public Protocol(ISocket Socket)
        {
            this.socket = Socket;
        }

        private ISocket socket;
        
        public TResponse ConfigureSCM<TRequest, TResponse>(TRequest Request, Func<object, object> ExtensionFunction) where TRequest : class  where TResponse:class
        {
            IDictionary<string, int[]> config = MessageConfig.FromJson($"{typeof(TRequest).Name}.json");

            dynamic expando = new System.Dynamic.ExpandoObject();
                
            Mapper<TRequest>.Map(Request, expando);

            byte[] bytes = MessageBuilder.CreateBytes(expando, config);

            this.socket.Send(bytes);

            if (ExtensionFunction != null)
            {
                ExtensionFunction(this.socket);
            }

            int bytesReceived = -1;

            byte[] receivedBytes = this.socket.Receive(out bytesReceived);

            config = MessageConfig.FromJson($"{typeof(TResponse).Name}.json");

            dynamic expandoRec = MessageBuilder.CreateMessage(receivedBytes, config);

            TResponse response = default(TResponse);

            Mapper<TResponse>.Map(expandoRec, response);

            return response;
        }

        public TResponse Diagnose<TRequest, TResponse>(TRequest Request, Func<object, object> ExtensionFunction) where TRequest : class where TResponse : class
        {
            IDictionary<string, int[]> config = MessageConfig.FromJson($"{typeof(TRequest).Name}.json");

            dynamic expando = new System.Dynamic.ExpandoObject();

            Mapper<TRequest>.Map(Request, expando);

            byte[] bytes = MessageBuilder.CreateBytes(expando, config);

            this.socket.Send(bytes);

            if (ExtensionFunction != null)
            {
                ExtensionFunction(this.socket);
            }

            int bytesReceived = -1;

            byte[] receivedBytes = this.socket.Receive(out bytesReceived);

            config = MessageConfig.FromJson($"{typeof(TResponse).Name}.json");

            dynamic expandoRec = MessageBuilder.CreateMessage(receivedBytes, config);

            TResponse response = default(TResponse);

            Mapper<TResponse>.Map(expandoRec, response);

            return response;
        }

        public TResponse GetMeasurementConfig<TRequest, TResponse>(TRequest Request, Func<object, object> ExtensionFunction) where TRequest : class where TResponse : class
        {
            IDictionary<string, int[]> config = MessageConfig.FromJson($"{typeof(TRequest).Name}.json");

            dynamic expando = new System.Dynamic.ExpandoObject();

            Mapper<TRequest>.Map(Request, expando);

            byte[] bytes = MessageBuilder.CreateBytes(expando, config);

            this.socket.Send(bytes);

            if (ExtensionFunction != null)
            {
                ExtensionFunction(this.socket);
            }

            int bytesReceived = -1;

            byte[] receivedBytes = this.socket.Receive(out bytesReceived);

            config = MessageConfig.FromJson($"{typeof(TResponse).Name}.json");

            dynamic expandoRec = MessageBuilder.CreateMessage(receivedBytes, config);

            TResponse response = default(TResponse);

            Mapper<TResponse>.Map(expandoRec, response);

            return response;
        }

        public TResponse GetMeasurementData<TRequest, TResponse>(TRequest Request, Func<object, object> ExtensionFunction) where TRequest : class where TResponse : class
        {
            IDictionary<string, int[]> config = MessageConfig.FromJson($"{typeof(TRequest).Name}.json");

            dynamic expando = new System.Dynamic.ExpandoObject();

            Mapper<TRequest>.Map(Request, expando);

            byte[] bytes = MessageBuilder.CreateBytes(expando, config);

            this.socket.Send(bytes);

            if (ExtensionFunction != null)
            {
                ExtensionFunction(this.socket);
            }

            int bytesReceived = -1;

            byte[] receivedBytes = this.socket.Receive(out bytesReceived);

            config = MessageConfig.FromJson($"{typeof(TResponse).Name}.json");

            dynamic expandoRec = MessageBuilder.CreateMessage(receivedBytes, config);

            TResponse response = default(TResponse);

            Mapper<TResponse>.Map(expandoRec, response);

            return response;
        }

        public TResponse ManipulateSCM<TRequest, TResponse>(TRequest Request, Func<object, object> ExtensionFunction) where TRequest : class where TResponse : class
        {
            IDictionary<string, int[]> config = MessageConfig.FromJson($"{typeof(TRequest).Name}.json");

            dynamic expando = new System.Dynamic.ExpandoObject();

            Mapper<TRequest>.Map(Request, expando);

            byte[] bytes = MessageBuilder.CreateBytes(expando, config);

            this.socket.Send(bytes);

            if (ExtensionFunction != null)
            {
                ExtensionFunction(this.socket);
            }

            int bytesReceived = -1;

            byte[] receivedBytes = this.socket.Receive(out bytesReceived);

            config = MessageConfig.FromJson($"{typeof(TResponse).Name}.json");

            dynamic expandoRec = MessageBuilder.CreateMessage(receivedBytes, config);

            TResponse response = default(TResponse);

            Mapper<TResponse>.Map(expandoRec, response);

            return response;
        }

        public TResponse Register<TRequest, TResponse>(TRequest Request, Func<object, object> ExtensionFunction) where TRequest : class where TResponse : class
        {
            IDictionary<string, int[]> config = MessageConfig.FromJson($"{typeof(TRequest).Name}.json");

            dynamic expando = new System.Dynamic.ExpandoObject();

            Mapper<TRequest>.Map(Request, expando);

            byte[] bytes = MessageBuilder.CreateBytes(expando, config);

            this.socket.Send(bytes);

            if (ExtensionFunction != null)
            {
                ExtensionFunction(this.socket);
            }

            int bytesReceived = -1;

            byte[] receivedBytes = this.socket.Receive(out bytesReceived);

            config = MessageConfig.FromJson($"{typeof(TResponse).Name}.json");

            dynamic expandoRec = MessageBuilder.CreateMessage(receivedBytes, config);

            TResponse response = default(TResponse);

            Mapper<TResponse>.Map(expandoRec, response);

            return response;
        }

        public TResponse SetMeasurementConfig<TRequest, TResponse>(TRequest Request, Func<object, object> ExtensionFunction) where TRequest : class where TResponse : class
        {
            IDictionary<string, int[]> config = MessageConfig.FromJson($"{typeof(TRequest).Name}.json");

            dynamic expando = new System.Dynamic.ExpandoObject();

            Mapper<TRequest>.Map(Request, expando);

            byte[] bytes = MessageBuilder.CreateBytes(expando, config);

            this.socket.Send(bytes);

            if (ExtensionFunction != null)
            {
                ExtensionFunction(this.socket);
            }

            int bytesReceived = -1;

            byte[] receivedBytes = this.socket.Receive(out bytesReceived);

            config = MessageConfig.FromJson($"{typeof(TResponse).Name}.json");

            dynamic expandoRec = MessageBuilder.CreateMessage(receivedBytes, config);

            TResponse response = default(TResponse);

            Mapper<TResponse>.Map(expandoRec, response);

            return response;
        }

        public bool Validate<TRequest, TResponse>(TRequest Request, out TResponse Response, Func<object, object> ExtensionFunction) where TRequest : class where TResponse : class
        {
            bool result = false;

            IDictionary<string, int[]> config = MessageConfig.FromJson($"{typeof(TRequest).Name}.json");

            dynamic expando = new System.Dynamic.ExpandoObject();

            Mapper<TRequest>.Map(Request, expando);

            byte[] bytes = MessageBuilder.CreateBytes(expando, config);

            this.socket.Send(bytes);

            int bytesReceived = -1;

            byte[] receivedBytes = this.socket.Receive(out bytesReceived);

            config = MessageConfig.FromJson($"{typeof(TResponse).Name}.json");

            dynamic expandoRec = MessageBuilder.CreateMessage(receivedBytes, config);

            TResponse response = default(TResponse);

            Mapper<TResponse>.Map(expandoRec, response);

            if (ExtensionFunction != null)
            {
               result = (bool)ExtensionFunction(response);
            }

            Response = response;

            return result;
        }

        public bool Validate(dynamic Request, out dynamic Response, IDictionary<string, int[]> RequestByteMappings, IDictionary<string, int[]> ResponseByteMappings, Func<object, object> ExtensionFunction)
        {
            bool result = false;

            byte[] bytes = MessageBuilder.CreateBytes(Request, RequestByteMappings);

            this.socket.Send(bytes);

            int bytesReceived = -1;

            byte[] receivedBytes = this.socket.Receive(out bytesReceived);

            dynamic response = MessageBuilder.CreateMessage(receivedBytes, ResponseByteMappings);

            if (ExtensionFunction != null)
            {
                result = (bool)ExtensionFunction(response);
            }

            Response = response;

            return result;
        }

        public dynamic Register(dynamic Request, IDictionary<string, int[]> RequestByteMappings, IDictionary<string, int[]> ResponseByteMappings, Func<object, object> ExtensionFunction)
        {
            byte[] bytes = MessageBuilder.CreateBytes(Request, RequestByteMappings);

            this.socket.Send(bytes);

            if (ExtensionFunction != null)
            {
                ExtensionFunction(socket);
            }

            int bytesReceived = -1;

            byte[] receivedBytes = this.socket.Receive(out bytesReceived);

            dynamic response = MessageBuilder.CreateMessage(receivedBytes, ResponseByteMappings);

            return response;
        }

        public dynamic GetMeasurementData(dynamic Request, IDictionary<string, int[]> RequestByteMappings, IDictionary<string, int[]> ResponseByteMappings, Func<object, object> ExtensionFunction)
        {
            byte[] bytes = MessageBuilder.CreateBytes(Request, RequestByteMappings);

            this.socket.Send(bytes);

            if (ExtensionFunction != null)
            {
                ExtensionFunction(socket);
            }

            int bytesReceived = -1;

            byte[] receivedBytes = this.socket.Receive(out bytesReceived);

            dynamic response = MessageBuilder.CreateMessage(receivedBytes, ResponseByteMappings);

            return response;
        }

        public dynamic GetMeasurementConfig(dynamic Request, IDictionary<string, int[]> RequestByteMappings, IDictionary<string, int[]> ResponseByteMappings, Func<object, object> ExtensionFunction)
        {
            byte[] bytes = MessageBuilder.CreateBytes(Request, RequestByteMappings);

            this.socket.Send(bytes);

            if (ExtensionFunction != null)
            {
                ExtensionFunction(socket);
            }

            int bytesReceived = -1;

            byte[] receivedBytes = this.socket.Receive(out bytesReceived);

            dynamic response = MessageBuilder.CreateMessage(receivedBytes, ResponseByteMappings);

            return response;
        }

        public dynamic SetMeasurementConfig(dynamic Request, IDictionary<string, int[]> RequestByteMappings, IDictionary<string, int[]> ResponseByteMappings, Func<object, object> ExtensionFunction)
        {
            byte[] bytes = MessageBuilder.CreateBytes(Request, RequestByteMappings);

            this.socket.Send(bytes);

            if (ExtensionFunction != null)
            {
                ExtensionFunction(socket);
            }

            int bytesReceived = -1;

            byte[] receivedBytes = this.socket.Receive(out bytesReceived);

            dynamic response = MessageBuilder.CreateMessage(receivedBytes, ResponseByteMappings);

            return response;
        }

        public dynamic ManipulateSCM(dynamic Request, IDictionary<string, int[]> RequestByteMappings, IDictionary<string, int[]> ResponseByteMappings, Func<object, object> ExtensionFunction)
        {
            byte[] bytes = MessageBuilder.CreateBytes(Request, RequestByteMappings);

            this.socket.Send(bytes);

            if (ExtensionFunction != null)
            {
                ExtensionFunction(socket);
            }

            int bytesReceived = -1;

            byte[] receivedBytes = this.socket.Receive(out bytesReceived);

            dynamic response = MessageBuilder.CreateMessage(receivedBytes, ResponseByteMappings);

            return response;
        }

        public dynamic ConfigureSCM(dynamic Request, IDictionary<string, int[]> RequestByteMappings, IDictionary<string, int[]> ResponseByteMappings, Func<object, object> ExtensionFunction)
        {
            byte[] bytes = MessageBuilder.CreateBytes(Request, RequestByteMappings);

            this.socket.Send(bytes);

            if (ExtensionFunction != null)
            {
                ExtensionFunction(socket);
            }

            int bytesReceived = -1;

            byte[] receivedBytes = this.socket.Receive(out bytesReceived);

            dynamic response = MessageBuilder.CreateMessage(receivedBytes, ResponseByteMappings);

            return response;
        }

        public dynamic Diagnose(dynamic Request, IDictionary<string, int[]> RequestByteMappings, IDictionary<string, int[]> ResponseByteMappings, Func<object, object> ExtensionFunction)
        {
            byte[] bytes = MessageBuilder.CreateBytes(Request, RequestByteMappings);

            this.socket.Send(bytes);

            if (ExtensionFunction != null)
            {
                ExtensionFunction(socket);
            }

            int bytesReceived = -1;

            byte[] receivedBytes = this.socket.Receive(out bytesReceived);

            dynamic response = MessageBuilder.CreateMessage(receivedBytes, ResponseByteMappings);

            return response;
        }

        public IDictionary<string, object> Info()
        {
            throw new NotImplementedException();
        }

        public bool Initialize(dynamic Argument)
        {
            throw new NotImplementedException();
        }
    }
}
