using IdentityModel;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace AuthorizationServer.Data
{
    public class TestData
    {
        public static void Seed(ConfigurationDbContext context)
        {
            if (!context.Clients.Any())
            {
                foreach (var client in Clients.Get())
                {
                    context.Clients.Add(client.ToEntity());
                }
                context.SaveChanges();
            }

            if (!context.IdentityResources.Any())
            {
                foreach (var resource in Resources.GetIdentityResources())
                {
                    context.IdentityResources.Add(resource.ToEntity());
                }
                context.SaveChanges();
            }

            if (!context.ApiScopes.Any())
            {
                foreach (var scope in Resources.GetApiScopes())
                {
                    context.ApiScopes.Add(scope.ToEntity());
                }
                context.SaveChanges();
            }

            if (!context.ApiResources.Any())
            {
                foreach (var resource in Resources.GetApiResources())
                {
                    context.ApiResources.Add(resource.ToEntity());
                }
                context.SaveChanges();
            }
        }

        public static void Seed(UserManager<IdentityUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new IdentityUser("alice")
                {
                    Id = "5be86359-073c-434b-ad2d-a3932222dabe",
                    Email = "alice@example.com",
                    EmailConfirmed = true
                };

                userManager.CreateAsync(user, "Password123!").Wait();
                userManager.AddClaimsAsync(user, new List<Claim>
                    {
                        new Claim(JwtClaimTypes.Email, "alice@example.com"),
                        new Claim(JwtClaimTypes.Role, "admin")
                    }).Wait();
            }
        }
    }
}
