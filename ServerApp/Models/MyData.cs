using ServerApp.Services; 

namespace ServerApp.Models
{
    public class MyData
    {
        public int Id { get; set; }
        public string Name { get; set; } = "Lab 5 Data";
        
       
        public MyService? Service { get; set; }
    }
}
