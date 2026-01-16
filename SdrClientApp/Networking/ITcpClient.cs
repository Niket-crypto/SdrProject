using System.Threading.Tasks;

namespace SdrClientApp.Networking
{
    public interface ITcpClient
    {
        bool Connected { get; }
        void Connect();
        void Disconnect();
        Task SendMessageAsync(byte[] data);
    }
}