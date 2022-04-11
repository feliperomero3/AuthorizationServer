using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

namespace AuthorizationServer.IntegrationTests
{
    public static class WebHostBuilderExtensions
    {
        public static IWebHostBuilder WithAuthentication(this IWebHostBuilder builder)
        {
            return builder.ConfigureTestServices(services =>
            {
                services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = "Test";
                }).AddScheme<AuthenticationSchemeOptions, TestAuthenticationHandler>("Test", options => { });
            });
        }
    }
}
