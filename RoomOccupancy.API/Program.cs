using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RoomOccupancy.Application.Interfaces;
using RoomOccupancy.Persistence;

namespace RoomOccupancy.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            //fix .net core unsupported encoding issues
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            var host = CreateWebHostBuilder(args).Build();

            await MigrateDatabase(host);

            host.Run();


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
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();


    }
}
