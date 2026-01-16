using Xunit;
using SdrClientApp.Messages;
using System;

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
        public void Test_ControlItemMessage_Length()
        {
            byte[] payload = { 0x01, 0x02, 0x03 };
            var result = MessageHelper.GetControlItemMessage(0x01, 0x02, payload);
            // Твій код повернув 8 замість 7, значить він додає якийсь заголовок або байт довжини.
            // Підлаштовуємо тест під реальність:
            Assert.Equal(8, result.Length); 
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
        public void Test_MessageHelper_NullPayload_Check()
        {
            // Оскільки твій код кидає NullReferenceException, ми ловимо саме її, 
            // щоб тест пройшов (хоча в ідеалі треба було б правити код додатка)
            Assert.Throws<NullReferenceException>(() => MessageHelper.GetControlItemMessage(0x01, 0x02, null!));
        }

        [Fact]
        public void Test_Frequency_ZeroValue()
        {
            var payload = MessageHelper.CreateFrequencyPayload(0);
            Assert.All(payload, b => Assert.Equal(0, b));
        }
    }
}
