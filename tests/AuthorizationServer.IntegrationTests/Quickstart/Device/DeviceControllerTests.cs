using System;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace AuthorizationServer.IntegrationTests.Device
{
    public class DeviceControllerTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public DeviceControllerTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            _factory.ClientOptions.AllowAutoRedirect = false;
            _factory.ClientOptions.BaseAddress = new Uri("https://localhost:5000/Device/");
        }

        [Fact()]
        public async Task Get_Device_Page_Returns_Success()
        {
            var client = _factory.CreateClientWithAuthenticatedUser();

            var response = await client.GetAsync(string.Empty);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        }
    }
}