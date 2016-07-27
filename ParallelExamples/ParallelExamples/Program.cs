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
            var pLinq = new ParallelLinq();
            pLinq.Run1();
            pLinq.Run1Sequential();
            Console.ReadLine();
        }
    }
}
