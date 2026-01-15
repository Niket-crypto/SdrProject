using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace SdrClientApp.Networking
{
    public class UdpWrapper : IUdpClient
    {
        private UdpClient? _udpClient;
        private readonly int _port;
        private bool _isListening;
        // ВИДАЛЕНО: private Task? _listenTask; (Сонар сварився на unused field)

        public event EventHandler<byte[]>? MessageReceived;

        public UdpWrapper(int port = 60000)
        {
            _port = port;
        }

        public async Task StartListeningAsync()
        {
            if (_isListening) return; // Спростили перевірку

            _udpClient = new UdpClient(_port);
            _isListening = true;

            Console.WriteLine($"[UDP] Listening for IQ data on port {_port}...");

            // Запускаємо фонову задачу без збереження її в поле, 
            // оскільки ми не чекаємо її завершення через await в іншому місці
            _ = Task.Run(async () =>
            {
                while (_isListening)
                {
                    try
                    {
                        if (_udpClient == null) break;
                        var result = await _udpClient.ReceiveAsync();
                        MessageReceived?.Invoke(this, result.Buffer);
                    }
                    catch (ObjectDisposedException) { break; }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"[UDP] Error: {ex.Message}");
                    }
                }
            });

            await Task.CompletedTask;
        }

        public void StopListening()
        {
            _isListening = false;
            _udpClient?.Close();
            _udpClient = null;
            Console.WriteLine("[UDP] Stopped listening.");
        }
    }
}
