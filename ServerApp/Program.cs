using System;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;

namespace ServerApp
{
    static class Program
    {
        // Використовуємо RandomNumberGenerator замість Random для безпеки (Security Hotspot)
        private static readonly RandomNumberGenerator _rng = RandomNumberGenerator.Create();

        static async Task Main(string[] args)
        {
            Console.Title = "SDR Hardware Emulator";
            const int tcpPort = 50000;
            const int udpPort = 60000;

            using var cts = new CancellationTokenSource();
            
            Console.WriteLine($"[Server] Starting emulator on TCP:{tcpPort}...");
            var listener = new TcpListener(IPAddress.Any, tcpPort);
            listener.Start();

            try 
            {
                // Використовуємо токен для коректного виходу з циклу
                while (!cts.IsCancellationRequested)
                {
                    Console.WriteLine("[Server] Waiting for client connection...");
                    
                    // Приймаємо клієнта
                    using var client = await listener.AcceptTcpClientAsync();
                    Console.WriteLine("[Server] Client connected! Emulating IQ data stream...");

                    using var udpClient = new UdpClient();
                    var endpoint = new IPEndPoint(IPAddress.Loopback, udpPort);

                    try
                    {
                        for (int i = 0; i < 100; i++)
                        {
                            byte[] dummyIqData = new byte[1024];
                            
                            // Заповнюємо дані безпечним рандомом (виправляє Security Hotspot)
                            _rng.GetBytes(dummyIqData); 
                            
                            await udpClient.SendAsync(dummyIqData, dummyIqData.Length, endpoint);

                            if (i % 10 == 0) 
                                Console.WriteLine($"[Server] Sent {i} packets of IQ data");

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
            catch (OperationCanceledException)
            {
                Console.WriteLine("[Server] Operation cancelled.");
            }
            finally
            {
                listener.Stop();
                _rng.Dispose(); // Звільняємо ресурси генератора
            }
        }
    }
}
