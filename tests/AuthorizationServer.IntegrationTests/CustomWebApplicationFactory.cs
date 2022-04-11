﻿using AuthorizationServer.Data;
using IdentityServer4.EntityFramework.DbContexts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using System.IO;

namespace AuthorizationServer.IntegrationTests
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft.AspNetCore.Mvc", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.AspNetCore.StaticFiles", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.AspNetCore.DataProtection", LogEventLevel.Information)
                .MinimumLevel.Override("Microsoft.AspNetCore.Routing", LogEventLevel.Information)
                .MinimumLevel.Override("Microsoft.Extensions.Http", LogEventLevel.Information)
                .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Information)
                .WriteTo.Debug(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}")
                .CreateLogger();

            var projectDir = Directory.GetCurrentDirectory();
            var configPath = Path.Combine(projectDir, "appsettings.json");

            builder.ConfigureAppConfiguration((context, config) =>
            {
                config.AddJsonFile(configPath);
            });

            builder.UseEnvironment("Development");

            builder.ConfigureTestServices(services =>
            {
                var sp = services.BuildServiceProvider();

                using var scope = sp.CreateScope();

                var provider = scope.ServiceProvider;

                var context = provider.GetRequiredService<ApplicationDbContext>();

                try
                {
                    var grantsDb = provider.GetRequiredService<PersistedGrantDbContext>();

                    grantsDb.Database.Migrate();

                    var configurationDb = provider.GetRequiredService<ConfigurationDbContext>();

                    var userManager = provider.GetRequiredService<UserManager<IdentityUser>>();
                    var applicationDb = provider.GetRequiredService<ApplicationDbContext>();

                    DatabaseHelper.SeedTestDatabase(context, configurationDb, userManager);
                }
                catch (SqlException sqlException)
                {
                    var logger = provider.GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();

                    logger.LogError(sqlException, "An error occurred creating the test database.");

                    throw;
                }
            });
        }
    }
}
