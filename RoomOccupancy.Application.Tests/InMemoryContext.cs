using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RoomOccupancy.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace RoomOccupancy.Application.Tests
{
    public static class InMemoryContext
    {

        public static ReservationDbContext Create()
        {
            var options = CreateNewContextOptions();

            var context = new ReservationDbContext(options);

            return context; 
        }
        private static DbContextOptions<ReservationDbContext> CreateNewContextOptions()
        {
            // Create a fresh service provider, and therefore a fresh 
            // InMemory database instance.
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            // Create a new options instance telling the context to use an
            // InMemory database and the new service provider.
            var builder = new DbContextOptionsBuilder<ReservationDbContext>();
            builder.UseInMemoryDatabase("Test")
                   .UseInternalServiceProvider(serviceProvider);
            return builder.Options;
        }
    }
}
