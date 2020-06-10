using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Net;
using System.Net.Sockets;
using Rally.Lib.Signal.Core;
using Rally.Lib.Signal.Core.Parameter;
using Rally.Lib.Signal.Tcp;

namespace UnitTestTcpServer
{
    class Program
    {
        static ISocket socket = TcpSocket.NewInstance();
        static Timer timer;

        static void Main(string[] args)
        {
            //InitServer();

            string hostname = Dns.GetHostName();
            IPHostEntry localhost = Dns.GetHostByName(hostname);
            foreach (var iPAddress in localhost.AddressList)
            {
                Console.WriteLine(iPAddress);
            }

            try
            {
                InitServer();
                //InitSendingServer();

               
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            Console.Read();
        }

        static void InitServer()
        {
            int port = 8087; //9050;

            socket.Initialize(new NetworkParameter() {
                 LocalPort = port,
                 LocalAddress = "",//"192.168.0.108",
                 ReceiveTimeout = -1,
                 SendTimeout = -1,
                 BufferSize = 4096,
                 ShouldKeepAlive = true
            });

            Console.WriteLine(String.Format("正在端口{0}上监听...按Esc键退出...", port));

            socket.Listen();
            socket.Accept();

            byte[] bytes = new byte[1024];
            int bytesReceived = -1;
            string dataString = "";          

            while (true)
            {
                bytes = socket.Receive(out bytesReceived);

                if (bytes != null && bytesReceived >= 0)
                {
                    //dataString = Encoding.UTF8.GetString(bytes); //Convert.ToBase64String(bytes);

                    dataString = BitConverter.ToString(bytes);

                    Console.WriteLine("收到数据：");
                    Console.WriteLine(dataString);
                    Console.WriteLine("\r\n");
                }

                //if (Console.ReadKey(false).Key == ConsoleKey.Escape)
                //{
                //    break;
                //}
            }

            socket.Close();
        }

        static void InitSendingServer()
        {
            int port = 8087;//9050;

            socket.Initialize(new NetworkParameter()
            {
                LocalPort = port,
                LocalAddress = "",
                ReceiveTimeout = -1,
                SendTimeout = -1,
                ShouldKeepAlive = true
            });

            Console.WriteLine(String.Format("正在端口{0}上监听...按Esc键退出...", port));

            socket.Listen();
            socket.Accept();

            //timer = new Timer(5000){
            //    Enabled = true
            //};

            string dataString = "Hello, world!";
            byte[] dataBytes = Encoding.UTF8.GetBytes(dataString);

            float[] data = new float[36];
            data[0] = 1.0F;
            data[1] = 1.0F;
            data[2] = 10.0F;
            data[3] = 5.0F;
            data[4] = 4.0F;
            data[5] = 0.00002F;
            data[6] = 3.4F;
            data[7] = 2.0F;
            data[8] = 1.0F;
            data[9] = 1.2F;
            data[10] = 300.0F * 0.0001F;
            data[11] = 7.0F;
            data[12] = 0.0F;
            data[13] = 6000.0F * 0.0001F;
            data[14] = 1.0F;
            data[15] = 2.0F;
            data[16] = 3.0F;
            data[17] = 2.0F;
            data[18] = 1;
            data[19] = 0.5F;
            data[20] = 2.0F;
            data[21] = 1F;
            data[22] = 7F;
            data[23] = 1F;
            data[24] = 180F;
            data[25] = 2.0F;
            data[26] = 5.0F;
            data[27] = 10.0F;
            data[28] = 1.0F;
            data[29] = 7.1F;
            data[30] = 1F;
            data[31] = 1.1F;
            data[32] = 2.1F;
            data[33] = 3.1F;
            data[34] = 4.1F;
            data[35] = 5.1F;

            //timer.Elapsed += (s, e) => {
            //    //dataString = String.Format("Hello, world!--{0}", e.SignalTime);
            //    //dataBytes = Encoding.UTF8.GetBytes(dataString);

            //    data[16] = e.SignalTime.Second * data[16] * 0.001F;
            //    data[17] = e.SignalTime.Second * data[17] * 0.001F;
            //    data[18] = e.SignalTime.Second * data[18] * 0.001F;
            //    data[19] = e.SignalTime.Second * data[19] * 0.001F;
            //    data[20] = e.SignalTime.Second * data[20] * 0.001F;
            //    data[21] = e.SignalTime.Second * data[21] * 0.001F;
            //    data[22] = e.SignalTime.Second * data[22] * 0.001F;
            //    data[23] = e.SignalTime.Second * data[23] * 0.001F;
            //    data[24] = e.SignalTime.Second * data[24] * 0.001F;
            //    data[25] = e.SignalTime.Second * data[25] * 0.001F;
            //    data[26] = e.SignalTime.Second * data[26] * 0.001F;
            //    data[27] = e.SignalTime.Second * data[27] * 0.001F;
            //    data[28] = e.SignalTime.Second * data[28] * 0.001F;

            //    dataBytes = new byte[data.Length];

            //    for (int i = 0; i < data.Length; i++)
            //    {
            //        dataBytes[i] = Convert.ToByte(data[i]);
            //    }

            //    //socket.Send(dataBytes);
            //};

            //timer.Start();

            Console.WriteLine("按Esc键退出...");

            byte[] bytes = new byte[1024];
            int bytesReceived = -1;

            while (true)
            {
                //socket.Accept();
                bytes = socket.Receive(out bytesReceived);

                if (bytes != null && bytesReceived > 0)
                {
                    //dataString = Encoding.UTF8.GetString(bytes); //Convert.ToBase64String(bytes);

                    dataString = BitConverter.ToString(bytes);

                    Console.WriteLine("收到数据：");
                    Console.WriteLine(dataString);
                    Console.WriteLine("\r\n");

                    data[16] = DateTime.Now.Second * data[16] * 0.001F;
                    data[17] = DateTime.Now.Second * data[17] * 0.001F;
                    data[18] = DateTime.Now.Second * data[18] * 0.001F;
                    data[19] = DateTime.Now.Second * data[19] * 0.001F;
                    data[20] = DateTime.Now.Second * data[20] * 0.001F;
                    data[21] = DateTime.Now.Second * data[21] * 0.001F;
                    data[22] = DateTime.Now.Second * data[22] * 0.001F;
                    data[23] = DateTime.Now.Second * data[23] * 0.001F;
                    data[24] = DateTime.Now.Second * data[24] * 0.001F;
                    data[25] = DateTime.Now.Second * data[25] * 0.001F;
                    data[26] = DateTime.Now.Second * data[26] * 0.001F;
                    data[27] = DateTime.Now.Second * data[27] * 0.001F;
                    data[28] = DateTime.Now.Second * data[28] * 0.001F;

                    dataBytes = new byte[data.Length];

                    for (int i = 0; i < data.Length; i++)
                    {
                        dataBytes[i] = Convert.ToByte(data[i]);
                    }

                    socket.Send(dataBytes);            
                }

                //if (Console.ReadKey(false).Key == ConsoleKey.Escape)
                //{
                //    break;
                //}

                //socket.Close();
            }

            //socket.Close();
            //timer.Stop();
        }
    }
}
