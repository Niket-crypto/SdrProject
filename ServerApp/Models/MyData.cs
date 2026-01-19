using ServerApp.Services;

namespace ServerApp.Models
{
    public class MyData
    {
        public string Name { get; set; } = string.Empty;
        
        public string GetFormattedInfo(IMyService service)
        {
            if (service == null) return Name;
            return $"{Name} - {service.GetServiceStatus()}";
        }
    }
}
