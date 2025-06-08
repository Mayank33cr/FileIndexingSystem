
using System.Net.Sockets;
using System.Text;
using System.Text.Json;


namespace AgentB
{
    public class TcpSender
    {
        private readonly string _serverIp;
        private readonly int _serverPort;

        public TcpSender(string serverIp, int serverPort)
        {
            _serverIp = serverIp;
            _serverPort = serverPort;
        }

        public async Task SendFileListAsync(List<string> filePaths)
        {
            try
            {
                using var client = new TcpClient();
                await client.ConnectAsync(_serverIp, _serverPort);
                Console.WriteLine($"Agent B: Connected to master at {_serverIp}:{_serverPort}");

                await using var stream = client.GetStream();
                await using var writer = new StreamWriter(stream, Encoding.UTF8) { AutoFlush = true };

                foreach (var filePath in filePaths)
                {
                    await writer.WriteLineAsync(filePath);
                }

                await writer.WriteLineAsync("END");
                Console.WriteLine($"Agent B: Sent {filePaths.Count} files to master.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Agent B: Error sending files - {ex.Message}");
            }
        }

        public async Task SendFileWordCountsAsync(Dictionary<string, Dictionary<string, int>> fileWordCounts)
        {
            try
            {
                using var client = new TcpClient();
                await client.ConnectAsync(_serverIp, _serverPort);
                Console.WriteLine($"Agent B: Connected to master at {_serverIp}:{_serverPort}");

                await using var stream = client.GetStream();
                await using var writer = new StreamWriter(stream, Encoding.UTF8) { AutoFlush = true };

                // Serialize dictionary to JSON string
                string json = JsonSerializer.Serialize(fileWordCounts);

                // Send JSON string
                await writer.WriteLineAsync(json);

                // Send END to mark end of data
                await writer.WriteLineAsync("END");

                Console.WriteLine($"Agent B: Sent word count data of {fileWordCounts.Count} files to master.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Agent B: Error sending data - {ex.Message}");
            }
        }
    }
}