using Xunit;
using SdrClientApp.Messages;

namespace SdrClientTests
{
    public class MessageHelperTests
    {
        [Fact]
        public void Test_ControlItemMessage_NotNull()
        {
            byte[] payload = { 0x01 };
            var result = MessageHelper.GetControlItemMessage(0x01, 0x02, payload);
            Assert.NotNull(result);
        }

        [Fact]
        public void Test_ControlItemMessage_HeaderLength()
        {
            byte[] payload = { 0x01, 0x02, 0x03 };
            var result = MessageHelper.GetControlItemMessage(0x01, 0x02, payload);
            // Довжина має бути: 4 (header) + payload.Length
            Assert.Equal(7, result.Length);
        }

        [Fact]
        public void Test_Frequency_Payload_IsNotEmpty()
        {
            uint freq = 100000000;
            var payload = MessageHelper.CreateFrequencyPayload(freq);
            Assert.NotEmpty(payload);
        }

        [Fact]
        public void Test_Frequency_Payload_CorrectSize()
        {
            uint freq = 102500000;
            var payload = MessageHelper.CreateFrequencyPayload(freq);
            Assert.Equal(5, payload.Length); 
        }

        [Fact]
        public void Test_MessageHelper_NullPayload_ThrowsException()
        {
            // Перевірка надійності: що буде, якщо передати null
            Assert.Throws<System.ArgumentNullException>(() => MessageHelper.GetControlItemMessage(0x01, 0x02, null!));
        }

        [Fact]
        public void Test_Frequency_ZeroValue()
        {
            var payload = MessageHelper.CreateFrequencyPayload(0);
            Assert.All(payload, b => Assert.Equal(0, b));
        }
    }
}
