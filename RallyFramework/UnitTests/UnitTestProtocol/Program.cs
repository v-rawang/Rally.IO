using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Collections.Concurrent;
using Rally.Lib.Protocol.Message;
using Rally.Lib.Protocol.Contract;

namespace UnitTestProtocol
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            //IDictionary<string, int[]> config = MessageConfig.FromJson("protocol.json");

            IDictionary<string, int[]> config = MessageConfig.FromCsv("protocol.csv");

            if (config != null)
            {
                foreach (var item in config)
                {
                    Console.WriteLine($"{item.Key}:{item.Value}");
                }
            }

            byte[] raw = new byte[] {0xAA, 0xAA, 0x41, 0x01, 0x11, 0x00, 0x12, 0x13, 0xEE,0xEE };

            dynamic message = MessageBuilder.CreateMessage(raw, config);

            if (message != null)
            {
                Console.WriteLine($"{message.Address},{message.Receiver},{message.OperationCode},{message.DataLength}");

                byte[] newBytes = MessageBuilder.CreateBytes(message, config);

                Console.WriteLine(BitConverter.ToString(newBytes));

                MessageTest messageTest = new MessageTest();

                Mapper<MessageTest>.Map(message, messageTest);

                //MessageTest messageTest = (MessageTest)message;

                Console.WriteLine($"{messageTest.Address},{messageTest.Receiver},{messageTest.OperationCode},{messageTest.DataLength}");

                TestTNameOf<MessageTest>(messageTest);

                dynamic expando = new System.Dynamic.ExpandoObject();

                Mapper<MessageTest>.Map(messageTest, expando);

                Console.WriteLine($"{expando.Address},{expando.Receiver},{expando.OperationCode},{expando.DataLength}");
            }

            //TestContractGeneration();

            //UnitTestTasks();

            UnitTestTimers();

            Console.Read();
        }

        static Timer timer;
        static ConcurrentBag<Task> tasks;
        static List<Timer> timers;
        static Dictionary<string, Timer> timerDic;

        static void TestTNameOf<T>(T Object)
        {
            Console.WriteLine(typeof(T).Name);
        }

        static void TestContractGeneration()
        {
            string nameSpace = "Rally.Nuclide.Protocol.Contract", className = "NuclideBoardContract", dir = "", json = "";
            json = "{\"Address\":0x12, \"Header\":0x12, \"FunctionCode\":0x12}";
            ContractGenerator contractGenerator = new ContractGenerator();
            contractGenerator.GenerateContract(nameSpace, className, json);

            ContractCompiler contractCompiler = new ContractCompiler();
            int result = contractCompiler.CompileContract(new string[] { $"{className}.cs" }, "NuclideBoardContract.dll");

            Console.WriteLine(result);
        }

        static void UnitTestTasks()
        {
            //tasks = new ConcurrentBag<Task>() {
            //    { new Task(new Action(()=> { Console.WriteLine(1); })) },
            //    { new Task(new Action(()=> { Console.WriteLine(2); })) },
            //    { new Task(new Action(()=> { Console.WriteLine(3); })) },
            //    { new Task(new Action(()=> { Console.WriteLine(4); })) }
            //};

            tasks = new ConcurrentBag<Task>();

            for (int i = 0; i < 10; i++)
            {
                tasks.Add(new Task(new Action(() => { Console.WriteLine(i); })));
            }
            
            timer = new Timer() { Interval = 1000, Enabled = true };
            timer.Elapsed += (s, e) => {
                foreach (var task in tasks)
                {
                    if (task.Status != TaskStatus.Running)
                    {
                        task.Start();
                    }
                }
            };

            timer.Start();
        }

        static void UnitTestTimers()
        {
            //timers = new List<Timer>() {
            //    { new Timer(){ Interval = 1000, Enabled = true } },
            //    { new Timer(){ Interval = 1000, Enabled = true } },
            //    { new Timer(){ Interval = 1000, Enabled = true } },
            //    { new Timer(){ Interval = 1000, Enabled = true } },
            //    { new Timer(){ Interval = 1000, Enabled = true } }
            //};

            //for (int i = 0; i < timers.Count; i++)
            //{
            //    timers[i].Elapsed += (s, e) => { Console.WriteLine($"{e.SignalTime}\r\n"); Console.WriteLine(i.ToString()); };
            //    timers[i].Start();
            //}

            //timerDic = new Dictionary<string, Timer>() {
            //    {"1", new Timer(){ Interval = 1000, Enabled = true } },
            //    {"2", new Timer(){ Interval = 1000, Enabled = true } },
            //    {"3", new Timer(){ Interval = 1000, Enabled = true } },
            //    {"4", new Timer(){ Interval = 1000, Enabled = true } },
            //    {"5", new Timer(){ Interval = 1000, Enabled = true } }
            //};

            timerDic = new Dictionary<string, Timer>() {
                {Guid.NewGuid().ToString(), new Timer(){ Interval = 1000, Enabled = true } },
                {Guid.NewGuid().ToString(), new Timer(){ Interval = 1000, Enabled = true } },
                {Guid.NewGuid().ToString(), new Timer(){ Interval = 1000, Enabled = true } },
                {Guid.NewGuid().ToString(), new Timer(){ Interval = 1000, Enabled = true } },
                {Guid.NewGuid().ToString(), new Timer(){ Interval = 1000, Enabled = true } }
            };

            foreach (string key in timerDic.Keys)
            {
                timerDic[key].Elapsed += (s, e) => {
                   // Console.WriteLine($"{e.SignalTime}\r\n");
                    Console.WriteLine(key);
                };            
            }

            foreach (string key in timerDic.Keys)
            {
                timerDic[key].Start();
            }
        }
    }
}
