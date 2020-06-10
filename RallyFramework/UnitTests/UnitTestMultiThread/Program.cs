using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestMultiThread
{
    class Program
    {
        static readonly object lockingObj = new object();

        static void Main(string[] args)
        {
            int[] values = new int[1024];

            DateTime startTime, endTime;

            lock (lockingObj)
            {
                startTime = DateTime.Now;

                //Parallel.For(0, values.Length, i => {
                //    values[i] = i * i;
                //});

                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = i * i;
                }

                //Parallel.For(0, values.Length, i =>
                //{
                //    Console.WriteLine(values[i]);
                //});

                for (int i = 0; i < values.Length; i++)
                {
                    Console.WriteLine(values[i]);
                }

                endTime = DateTime.Now;
            }

            Console.WriteLine(endTime.Subtract(startTime).TotalMilliseconds);

            //Parallel.For(0, values.Length, i =>
            //{
            //    Console.WriteLine(values[i]);
            //});

            //for (int i = 0; i < values.Length; i++)
            //{
            //    Console.WriteLine(values[i]);
            //}

            Console.Read();
        }
    }
}
