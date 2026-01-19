using ServerApp.Models;
using ServerApp.Services;
using Xunit;

namespace ServerTests
{
    public class SimpleUnitTests
    {
        [Fact]
        public void Test_MyData_Properties()
        {
            var data = new MyData { Id = 1, Description = "Test" };
            Assert.Equal(1, data.Id);
            Assert.Equal("Test", data.Description);
        }

        [Fact]
        public void Test_MyService_Method()
        {
            var service = new MyService();
            var status = service.GetStatus();
            Assert.Equal("Service is running", status);
        }
    }
}
