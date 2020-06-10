using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Rally.Lib.Signal.Core;
using Rally.Lib.Signal.Core.Parameter;
using Rally.Lib.Signal.Tcp;

namespace UnitTestTcpClient
{
    class Program
    {
        static ISocket socket = TcpSocket.NewInstance();
        static Timer timer;

        static void Main(string[] args)
        {
            InitClient();
        }

        static void InitClient()
        {
            socket.Initialize(new NetworkParameter() {
                 RemoteAddress = "192.168.0.80",//"127.0.0.1",
                 LocalAddress = "",
                 LocalPort = 9051,
                 RemotePort = 9050,
                 ReceiveTimeout = -1,
                 SendTimeout = -1,
                 ShouldKeepAlive = true,
                 //BufferSize = 5
            });

            timer = new Timer(2200) {
                Enabled = true
            };

            socket.Connect();

            string dataString = "Hello, world!";
            byte[] dataBytes = Encoding.UTF8.GetBytes(dataString);

            byte[] bytes = new byte[1024];
            int bytesReceived = -1;


            //socket.Send(dataBytes);

            //dataString = String.Format("Hello, world!--{0}", DateTime.Now);
            //dataBytes = Encoding.UTF8.GetBytes(dataString);
            //socket.Send(dataBytes);

            //dataString = String.Format("Hello, world!--{0}", DateTime.Now);
            //dataBytes = Encoding.UTF8.GetBytes(dataString);
            //socket.Send(dataBytes);

            //dataBytes = new byte[] {0xA8, 0x01, 0xAA };

            bytes = new byte[5];
            //dataBytes = new byte[] {  Convert.ToByte("A8", 16), Convert.ToByte("01", 16), Convert.ToByte("AA", 16) };

            //dataBytes = new byte[] { Convert.ToByte("A8", 8), Convert.ToByte("01", 8), Convert.ToByte("AA", 8) };

            dataBytes = new byte[] { Convert.ToByte("01", 16), Convert.ToByte("04", 16), Convert.ToByte("00", 16), Convert.ToByte("00", 16), Convert.ToByte("00", 16), Convert.ToByte("01", 16), Convert.ToByte("31", 16), Convert.ToByte("CA", 16) };

            timer.Elapsed += (s, e) =>
            {
                //dataString = String.Format("Hello, world!--{0}", e.SignalTime);
                //dataBytes = Encoding.UTF8.GetBytes(dataString);
                socket.Send(dataBytes);

                bytes = socket.Receive(out bytesReceived);

                if (bytes != null && bytesReceived > 0)
                {
                    //dataString = Encoding.ASCII.GetString(bytes); //Encoding.UTF8.GetString(bytes); //Convert.ToBase64String(bytes);

                    dataString = BitConverter.ToString(bytes);

                    //dataString = Convert.ToInt32(bytes[2]) == 0 ? "+" : "-";
                    //dataString += $"{Convert.ToInt32(bytes[3])}.{Convert.ToInt32(bytes[4])}℃";

                    Console.WriteLine("收到数据：");
                    Console.WriteLine(dataString);
                    Console.WriteLine("\r\n");
                }
            };

            timer.Start();

            Console.WriteLine("按Esc键退出...");

            while (true)
            {
                //bytes = socket.Receive(out bytesReceived);

                //if (bytes != null && bytesReceived > 0)
                //{
                //    dataString = BitConverter.ToString(bytes); //Encoding.ASCII.GetString(bytes); //Encoding.UTF8.GetString(bytes); //Convert.ToBase64String(bytes);

                //    Console.WriteLine("收到数据：");
                //    Console.WriteLine(dataString);
                //    Console.WriteLine("\r\n");
                //}

                if (Console.ReadKey(false).Key == ConsoleKey.Escape)
                {
                    break;
                }
            }

            socket.Close();
            timer.Stop();
        }
    }
}
