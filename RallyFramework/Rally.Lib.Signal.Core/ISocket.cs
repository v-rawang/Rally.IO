using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rally.Lib.Signal.Core.Parameter;

namespace Rally.Lib.Signal.Core
{
    public interface ISocket
    {
        void Initialize(ParameterBase Parameters);

        int Send(byte[] Data);

        byte[] Receive(out int BytesReceived);

        void Listen();

        void Connect();

        void Close();

        void Accept();

        IDictionary<string, object> Options { get; set; }

        Func<object, object> ExtensionFunction { get; set; }
    }
}
