using System.Collections.Generic;
using IdentityServer4;
using IdentityServer4.Models;

namespace AuthorizationServer.Data
{
    internal static class Clients
    {
        public static IEnumerable<Client> Get()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "oauthClient",
                    ClientName = "Example client application using client credentials.",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = new List<Secret> {new Secret("SuperSecretPassword".Sha256())},
                    AllowedScopes = new List<string> {"api1.scope1"}
                },
                new Client
                {
                    ClientId = "js-app",
                    ClientName = "JavaScript Client",
                    AllowedGrantTypes = new[] { GrantType.Implicit },
                    RedirectUris = { "http://localhost:4000/callback.html" },
                    PostLogoutRedirectUris = { "http://localhost:4000/index.html" },
                    AllowedCorsOrigins = {"http://localhost:4000"},
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email
                    }
                },
            };
        }
    }
}
