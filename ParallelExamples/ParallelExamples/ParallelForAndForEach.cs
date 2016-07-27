using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;
using System.Collections.Concurrent;

namespace ParallelExamples
{
    static class ParallelForAndForEach
    {
        /// <summary>
        /// Use parallel loops to speed up operations where an expensive independent operation 
        /// needs to be performed for each input
        /// 
        /// </summary>
        public static void Run1()
        {
            var watch = new Stopwatch();
            watch.Start();
            var numbers = new int[1000000];
            var isPrime = new bool[numbers.Length];
            Parallel.For(0, numbers.Length, i =>
            {
                isPrime[i] = numbers[i] >= 2;
                computPrime(numbers[i], isPrime[i]);
            });
            watch.Stop();
            Console.WriteLine("Time run in parallel is: " + watch.ElapsedMilliseconds);
        }
        public static void Run1Sequential()
        {
            var watch = new Stopwatch();
            watch.Start();
            var numbers = new int[1000000];
            var isPrime = new bool[numbers.Length];
            for (int i = 0; i < numbers.Length; i++)
            {
                computPrime(numbers[i], isPrime[i]);
            };
            watch.Stop();
            Console.WriteLine("Time run in sequential is: " + watch.ElapsedMilliseconds);
        }

        private static void computPrime(int number, bool isPrime)
        {
            isPrime = number >= 2;
            for (int factor = 2; factor * factor <= number; factor++)
            {
                if (number % factor == 0)
                {
                    isPrime = false;
                    break;
                }
            }
        }

        /// <summary>
        /// partition for a chunk of  elements
        /// </summary>
        public static void Run2()
        {
            var watch = new Stopwatch();
            watch.Start();
            var numbers = new int[1000000];
            var isPrime = new bool[numbers.Length];
            Parallel.ForEach(Partitioner.Create(0, numbers.Length, 1024), range =>
            {
                for (int i = range.Item1; i < range.Item2; i++)
                {
                    computPrime(numbers[i], isPrime[i]);
                }               
            });
            watch.Stop();
            Console.WriteLine("Time run in parallel partition is: " + watch.ElapsedMilliseconds);
           
        }
    }
}
