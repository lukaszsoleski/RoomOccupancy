using AutoMapper;
using MediatR;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RoomOccupancy.Application.Interfaces;
using RoomOccupancy.Persistence;
using System;
using System.Text;
using System.Threading.Tasks;

namespace RoomOccupancy.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            //fix .net core unsupported encoding issues
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            var host = CreateWebHostBuilder(args).Build();
            var logger = host.Services.GetRequiredService<ILogger<Program>>();
            
            AssertMappingConfigurationIsValid(host, logger);

            await MigrateDatabase(host);

            host.Run();
        }

        private static void AssertMappingConfigurationIsValid(IWebHost host, ILogger<Program> logger)
        {
            var mapper = host.Services.GetService<IMapper>();
            try { 
            mapper.ConfigurationProvider.AssertConfigurationIsValid();
            }
            catch(AutoMapperConfigurationException ace)
            {
                logger.LogWarning(ace.Message);
            }
        }

        private static async Task MigrateDatabase(IWebHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                try
                {
                    var context = (ReservationDbContext)scope.ServiceProvider.GetService<IReservationDbContext>();

                    context.Database.Migrate();

                    var mediatr = scope.ServiceProvider.GetService<IMediator>();

                    await new ReservationInitializer(context, mediatr).Initialize();
                }
                catch (Exception ex)
                {
                    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while migrating or initializing the database.");
                    throw;
                }
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureLogging((hostingContext, logging) =>
                {
                    logging.ClearProviders();
                    logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                    logging.AddConsole();
                    logging.AddDebug(); 
                })
                .UseStartup<Startup>();
    }
}