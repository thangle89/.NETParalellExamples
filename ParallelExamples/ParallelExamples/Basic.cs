using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelExamples
{
    public static class Basic
    {
        public static void Run1()
        {

            Task task = Task.Factory.StartNew(() =>
            {
                double result = 0;
                for (int i = 0; i < 10000000; i++) result += Math.Sqrt(i);
                Console.WriteLine(result);
            });
            task.Wait();
            task.ContinueWith(
                (a) => { Console.WriteLine("Computation completed"); });

        }
        public static void Run2()
        {
            var tokenSource = new CancellationTokenSource();
            var token = tokenSource.Token;
            Task task = Task.Factory.StartNew(() =>
            {
                double result = 0;
                for (int i = 0; i < 10000000; i++) result += Math.Sqrt(i);
                Console.WriteLine(result);
            },
            token);
            tokenSource.Cancel();
            task.Wait();
            task.ContinueWith(
                (a) => { Console.WriteLine("Computation completed"); });

        }

        /// <summary>
        /// Avoid creating thread directly, use Task<T> to compute value asynchronously
        /// </summary>
        public static void Run3()
        {
            Task<int> a = Task<int>.Factory.StartNew(() => { return Compute(0); });
            Task<int> b = Task<int>.Factory.StartNew(() => { return Compute(1); });
            Task<int> c = Task<int>.Factory.StartNew(() => { return Compute(2); });
            int value = a.Result + b.Result + c.Result;
            Console.WriteLine(value);
        }

        private static int Compute(int v)
        {
            return v * v;
        }

        /// <summary>
        /// avoid accessing loop iteration variable from the task body
        /// </summary>
        public static void Run4()
        {
            for (int i = 0; i < 5; i++)
            {
                Task.Factory.StartNew(() => Console.WriteLine(i));
            }
        }

        public static void Run4Fix()
        {
            for (int i = 0; i < 5; i++)
            {
                int iLocal = i;
                Task.Factory.StartNew(() => Console.WriteLine(iLocal));
            }
        }


    }
}
