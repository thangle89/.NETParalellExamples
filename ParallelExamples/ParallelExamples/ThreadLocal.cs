using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelExamples
{
    public static class ThreadLocal
    {
        public static void Run1()
        {
            ThreadLocal<int> localCount = new ThreadLocal<int>(() => 0);
            Enumerable.Range(0, 100).AsParallel()
            .Select(x =>
            {
                localCount.Value = localCount.Value + 1;
                Console.WriteLine("{0} elements so far processed on thread {1}",
                    localCount.Value,
                    Thread.CurrentThread.ManagedThreadId);
                Thread.Sleep(100);
                return x;
            })
            .ToArray();
        }
    }
}
