using Microsoft.AspNetCore.Mvc.Testing;
using System.Threading.Tasks;
using Xunit;

namespace BeekeepingDiary.Tests.Controllers
{
    public class HomeControllerSystemTest: IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> factory;

        public HomeControllerSystemTest(WebApplicationFactory<Startup> factory)
            => this.factory = factory;

        [Fact]
        public async Task IndexShouldReturnCorrectStatusCode()
        {
            // Arrange
            var client = this.factory.CreateClient();

            // Act
            var result = await client.GetAsync("/");

            // Arrange
            Assert.True(result.IsSuccessStatusCode);
        }
    }
}
