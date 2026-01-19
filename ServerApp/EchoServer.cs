namespace ServerApp
{
    public class EchoServer
    {
        private readonly IMessageWriter _writer;

   
        public EchoServer(IMessageWriter writer)
        {
            _writer = writer;
        }

        public string Process(string input)
        {
            string response = $"Echo: {input}";
            _writer.Write(response);
            return response;
        }
    }
}
