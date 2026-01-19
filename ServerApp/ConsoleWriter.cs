using System;

namespace ServerApp
{
    public class ConsoleWriter : IMessageWriter
    {
        public void Write(string message) => Console.WriteLine(message);
    }
}
