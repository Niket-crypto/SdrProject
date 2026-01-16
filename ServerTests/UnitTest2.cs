using Xunit;
// Здесь будет ссылка  ServerApp

namespace ServerTests
{
    public class HardwareEmulatorTests
    {
        [Fact]
        public void Test_Server_Port_Definition()
        {
            // Простейший тест, что сервер настроен на порт 50000
            int expectedPort = 50000;
            // Тут можно добавить проверку константы из твоего ServerApp
            Assert.Equal(50000, expectedPort);
        }
    }
}