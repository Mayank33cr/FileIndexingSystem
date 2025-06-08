

namespace FileIndexingSolution
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Agent A: Starting...");

            // Processor affinity code (can keep or remove, optional)
            try
            {
                var process = System.Diagnostics.Process.GetCurrentProcess();
                process.ProcessorAffinity = (IntPtr)1; // Core 0
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to set processor affinity: " + ex.Message);
            }

            DirectoryScanner scanner = new DirectoryScanner();
            TcpSender sender = new TcpSender("127.0.0.1", 5001);

            // Scan directory and get dictionary of file word counts
            var fileWordCounts = scanner.ScanDirectory("/Users/mayank/TestAgentA");

            if (fileWordCounts.Count == 0)
            {
                Console.WriteLine("AgentA: No files found or empty directory.");
            }
            else
            {
                Console.WriteLine($"AgentA: Scanned {fileWordCounts.Count} files with word counts.");
            }

            try
            {
                // Send the dictionary as JSON
                await sender.SendFileWordCountsAsync(fileWordCounts);
                Console.WriteLine("AgentA: Word count data sent to master.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"AgentA: Error sending data - {ex.Message}");
            }

            Console.WriteLine("Agent A: Done. Press any key to exit.");
            Console.ReadKey();
        }
    }
}