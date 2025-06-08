
using System.Text.RegularExpressions;

namespace FileIndexingSolution
{
    public class DirectoryScanner
    {
        public Dictionary<string, Dictionary<string, int>> ScanDirectory(string path)
        {
            var result = new Dictionary<string, Dictionary<string, int>>();

            try
            {
                var files = Directory.GetFiles(path, "*.txt", SearchOption.AllDirectories);

                foreach (var file in files)
                {
                    var wordCount = new Dictionary<string, int>();
                    var text = File.ReadAllText(file);

                    // Regex to split words ignoring punctuation, case-insensitive
                    var words = Regex.Matches(text.ToLower(), @"\b\w+\b");

                    foreach (Match match in words)
                    {
                        var word = match.Value;
                        if (wordCount.ContainsKey(word))
                            wordCount[word]++;
                        else
                            wordCount[word] = 1;
                    }

                    result[file] = wordCount;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"AgentA: Error scanning directory - {ex.Message}");
            }

            return result;
        }
    }
}