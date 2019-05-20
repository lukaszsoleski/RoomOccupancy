using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Northwind.Persistence.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace RoomOccupancy.Persistence
{
    public class ReservationDbContextFactory : DesignTimeDbContextFactoryBase<ReservationDbContext>
    {
        protected override ReservationDbContext CreateNewInstance(DbContextOptions<ReservationDbContext> options)
        {
            return new ReservationDbContext(options);
        }
    }
}
