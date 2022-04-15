using System;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace AuthorizationServer.IntegrationTests.Account
{
    public class ExternalControllerTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public ExternalControllerTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            _factory.ClientOptions.AllowAutoRedirect = false;
            _factory.ClientOptions.BaseAddress = new Uri("https://localhost:5000/External/");
        }

        [Fact()]
        public async Task Challenge_Returns_Redirect_When_Unauthenticated_Request()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync("Challenge");

            Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
        }
    }
}