using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Master
{
    public class MasterReceiver
    {
        private readonly TcpReceiver _receiverA = new TcpReceiver(5001);
        private readonly TcpReceiver _receiverB = new TcpReceiver(5002);

        public async Task RunAsync()
        {
            Console.WriteLine("Master: Waiting for data from agents...");

            var taskA = _receiverA.ReceiveDataAsync();
            var taskB = _receiverB.ReceiveDataAsync();

            await Task.WhenAll(taskA, taskB);

            var filesFromA = taskA.Result;
            var filesFromB = taskB.Result;

            Console.WriteLine($"Master: Received {filesFromA.Count} files from AgentA.");
            Console.WriteLine($"Master: Received {filesFromB.Count} files from AgentB.");

            var allFiles = new List<string>();
            allFiles.AddRange(filesFromA);
            allFiles.AddRange(filesFromB);

            Console.WriteLine("Master: Combined file list:");
            foreach (var file in allFiles)
            {
                Console.WriteLine(file);
            }
        }
    }
}