using AuthorizationServer.Data;
using IdentityServer4.EntityFramework.DbContexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
namespace MoneySmart.IntegrationTests.Helpers
{
    public static class DatabaseHelper
    {
        private static readonly object _lock = new object();
        private static bool _databaseInitialized;

        public static void SeedTestDatabase(ApplicationDbContext context, ConfigurationDbContext configurationDb, UserManager<IdentityUser> userManager)
        {
            lock (_lock)
            {
                if (!_databaseInitialized)
                {
                    context.Database.Migrate();
                    configurationDb.Database.Migrate();

                    TestData.Seed(configurationDb);
                    TestData.Seed(userManager);

                    configurationDb.SaveChanges();
                    context.SaveChanges();

                    _databaseInitialized = true;
                }
            }
        }
    }
}
