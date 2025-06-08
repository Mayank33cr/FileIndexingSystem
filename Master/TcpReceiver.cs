using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Master
{
    public class TcpReceiver
    {
        private readonly int _port;

        public TcpReceiver(int port)
        {
            _port = port;
        }

        public async Task<List<string>> ReceiveDataAsync()
        {
            var files = new List<string>();

            var listener = new TcpListener(IPAddress.Loopback, _port);
            listener.Start();
            Console.WriteLine($"Master: Listening on port {_port}...");

            using (var client = await listener.AcceptTcpClientAsync())
            {
                Console.WriteLine($"Master: Connection accepted on port {_port}.");

                await using var stream = client.GetStream();
                using var reader = new StreamReader(stream, Encoding.UTF8);

                string? line;
                while ((line = await reader.ReadLineAsync()) != null)
                {
                    if (line == "END")
                        break;
                    files.Add(line);
                }
            }

            listener.Stop();
            Console.WriteLine($"Master: Finished receiving data on port {_port}, {files.Count} items received.");
            return files;
        }
    }
}