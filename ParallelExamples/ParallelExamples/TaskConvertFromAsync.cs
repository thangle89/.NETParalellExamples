using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ParallelExamples
{
    static class TaskConvertFromAsync
    {
        public static void Run1()
        {
            IAsyncResult asyncResult = Dns.BeginGetHostAddresses("localhost", null, null);
            // Convert the IAsyncResult to a task
            Task<IPAddress[]> task = Task<IPAddress[]>.Factory.FromAsync(Dns.BeginGetHostAddresses, Dns.EndGetHostAddresses, "localhost", null);
            // Task is often more convenient than an IAsyncResult.
            // For example, you can schedule a continuation task:
            task.ContinueWith((doneTask) =>
            {
                Console.WriteLine("Found {0} IP addresses", doneTask.Result.Length);


            });
            // We must wait until the continuation executes and prints the result
        }
    }
}
