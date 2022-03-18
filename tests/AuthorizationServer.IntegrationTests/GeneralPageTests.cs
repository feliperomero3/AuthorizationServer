using AuthorizationServer;
using System;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace MoneySmart.IntegrationTests.Pages
{
    public class GeneralPageTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public GeneralPageTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            _factory.ClientOptions.AllowAutoRedirect = false;
            _factory.ClientOptions.BaseAddress = new Uri("https://localhost:5000/");
        }

        [Fact]
        public async Task Get_Home_Page_Returns_Success()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync(string.Empty);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
