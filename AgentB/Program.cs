

namespace AgentB
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string pathToScan = args.Length > 0 ? args[0] : "/Users/mayank/TestAgentB";

            var scanner = new DirectoryScanner();
            var files = scanner.ScanDirectory(pathToScan);

            var wordCounts = new Dictionary<string, Dictionary<string, int>>();

            foreach (var filePath in files)
            {
                var counts = CountWordsInFile(filePath);
                wordCounts[filePath] = counts;
            }

            var sender = new TcpSender("127.0.0.1", 5002);
            await sender.SendFileWordCountsAsync(wordCounts);

            Console.WriteLine($"Agent B: Scanned directory {pathToScan}");
            Console.WriteLine("Agent B: Done. Press any key to exit.");
            Console.ReadKey();
        }

        static Dictionary<string, int> CountWordsInFile(string filePath)
        {
            var wordCount = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            try
            {
                string text = File.ReadAllText(filePath);
                var words = text.Split(new char[] { ' ', '\r', '\n', '\t', '.', ',', '!', '?' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var word in words)
                {
                    if (wordCount.ContainsKey(word))
                        wordCount[word]++;
                    else
                        wordCount[word] = 1;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Agent B: Error reading file {filePath} - {ex.Message}");
            }
            return wordCount;
        }
    }
}