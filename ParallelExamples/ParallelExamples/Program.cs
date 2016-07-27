using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallelExamples
{
    class Program
    {
        static void Main(string[] args)
        {
            //Basic.Run1();
            //Basic.Run2();
            //Basic.Run3();
            //Basic.Run4();
            //Basic.Run4Fix();

            //TaskConvertFromAsync.Run1();

            //ParallelForAndForEach.Run1();
            //ParallelForAndForEach.Run1Sequential();
            //ParallelForAndForEach.Run2();

            //ParallelLinq.Run1();
            //ParallelLinq.Run1Sequential();
            //ParallelLinq.Run2();
            //ParallelLinq.Run2Fix();
            //ParallelLinq.Run3();

            //ParallelLinq.Run4();
            //ParallelLinq.Run1Sequential();

            //ConcurrentCollections.Run1QueueLock();
            //ConcurrentCollections.Run1();

            //ThreadLocal.Run1();

            //FalseSharing.Run1();
            //FalseSharing.Run1Sequential();
            //FalseSharing.Run1SharingFix();

            OptimalParallelism.MatrixMultiply( new int[100, 100], new int[100, 100], 4);


            Console.ReadLine();
        }
    }
}
