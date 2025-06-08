using System;
using System.Threading.Tasks;

namespace Master
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Starting Master...");

            var master = new MasterReceiver();
            await master.RunAsync();

            Console.WriteLine("Master finished receiving data. Press any key to exit.");
            Console.ReadKey();
        }
    }
}