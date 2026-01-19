using Xunit;
using ServerApp;
using System;
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
            var fakeWriter = new FakeWriter();
            var server = new EchoServer(fakeWriter);
            string result = server.Process("Hello");

            Assert.Equal("Echo: Hello", result);
            Assert.Contains("Echo: Hello", fakeWriter.Messages);
        }

        [Fact]
        public void Constructor_ShouldThrowException_WhenWriterIsNull()
        {
            
            Assert.Throws<ArgumentNullException>(() => new EchoServer(null!));
        }
    }
}
