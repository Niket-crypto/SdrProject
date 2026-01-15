using System;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace SdrClientApp.Networking
{
    public class TcpWrapper : ITcpClient
    {
        private TcpClient? _client;
        private NetworkStream? _stream;
        private readonly string _host;
        private readonly int _port;

        public bool Connected => _client != null && _client.Connected;

        public TcpWrapper(string host, int port)
        {
            _host = host;
            _port = port;
        }

        public void Connect()
        {
            try
            {
                _client = new TcpClient(_host, _port);
                _stream = _client.GetStream();
                Console.WriteLine($"[TCP] Successfully connected to {_host}:{_port}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[TCP] Connection error: {ex.Message}");
            }
        }

        public void Disconnect()
        {
            _stream?.Close();
            _client?.Close();
            _client = null;
            _stream = null; // Обнуляємо стрім для безпеки
            Console.WriteLine("[TCP] Connection closed.");
        }

        public async Task SendMessageAsync(byte[] data)
        {
            // Сонар просить перевіряти data на null (правило CA1062)
            if (data == null) return;

            if (Connected && _stream != null)
            {
                try
                {
                    // ВИПРАВЛЕННЯ: використовуємо Memory замість (byte[], int, int)
                    // Це прибирає попередження RSPEC-4487 / CS8604
                    await _stream.WriteAsync(data.AsMemory(0, data.Length));
                    await _stream.FlushAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[TCP] Send error: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("[TCP] Cannot send message: Not connected.");
            }
        }
    }
}
