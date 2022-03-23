using AuthorizationServer.Data;
using IdentityServer4.EntityFramework.DbContexts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;

namespace AuthorizationServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft.AspNetCore.Mvc", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.AspNetCore.StaticFiles", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.AspNetCore.DataProtection", LogEventLevel.Information)
                .MinimumLevel.Override("Microsoft.AspNetCore.Routing", LogEventLevel.Information)
                .MinimumLevel.Override("Microsoft.Extensions.Http", LogEventLevel.Information)
                .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Information)
                .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}")
                .WriteTo.Debug()
                .CreateLogger();

            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    Log.Information("Seeding database...");

                    var grantsDb = services.GetRequiredService<PersistedGrantDbContext>();

                    grantsDb.Database.Migrate();

                    var configurationDb = services.GetRequiredService<ConfigurationDbContext>();

                    configurationDb.Database.Migrate();

                    var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
                    var applicationDb = services.GetRequiredService<ApplicationDbContext>();

                    applicationDb.Database.Migrate();

                    TestData.Seed(configurationDb);
                    TestData.Seed(userManager);

                    Log.Information("Done seeding database.");
                }
                catch (SqlException e)
                {
                    Log.Error(e, "An error occurred creating the Database.");

                    Log.CloseAndFlush();

                    throw;
                }
            }

            Log.Information("Starting web host.");

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
