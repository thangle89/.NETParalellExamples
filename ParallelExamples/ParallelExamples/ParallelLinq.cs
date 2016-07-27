using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelExamples
{
    static class ParallelLinq
    {
        public static void Run1()
        {
            var watch = new Stopwatch();
            watch.Start();
            var arr = new List<int>(); ;
            for (int i = 0; i < 100000000; i++)
                arr.Add(i * 100000);

            var q = arr.AsParallel().Where(x => ExpensiveFilter(x));
            watch.Stop();
            Console.WriteLine("Time run Parallel: " + watch.ElapsedMilliseconds);
        }

        public static void Run1Sequential()
        {
            var watch = new Stopwatch();
            watch.Start();
            var arr = new List<int>(); ;
            for (int i = 0; i < 100000000; i++)
                arr.Add(i);

            var q = arr.Where(x => ExpensiveFilter(x));
            watch.Stop();
            Console.WriteLine("Time run Sequential: " + watch.ElapsedMilliseconds);
        }

        // increase 
        private static bool ExpensiveFilter(int x)
        {
            Thread.Sleep(100);
            return true;
        }

        /// <summary>
        /// Not run in order
        /// </summary>
        public static void Run2()
        {
            var q = Enumerable.Range(0, 100)
                .AsParallel()
                .Select(x => -x);
            foreach (var x in q) Console.WriteLine(x);
        }

        public static void Run2Fix()
        {
            var q = Enumerable.Range(0, 100)
                .AsParallel()
                .AsOrdered()
                .Select(x => -x);
            foreach (var x in q) Console.WriteLine(x);
        }

        /// <summary>
        /// break up more complex
        /// queries so that the cheap but complex part is done 
        /// externally to PLINQ, e.g. in LINQ to Objects
        /// </summary>
        public static void Run3()
        {
            var test = Enumerable.Range(0, 100);
            var q = Enumerable.Range(0, 100)
                .TakeWhile(x => SomeFunction(x))
                .AsParallel()
                .Select(x => Foo(x));
            foreach (var x in q)
                Console.WriteLine(x);
        }

        private static int Foo(int x)
        {
            return x;
        }

        private static bool SomeFunction(int x)
        {
            return true;
        }

        /// <summary>
        /// use on deman load-balancing to fix static partition 
        /// </summary>
        public static void Run4()
        {
            var watch = new Stopwatch();
            watch.Start();
            var arr = new List<int>(); ;
            for (int i = 0; i < 100000000; i++)
                arr.Add(i * 100000);
            Partitioner<int> partitioner = Partitioner.Create(arr, true);
            var q = partitioner.AsParallel().Where(x => ExpensiveFilter(x));
            watch.Stop();
            Console.WriteLine("Time run Parallel: " + watch.ElapsedMilliseconds);
        }
    }
}
