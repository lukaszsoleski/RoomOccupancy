using Microsoft.EntityFrameworkCore;
using RoomOccupancy.Domain.Entities.Campus;
using RoomOccupancy.Domain.Entities.Reservation;
using RoomOccupancy.Domain.Entities.Schedule;
using System.Threading;
using System.Threading.Tasks;

namespace RoomOccupancy.Application.Interfaces
{
    public interface IReservationDbContext
    {
        #region Tables
        DbSet<Building> Buildings { get; set; }
        DbSet<Disponent> Disponents { get; set; }
        DbSet<Room> Rooms { get; set; }
        DbSet<Equipment> Equipment { get; set; }
        DbSet<Faculty> Faculties { get; set; }
        DbSet<Reservation> Reservations { get; set; }
        DbSet<Course> Courses { get; set; }
        DbSet<DegreeProgramme> DegreeProgrammes { get; set; }
        DbSet<Lecturer> Lecturers { get; set; }
        DbSet<Schedule> Schedules { get; set; }
        DbSet<FacultyRoom> FacultyRooms { get; set; }
        #endregion
        #region EF Core methods 
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
        #endregion
    }
}