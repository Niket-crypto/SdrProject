using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace ServerApp
{
    class Program
    {
        // Використовуємо один екземпляр Random для всього класу (виправляє Reliability)
        private static readonly Random _random = new Random();

        static async Task Main(string[] args)
        {
            Console.Title = "SDR Hardware Emulator";
            int tcpPort = 50000;
            int udpPort = 60000;

            // Додаємо токен для коректної зупинки (гарна практика)
            using var cts = new CancellationTokenSource();
            
            Console.WriteLine($"[Server] Starting emulator on TCP:{tcpPort}...");
            var listener = new TcpListener(IPAddress.Any, tcpPort);
            listener.Start();

            try 
            {
                // Поки не скасовано токен
                while (!cts.IsCancellationRequested)
                {
                    Console.WriteLine("[Server] Waiting for client connection...");
                    using var client = await listener.AcceptTcpClientAsync();
                    Console.WriteLine("[Server] Client connected! Emulating IQ data stream...");

                    // UDP клієнт для імітації передачі сигналу
                    using var udpClient = new UdpClient();
                    var endpoint = new IPEndPoint(IPAddress.Loopback, udpPort);

                    try
                    {
                        // Імітуємо потік даних протягом 10 секунд
                        for (int i = 0; i < 100; i++)
                        {
                            byte[] dummyIqData = new byte[1024];
                            _random.NextBytes(dummyIqData); // Використовуємо статичний Random
                            
                            await udpClient.SendAsync(dummyIqData, dummyIqData.Length, endpoint);

                            if (i % 10 == 0) Console.WriteLine($"[Server] Sent {i} packets of IQ data");
                            await Task.Delay(100, cts.Token);
                        }
                    }
                    catch (OperationCanceledException) { break; }
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
