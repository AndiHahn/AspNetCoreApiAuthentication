using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WebAppAuthentication.Database;

namespace WebAppAuthentication
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                var loggerFactory = services.GetRequiredService<ILoggerFactory>();
                var logger = loggerFactory.CreateLogger<Program>();

                logger.LogInformation("Starting database migration");
                try
                {
                    var jwtContext = services.GetRequiredService<JwtAuthenticationDbContext>();
                    await jwtContext.Database.MigrateAsync();
                }
                catch (Exception exception)
                {
                    logger.LogError(exception, "Error by migrating the database");
                }

                logger.LogInformation("Database migration was successful.");
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
