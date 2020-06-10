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
    public class SerialPortContinuous : IPort
    {
        private System.IO.Ports.SerialPort serialPort;

        private SerialPortParameter parameters;

        public static IPort NewInstance()
        {
            return new SerialPortContinuous();
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

            this.serialPort.DataReceived += SerialPort_DataReceived;
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

        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            DateTime receiveTime = DateTime.Now;

            string data = this.parameters.IsReadingByLine ? this.serialPort.ReadLine() : this.serialPort.ReadExisting();

            if (this.ExtensionFunction !=  null)
            {
                this.ExtensionFunction(new object[] { data, receiveTime, e.EventType.ToString()});
            }
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
            throw new NotImplementedException();
        }

        public int Write(byte[] Data)
        {
            throw new NotImplementedException();
        }

        public int Read<T>(byte[] Buffer, int Threshold, Func<object, object, T> PreFunction, Func<object, object, T> PostFunction, out T[] OutputObjects)
        {
            throw new NotImplementedException();
        }
    }
}
