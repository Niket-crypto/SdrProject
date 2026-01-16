using System;
using System.Threading.Tasks;
using System.IO;
using SdrClientApp.Networking;
using SdrClientApp.Messages;

namespace SdrClientApp
{
    public class RadioClient
    {
        private readonly ITcpClient _control;
        private readonly IUdpClient _data;
        private readonly string _logFile = "iq_samples.bin";

        public RadioClient(ITcpClient control, IUdpClient data)
        {
            _control = control;
            _data = data;
            // Підписка на подій отримання даних (Лаба №2)
            _data.MessageReceived += HandleIncomingIqSamples;
        }

        public async Task ConnectAsync()
        {
            if (!_control.Connected)
            {
                _control.Connect();

                // Надсилаємо команду старту приймача через MessageHelper
                byte[] startPayload = { 1 }; // 1 = ON
                byte[] command = MessageHelper.CreateCaptureCommand(MessageHelper.CID_RECEIVER_STATE, startPayload);
                await _control.SendMessageAsync(command);

                // Запускаємо прослуховування UDP для IQ-даних
                await _data.StartListeningAsync();
            }
        }

        public async Task ChangeFrequencyAsync(long frequencyHz)
        {
            if (_control.Connected)
            {
                // Формуємо параметри: 1 канал + 5 байт частоти
                byte[] freqData = MessageHelper.FormatFrequency(frequencyHz);
                byte[] payload = new byte[6];
                payload[0] = 1; // Channel ID
                Array.Copy(freqData, 0, payload, 1, 5);

                byte[] command = MessageHelper.CreateCaptureCommand(MessageHelper.CID_RECEIVER_FREQUENCY, payload);
                await _control.SendMessageAsync(command);

                Console.WriteLine($"[Radio] Command sent: Set frequency to {frequencyHz} Hz");
            }
        }

        private void HandleIncomingIqSamples(object? sender, byte[] buffer)
        {

            using (FileStream fs = new FileStream(_logFile, FileMode.Append, FileAccess.Write))
            {
                fs.Write(buffer, 0, buffer.Length);
            }// Логіка запису у файл (Лаба №2)

           
        }

        public void Disconnect()
        {
            _control.Disconnect();
            _data.StopListening();
        }
    }
}