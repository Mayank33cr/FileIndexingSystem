

namespace AgentB
{
    public class DirectoryScanner
    {
        public List<string> ScanDirectory(string folderPath)
        {
            var files = new List<string>();

            try
            {
                if (Directory.Exists(folderPath))
                {
                    files.AddRange(Directory.GetFiles(folderPath, "*.*", SearchOption.AllDirectories));
                }
                else
                {
                    Console.WriteLine($"Directory not found: {folderPath}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error scanning directory: {ex.Message}");
            }

            return files;
        }
    }
}