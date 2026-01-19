using Xunit;
using ServerApp;
using ServerApp.Models; // Виправлено: додано для доступу до класу MyData
using System;
using System.Collections.Generic;

namespace ServerTests
{
    // Допоміжний клас для тестів
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
            
            // Act
            string result = server.Process("Hello");

            // Assert
            Assert.Equal("Echo: Hello", result);
            Assert.Contains("Echo: Hello", fakeWriter.Messages);
        }

        [Fact]
        public void Constructor_ShouldThrowException_WhenWriterIsNull()
        {
            // Assert
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
        public void MyData_Name_ShouldStoreAndRetrieveValue()
        {
            // Arrange
            var data = new MyData();
            var expectedValue = "Lab 8 Status: Green";

            // Act
            data.Name = expectedValue;

            // Assert
            Assert.Equal(expectedValue, data.Name);
        }
    }
}
