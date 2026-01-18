using NetArchTest.Rules;
using Xunit;
using System.Reflection;

namespace ServerTests
{
    public class ArchitectureTests
    {
        [Fact]
        public void Models_Should_Not_Depend_On_Services()
        {
           
            var assembly = Assembly.Load("ServerApp");

            
            var result = Types.InAssembly(assembly)
                .That()
                .ResideInNamespace("ServerApp.Models")
                .ShouldNot()
                .HaveDependencyOn("ServerApp.Services")
                .GetResult();

            Assert.True(result.IsSuccessful, "Архітектурна помилка: Models залежать від Services!");
        }
    }
}
