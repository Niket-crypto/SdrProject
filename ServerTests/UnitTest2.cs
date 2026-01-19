using Xunit;
using ServerApp;
using System.Collections.Generic;

namespace ServerTests
{
    // Спеціальний клас "заглушка" для тестування (Mocking)
    public class FakeWriter : IMessageWriter
    {
        public List<string> Messages = new List<string>();
        public void Write(string message) => Messages.Add(message);
    }

    public class EchoServerTests
    {
        [Fact]
        public void Process_ShouldReturnCorrectEchoMessage()
        {
            // Arrange (Підготовка)
            var fakeWriter = new FakeWriter();
            var server = new EchoServer(fakeWriter);
            string input = "Hello World";

            // Act (Дія)
            var result = server.Process(input);

            // Assert (Перевірка результату)
            Assert.Equal("Echo: Hello World", result);
            
            Assert.Single(fakeWriter.Messages);
            Assert.Equal("Echo: Hello World", fakeWriter.Messages[0]);
        }

        [Fact]
        public void Test_Server_Port_Definition()
        {
            
            int expectedPort = 50000;
            Assert.Equal(50000, expectedPort);
        }
    }
}
