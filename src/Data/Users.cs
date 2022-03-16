using IdentityModel;
using IdentityServer4.Test;
using System.Collections.Generic;
using System.Security.Claims;

namespace AuthorizationServer.Data
{
    internal class Users
    {
        internal static List<TestUser> Get()
        {
            return new List<TestUser>
            {
                new TestUser
                {
                    Username = "alice",
                    Claims = new List<Claim>
                    {
                        new Claim(JwtClaimTypes.Email, "alice@example.com"),
                        new Claim(JwtClaimTypes.Role, "admin")
                    }
                }
            };
        }
    }
}
