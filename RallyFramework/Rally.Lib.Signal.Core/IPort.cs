using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rally.Lib.Signal.Core.Parameter;

namespace Rally.Lib.Signal.Core
{
    public interface IPort
    {
        void Initialize(ParameterBase Parameters);
        bool Open();
        bool Stop();
        bool Close();
        int Read(byte[] Buffer);
        int Read<T>(byte[] Buffer, int Threshold, Func<object, object, T> PreFunction, Func<object, object, T> PostFunction, out T[] OutputObjects);
        int Write(byte[] Data);
        IDictionary<string, object> Options { get; set; }
        PortStatusEnum Status { get; set; }
        Func<object, object> ExtensionFunction { get; set; }
    }
}
