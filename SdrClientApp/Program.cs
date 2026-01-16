using System;
using System.Threading.Tasks;
using SdrClientApp;
using SdrClientApp.Networking;

namespace SdrClientApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.Title = "SDR Client Console";
            Console.WriteLine("========================================");
            Console.WriteLine("   NETSDR CLIENT v1.0 (Lab Edition)    ");
            Console.WriteLine("========================================\n");

            // Ініціалізація обгортки для TCP (50000) та UDP (60000)
            var tcp = new TcpWrapper("127.0.0.1", 50000);
            var udp = new UdpWrapper(60000);

            var radio = new RadioClient(tcp, udp);

            Console.WriteLine("Commands:");
            Console.WriteLine("[C] - Connect to Receiver");
            Console.WriteLine("[F] - Set Frequency (102.5 MHz)");
            Console.WriteLine("[D] - Disconnect and Stop");
            Console.WriteLine("[Q] - Quit Application\n");

            bool running = true;
            while (running)
            {
                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(true).Key;
                    switch (key)
                    {
                        case ConsoleKey.C:
                            await radio.ConnectAsync();
                            break;
                        case ConsoleKey.F:
                            await radio.ChangeFrequencyAsync(102500000);
                            break;
                        case ConsoleKey.D:
                            radio.Disconnect();
                            break;
                        case ConsoleKey.Q:
                            radio.Disconnect();
                            running = false;
                            break;
                    }
                }
                await Task.Delay(100); // Щоб не вантажити процесор
            }
        }
    }
}