using Xunit;
using ServerApp;
using ServerApp.Models; 
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

        [Fact]
        public void ConsoleWriter_Write_ShouldNotThrowException()
        {
            // Arrange
            var writer = new ConsoleWriter();
            var message = "Test message";

            // Act & Assert
            var exception = Record.Exception(() => writer.Write(message));
            Assert.Null(exception);
        }
    }

  
    public class MyDataTests
    {
        [Fact]
        public void MyData_Property_ShouldSetAndGet()
        {
            // Arrange
            var data = new MyData();
            var testValue = "Lab 8 Clean Code";

            // Act
            data.Name = testValue;

            // Assert
            Assert.Equal(testValue, data.Name);
        }
    }
}
