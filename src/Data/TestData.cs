﻿using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.AspNetCore.Identity;
using System.Linq;

namespace AuthorizationServer.Data
{
    internal class TestData
    {
        internal static void Seed(ConfigurationDbContext context)
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

        internal static void Seed(UserManager<IdentityUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                foreach (var testUser in Users.Get())
                {
                    var user = new IdentityUser(testUser.Username)
                    {
                        Id = testUser.SubjectId,
                        Email = "alice@example.com",
                        EmailConfirmed = true
                    };

                    userManager.CreateAsync(user, "Password123!").Wait();
                    userManager.AddClaimsAsync(user, testUser.Claims.ToList()).Wait();
                }
            }
        }
    }
}
