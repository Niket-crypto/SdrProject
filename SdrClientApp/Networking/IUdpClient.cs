using System;
using System.Threading.Tasks;

namespace SdrClientApp.Networking
{
    public interface IUdpClient
    {
        event EventHandler<byte[]> MessageReceived;
        Task StartListeningAsync();
        void StopListening();
    }
}