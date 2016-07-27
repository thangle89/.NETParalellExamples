using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallelExamples
{
    public static class FalseSharing
    {
        public static void Run1()
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            int cores = Environment.ProcessorCount;
            int[] counts = new int[cores];
            Parallel.For(0, cores, i =>
            {
                for (int j = 0; j < 10000000; j++)
                {
                    counts[i] = counts[i] + 3;
                }
            });
            stopWatch.Stop();
            Console.WriteLine("Run parallel:" + stopWatch.ElapsedMilliseconds);
        }
        public static void Run1Sequential()
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            int cores = Environment.ProcessorCount;
            int[] counts = new int[cores];
            for (int i = 0; i < cores; i++)
            {
                for (int j = 0; j < 10000000; j++)
                {
                    counts[i] = counts[i] + 3;
                }
            };
            stopWatch.Stop();
            Console.WriteLine("Run sequential:" + stopWatch.ElapsedMilliseconds);
        }
        public static void Run1SharingFix()
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            int cores = Environment.ProcessorCount;
            int[] counts = new int[cores];
            Parallel.For(0, cores, i => {
                int localCount = 0;
                for (int j = 0; j < 10000000; j++)
                {
                    localCount = localCount + 3;
                }
                counts[i] = localCount;
            });
            stopWatch.Stop();
            Console.WriteLine("Run parallel:" + stopWatch.ElapsedMilliseconds);
        }

      
    }
}
