using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallelExamples
{
    /// <summary>
    ///  ConcurrentQueue, ConcurrentStack, ConcurrentDictionary
    ///  use BlockingCollection to represent the communication buffer in consumer-producer
    ///  scenarios.In such scenarios, one or more producer threads insert elements into a BlockingCollection,
    ///  and one or more consumer threads remove elements from the BlockingCollection
    /// </summary>
    /// 
    /// If a consumer attempts to remove an element from the BlockingCollection when the collection is empty,
    /// it will block and wait until an element gets inserted.A BlockingCollection can have a maximum size, and
    /// if a producer attempts to insert an element into a full BlockingCollection, the producer will block and
    /// wait until an element gets removed.
    /// 
    /// DO use regular collections with locks instead of concurrent collections if you need to perform compound
    /// atomic operations that are not supported by the corresponding concurrent collection.

    public static class ConcurrentCollections
    {
        public static void Run1QueueLock()
        {
            Queue<int> q = new Queue<int>();
            object myLock = new object();
            var task = Task.Factory.StartNew(() =>
            {
                for (int i = 0; i < 100; i++)
                {
                    int result = ComputeSomething(i);
                    lock (myLock)
                    {
                        q.Enqueue(result);
                    }
                };
            });
            task.Wait();
            Console.WriteLine(q.Count());
        }

        private static int ComputeSomething(int i)
        {
            return i * i;
        }

        public static void Run1()
        {
            ConcurrentQueue<int> q = new ConcurrentQueue<int>();
            var task = Task.Factory.StartNew(() =>
            {
                for (int i = 0; i < 100; i++)
                {
                    q.Enqueue(ComputeSomething(i));
                };
            });
            task.Wait();
            Console.WriteLine(q.Count());
        }
    }
}
