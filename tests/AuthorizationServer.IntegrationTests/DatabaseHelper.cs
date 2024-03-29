﻿using AuthorizationServer.Data;
using IdentityServer4.EntityFramework.DbContexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AuthorizationServer.IntegrationTests
{
    public static class DatabaseHelper
    {
        private static readonly object _lock = new object();
        private static bool _databaseInitialized;

        public static void SeedTestDatabase(ApplicationDbContext applicationDb, PersistedGrantDbContext grantsDb, ConfigurationDbContext configurationDb, UserManager<IdentityUser> userManager)
        {
            lock (_lock)
            {
                if (!_databaseInitialized)
                {
                    applicationDb.Database.Migrate();
                    grantsDb.Database.Migrate();
                    configurationDb.Database.Migrate();

                    SeedData.Seed(configurationDb);
                    SeedData.Seed(userManager);

                    applicationDb.SaveChanges();
                    grantsDb.SaveChanges();
                    configurationDb.SaveChanges();

                    _databaseInitialized = true;
                }
            }
        }
    }
}
