using System.Collections.Generic;
using IdentityServer4.Models;

namespace AuthorizationServer.Data
{
    internal static class Resources
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
                new IdentityResource
                {
                    Name = "role",
                    UserClaims = new List<string> { "role" }
                }
            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new[]
            {
                new ApiResource
                {
                    Name = "api1",
                    DisplayName = "API #1",
                    Description = "Allow the application to access API #1 on your behalf.",
                    Scopes = new List<string> { "api1.scope1", "api1.scope2" },
                    ApiSecrets = new List<Secret> { new Secret("ApiSecret".Sha256()) },
                    UserClaims = new List<string> { "role" }
                }
            };
        }

        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new[]
            {
                new ApiScope("api1.scope1", "scope1 access to API #1"),
                new ApiScope("api1.scope2", "scope2 access to API #1")
            };
        }
    }
}
