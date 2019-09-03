using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RoomOccupancy.Application.Interfaces;
using RoomOccupancy.Domain.Entities.Campus;
using RoomOccupancy.Domain.Entities.Reservation;
using RoomOccupancy.Domain.Entities.Schedule;
using RoomOccupancy.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace RoomOccupancy.Persistence
{
    public class ReservationDbContext : IdentityDbContext<AppUser>, IReservationDbContext
    {
        public ReservationDbContext(DbContextOptions<ReservationDbContext> options)
         : base(options)
        {
            
        }
        public DbSet<Building> Buildings { get; set; }
        public DbSet<Disponent> Disponents { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Equipment> Equipment { get; set; }
        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Lecturer> Lecturers { get; set; }
        public DbSet<FacultyRoom> FacultyRooms { get; set; }
        public DbSet<BuildingFaculty> BuildingFaculties { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ReservationDbContext).Assembly);
        }

    }
}
