namespace ServerApp.Services
{
    public interface IMyService
    {
        string GetServiceStatus();
    }

    public class MyService : IMyService
    {
        public string GetServiceStatus() => "Service is running optimally";
    }
}
