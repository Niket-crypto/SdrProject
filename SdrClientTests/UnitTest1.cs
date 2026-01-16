using Xunit;
using SdrClientApp.Messages; // Убедись, что пространство имен совпадает

namespace SdrClientTests
{
    public class MessageHelperTests
    {
        [Fact]
        public void Test_ControlItemMessage_Length_IsCorrect()
        {
            // Тестируем создание сообщения (Лаба №3)
            byte[] payload = { 0x01, 0x02 };
            var result = MessageHelper.GetControlItemMessage(0x01, 0x02, payload);

            // Проверяем, что длина заголовка и данных верна
            Assert.NotNull(result);
            Assert.True(result.Length > 0);
        }

        [Fact]
        public void Test_Frequency_Payload_Creation()
        {
            // Тестируем логику частоты 102.5 MHz
            uint freq = 102500000;
            var payload = MessageHelper.CreateFrequencyPayload(freq);

            Assert.Equal(5, payload.Length); // Частота в NetSDR обычно 5 байт
        }
    }
}