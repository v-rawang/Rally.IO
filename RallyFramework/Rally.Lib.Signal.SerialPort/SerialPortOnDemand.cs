using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using Rally.Lib.Signal.Core;
using Rally.Lib.Signal.Core.Parameter;

namespace Rally.Lib.Signal.SerialPort
{
    public class SerialPortOnDemand : IPort
    {
        private System.IO.Ports.SerialPort serialPort;

        private SerialPortParameter parameters;

        public static IPort NewInstance()
        {
            return new SerialPortOnDemand();
        }

        public IDictionary<string, object> Options { get; set; }
        public PortStatusEnum Status { get; set; }
        public Func<object, object> ExtensionFunction { get; set; }

        public void Initialize(ParameterBase Parameters)
        {
            this.parameters = Parameters as SerialPortParameter;

            this.serialPort = new System.IO.Ports.SerialPort()
            {
                PortName = this.parameters.SerialPortName,
                BaudRate = this.parameters.SerialPortBaudRate,
                DataBits = this.parameters.SerialPortDataBits,
                StopBits = (StopBits)(Enum.Parse(typeof(StopBits), this.parameters.SerialPortStopBits)),
                Parity = (Parity)(Enum.Parse(typeof(Parity), this.parameters.SerialPortParity)),
                Handshake = (Handshake)(Enum.Parse(typeof(Handshake), this.parameters.SerialPortHandShake)),
                ReadBufferSize = this.parameters.SerialPortReadBufferSize,
                ReadTimeout = this.parameters.SerialPortReadTimeout,
                WriteBufferSize = this.parameters.SerialPortWriteBufferSize,
                WriteTimeout = this.parameters.SerialPortWriteTimeout,
                RtsEnable = this.parameters.SerialPortRtsEnable,
                DtrEnable = this.parameters.SerialPortDtrEnable,
                Encoding = Encoding.GetEncoding(this.parameters.SerialPortEncodingCodePage),
            };
        }

        public bool Open()
        {
            if (this.serialPort != null && !this.serialPort.IsOpen)
            {
                this.serialPort.Open();
                this.Status = PortStatusEnum.Opened;
                return true;
            }

            return false;
        }

        public bool Stop()
        {
            if (this.serialPort != null && this.serialPort.IsOpen)
            {
                this.serialPort.Close();
                this.Status = PortStatusEnum.Closed;
                return true;
            }

            return false;
        }

        public bool Close()
        {
            if (this.serialPort != null && this.serialPort.IsOpen)
            {
                this.serialPort.Close();
                this.Status = PortStatusEnum.Closed;
                return true;
            }

            return false;
        }

        public int Read(byte[] Buffer)
        {
            int bytesRead = -1;

            if (this.serialPort.IsOpen && this.Status != PortStatusEnum.Busy)
            {
                this.Status = PortStatusEnum.Busy;

                bytesRead = this.serialPort.Read(Buffer, 0, Buffer.Length);

                if (this.parameters.ShouldDiscardOutBuffer)
                {
                    this.serialPort.DiscardOutBuffer();
                }

                if (this.parameters.ShouldDiscardOutBuffer)
                {
                    this.serialPort.DiscardInBuffer();
                }

                this.Status = PortStatusEnum.Idel;
            }      

            return bytesRead;
        }

        public int Write(byte[] Data)
        {
            int bytesWrite = -1;

            if (this.serialPort.IsOpen && this.Status != PortStatusEnum.Busy)
            {
                if (this.ExtensionFunction != null)
                {
                    this.ExtensionFunction(Data);
                }

                this.Status = PortStatusEnum.Busy;

                if (this.parameters.ShouldDiscardOutBuffer)
                {
                    this.serialPort.DiscardOutBuffer();
                }

                if (this.parameters.ShouldDiscardInBuffer)
                {
                    this.serialPort.DiscardInBuffer();
                }

                this.serialPort.Write(Data, 0, Data.Length);
                bytesWrite = Data.Length;

                this.Status = PortStatusEnum.Idel;
            }

            return bytesWrite;
        }

        public int Read<T>(byte[] Buffer, int Threshold, Func<object, object, T> PreFunction, Func<object, object, T> PostFunction, out T[] OutputObjects)
        {
            sbyte position = 0;

            T preFunctionOutput = default(T), postFunctionOutput = default(T);

            if (this.serialPort.IsOpen && this.Status != PortStatusEnum.Busy)
            {
                if (PreFunction != null)
                {
                   preFunctionOutput = PreFunction(Buffer, Threshold);
                }

                this.Status = PortStatusEnum.Busy;

                while (position < (Threshold + 1))
                {
                    Buffer[position] = (byte)this.serialPort.ReadByte();
                    position++;
                }

                if (this.parameters.ShouldDiscardOutBuffer)
                {
                    this.serialPort.DiscardOutBuffer();
                }

                if (this.parameters.ShouldDiscardInBuffer)
                {
                    this.serialPort.DiscardInBuffer();
                }  

                this.Status = PortStatusEnum.Idel;

                if (PostFunction != null)
                {
                    postFunctionOutput = PostFunction(Buffer, Threshold);
                }
            }

            OutputObjects = new T[] { preFunctionOutput, postFunctionOutput };

            return position;
        }
    }
}
