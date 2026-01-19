using Xunit;
using ServerApp;
using System.Collections.Generic;

namespace ServerTests
{
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
            // Arrange
            var fakeWriter = new FakeWriter();
            var server = new EchoServer(fakeWriter);
            string input = "Hello World";

            // Act
            var result = server.Process(input);

            // Assert
            Assert.Equal("Echo: Hello World", result);
            Assert.Single(fakeWriter.Messages);
            Assert.Equal("Echo: Hello World", fakeWriter.Messages[0]);
        }
    }
}
