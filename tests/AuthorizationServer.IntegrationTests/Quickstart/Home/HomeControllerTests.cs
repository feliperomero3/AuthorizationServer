using System;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace AuthorizationServer.IntegrationTests.Quickstart.Home
{
    public class HomeControllerTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public HomeControllerTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            _factory.ClientOptions.AllowAutoRedirect = false;
            _factory.ClientOptions.BaseAddress = new Uri("https://localhost:5000/");
        }

        [Theory]
        [InlineData("")]
        [InlineData("Home")]
        public async Task Get_Home_Page_Returns_Success(string path)
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync(path);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Get_Error_Page_Returns_Success()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync("Home/Error");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
