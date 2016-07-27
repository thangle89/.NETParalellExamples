using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelExamples
{
    class ParallelLinq
    {
        public void Run1()
        {
            var watch = new Stopwatch();
            watch.Start();
            var arr = new List<int>(); ;
            for (int i = 0; i < 100000000; i++)
                arr.Add(i*100000);

            var q = arr.AsParallel().Where(x => ExpensiveFilter(x));
            watch.Stop();
            Console.WriteLine("Time run Parallel: " + watch.ElapsedMilliseconds);
        }

        public void Run1Sequential()
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
        private bool ExpensiveFilter(int x)
        {
            if (x % 2 == 0)
            {
                return true;
            }
            return false;
        }
    }
}
