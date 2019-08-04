using Microsoft.EntityFrameworkCore;
using RoomOccupancy.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace RoomOccupancy.Application.Tests
{
    public static class InMemoryContext
    {
        public static ReservationDbContext Inst =>
            new ReservationDbContext(
                new DbContextOptionsBuilder<ReservationDbContext>()
                .UseInMemoryDatabase("TestDb")
                .Options
            ); 
    }
}
