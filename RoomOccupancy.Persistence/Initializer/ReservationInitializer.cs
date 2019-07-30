using RoomOccupancy.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RoomOccupancy.Persistence
{
    public static class ReservationInitializer
    {

        public static void Initialize(ReservationDbContext dbContext)
        {
            dbContext.Database.EnsureCreated();

            if (dbContext.Rooms.Any())
                // database has been seeded
                return;
            SeedRooms(dbContext);

        }

        private static void SeedRooms(ReservationDbContext dbContext)
        {
            
        }
    }
}
