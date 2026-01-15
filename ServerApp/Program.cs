using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace ServerApp
{
    class Program
    {
        // Сонар вимагав статичний Random, щоб не створювати нові об'єкти в циклі
        private static readonly Random _random = new Random();

        static async Task Main(string[] args)
        {
            Console.Title = "SDR Hardware Emulator";
            int tcpPort = 50000;
            int udpPort = 60000;

            using var cts = new CancellationTokenSource();
            
            Console.WriteLine($"[Server] Starting emulator on TCP:{tcpPort}...");
            var listener = new TcpListener(IPAddress.Any, tcpPort);
            listener.Start();

            try 
            {
                while (!cts.IsCancellationRequested)
                {
                    Console.WriteLine("[Server] Waiting for client connection...");
                    using var client = await listener.AcceptTcpClientAsync();
                    Console.WriteLine("[Server] Client connected! Emulating IQ data stream...");

                    using var udpClient = new UdpClient();
                    var endpoint = new IPEndPoint(IPAddress.Loopback, udpPort);

                    try
                    {
                        for (int i = 0; i < 100; i++)
                        {
                            byte[] dummyIqData = new byte[1024];
                            _random.NextBytes(dummyIqData); // Тепер Reliability буде "A"
                            
                            await udpClient.SendAsync(dummyIqData, dummyIqData.Length, endpoint);

                            if (i % 10 == 0) Console.WriteLine($"[Server] Sent {i} packets of IQ data");
                            await Task.Delay(100, cts.Token);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"[Server] Stream error: {ex.Message}");
                    }
                    Console.WriteLine("[Server] Session ended.\n");
                }
            }
            finally
            {
                listener.Stop();
            }
        }
    }
}
