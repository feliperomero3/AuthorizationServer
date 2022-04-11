using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http;
using System.Net.Http.Headers;

namespace AuthorizationServer.IntegrationTests
{
    public static class WebApplicationFactoryExtensions
    {
        public static HttpClient CreateClientWithAuthenticatedUser(this WebApplicationFactory<Startup> factory)
        {
            var client = factory.WithWebHostBuilder(builder =>
            {
                builder.WithAuthentication();
            }).CreateClient();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Test");

            return client;
        }
    }
}
