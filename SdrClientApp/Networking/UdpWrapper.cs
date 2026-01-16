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
        private Task? _listenTask;

        public event EventHandler<byte[]>? MessageReceived;

        public UdpWrapper(int port = 60000)
        {
            _port = port;
        }

        public async Task StartListeningAsync()
        {
            if (_isStreaming) return;

            _udpClient = new UdpClient(_port);
            _isListening = true;

            Console.WriteLine($"[UDP] Listening for IQ data on port {_port}...");

            _listenTask = Task.Run(async () =>
            {
                while (_isListening)
                {
                    try
                    {
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

        private bool _isStreaming => _isListening;

        public void StopListening()
        {
            _isListening = false;
            _udpClient?.Close();
            _udpClient = null;
            Console.WriteLine("[UDP] Stopped listening.");
        }
    }
}