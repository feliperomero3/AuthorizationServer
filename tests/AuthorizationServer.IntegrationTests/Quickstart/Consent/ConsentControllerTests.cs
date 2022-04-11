using System;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace AuthorizationServer.IntegrationTests
{
    public class ConsentControllerTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public ConsentControllerTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            _factory.ClientOptions.AllowAutoRedirect = false;
            _factory.ClientOptions.BaseAddress = new Uri("https://localhost:5000/Consent/");
        }

        [Fact()]
        public async Task IndexTestAsync()
        {
            var client = _factory.CreateClientWithAuthenticatedUser();

            var response = await client.GetAsync(string.Empty);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}