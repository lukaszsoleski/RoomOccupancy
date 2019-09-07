using ExcelMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RoomOccupancy.Application.Campus.Equipment;
using RoomOccupancy.Application.Campus.Equipment.Commands.ImportRoomEquipment;
using RoomOccupancy.Application.Campus.Rooms.Commands.ImportRooms;
using RoomOccupancy.Application.Infrastructure.Mapping;
using RoomOccupancy.Application.Infrastructure.Mapping.Excel;
using RoomOccupancy.Application.Reservations.Commands.ImportSchedule;
using RoomOccupancy.Domain.Entities.Campus;
using RoomOccupancy.Domain.Entities.Reservation;
using RoomOccupancy.Domain.Entities.Users;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace RoomOccupancy.Persistence
{
    public class ReservationInitializer
    {
        private readonly ReservationDbContext _context;
        private readonly IMediator _mediator;
        private readonly string initializationDir;
        // TODO: Add folder scan with name conventions
        #region initialization files
        private readonly string roomsFileName = "Pomieszczenia.xlsx";
        private readonly string wzimRoomsFileName = "SaleWzim.xlsx";
        private readonly string buildingsFileName = "Budynki.xlsx";
        private readonly string facultiesFileName = "Wydziały.xlsx";
        private readonly string scheduleFileName = "Zajętość.xlsx";
        private readonly string equipmentFileName = "Wyposażenie-słownik.xlsx";
        private readonly string roomEquipmentFileName = "WyposażeniePomieszczeń.xlsx";
        private readonly string verifiedUserFileName = "ZweryfikowniUżytkownicy.xlsx";
        #endregion
        public ReservationInitializer(ReservationDbContext context, IMediator mediator)
        {
            _mediator = mediator;

            _context = context;

            initializationDir = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Initializer\Files\");
        }

        public async Task Initialize()
        {
#if DEBUG
            //_context.Database.EnsureDeleted();
            //_context.Database.Migrate();
#endif
            _context.Database.EnsureCreated();

            if (_context.Rooms.Any())
                // database has been seeded
                return;

            await SeedBuildings();
            await SeedFaculties();
            await SeedRooms();
            await SeedReservations();
            await SeedEquipment();
            await SeedRoomEquipment();
            await SeedVerifiedUsers();
        }

        private Task SeedVerifiedUsers() => ExcelSeed<VerifiedUsersDict, VerifiedUserClassMap>(verifiedUserFileName);

        private async Task SeedRoomEquipment()
        {
            var path = Path.Combine(initializationDir, roomEquipmentFileName);
            var content = File.ReadAllBytes(path);
            var equipment = ExcelHelper.Load<EquipmentModel, RoomEquipmentClassMap>(content);
            await _mediator.Send(new ImportRoomEquipmentCommand() { Equipment = equipment });
        }

        private Task SeedEquipment() => ExcelSeed<Equipment, EquipmentClassMap>(equipmentFileName);

        private async Task SeedReservations()
        {
            var path = Path.Combine(initializationDir, scheduleFileName);
            var content = File.ReadAllBytes(path);
            await _mediator.Send(new ImportScheduleCommand() { FileContent = content });
        }

        private Task SeedFaculties() => ExcelSeed<Faculty, FacultyClassMap>(facultiesFileName);

        private Task SeedBuildings() => ExcelSeed<Building, BuildingClassMap>(buildingsFileName);

        private async Task SeedRooms()
        {
            var b23Path = Path.Combine(initializationDir, roomsFileName);
            var b34Path = Path.Combine(initializationDir, wzimRoomsFileName);
            var b23Content = File.ReadAllBytes(b23Path);
            var b34Content = File.ReadAllBytes(b34Path);
            await _mediator.Send(new ImportRoomsCommand() { File = b23Content });
            await _mediator.Send(new ImportRoomsCommand() { File = b34Content });
        }

        private async Task ExcelSeed<TEntity, TConfiguration>(string fileName)
           where TConfiguration : ExcelClassMap<TEntity>, new()
        {
            var path = Path.Combine(initializationDir, fileName);
            var file = File.ReadAllBytes(path);
            var entities = ExcelHelper.Load<TEntity, TConfiguration>(file).ToList();

            entities.ForEach(x => _context.Entry(x).State = EntityState.Added);

            await _context.SaveChangesAsync();
        }
    }
}