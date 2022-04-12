using System;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace AuthorizationServer.IntegrationTests
{
    public class AccountControllerTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public AccountControllerTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            _factory.ClientOptions.AllowAutoRedirect = false;
            _factory.ClientOptions.BaseAddress = new Uri("https://localhost:5000/Account/");
        }

        [Fact]
        public async Task Get_Login_Page_Returns_Success()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync("Login");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Get_Logout_Page_Returns_Success()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync("Logout");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
