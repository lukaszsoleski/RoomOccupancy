using ExcelMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RoomOccupancy.Application.Campus.Rooms.Commands.ImportRooms;
using RoomOccupancy.Application.Infrastructure.Mapping;
using RoomOccupancy.Application.Infrastructure.Mapping.Excel;
using RoomOccupancy.Domain.Entities.Campus;
using RoomOccupancy.Domain.Entities.Reservation;
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
        private readonly string buildingRooms23 = "Zajetosc_Sal_do_POLON_budynek_23.xlsx";
        private readonly string buildingRooms34 = "SaleWzim.xlsx";
        private readonly string buildingsFile = "Budynki.xlsx";
        private readonly string facultiesFile = "Wydziały.xlsx";
        private readonly string scheduleFile = "Tst1_Zajetosc_sal_WZIM_2018_Wiosna.xlsx";
        #endregion
        public ReservationInitializer(ReservationDbContext context, IMediator mediator)
        {
            _mediator = mediator;

            _context = context;

            initializationDir = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Initializer\Files\");
        }

        public async Task Initialize()
        {
            _context.Database.EnsureDeleted();
            _context.Database.Migrate(); 
            _context.Database.EnsureCreated();

            if (_context.Rooms.Any())
                // database has been seeded
                return;
            
            await SeedBuildings();
            await SeedFaculties();
            await SeedRooms();
            await SeedReservations();
        }
        // TODO: implement logic for this
        private async Task SeedReservations() => await Task.CompletedTask;

        private async Task SeedFaculties() => await ExcelSeed<Faculty, FacultyClassMap>(facultiesFile);

        private async Task SeedBuildings() => await ExcelSeed<Building, BuildingClassMap>(buildingsFile);

        private async Task SeedRooms()
        {
            var b23Path = Path.Combine(initializationDir, buildingRooms23);
            var b34Path = Path.Combine(initializationDir, buildingRooms34);
            var b23Content = File.ReadAllBytes(b23Path);
            var b34Content = File.ReadAllBytes(b34Path); 
            await _mediator.Send(new ImportRoomsCommand() { File = b23Content });
            //TODO: No Columns found matching predicate from ["Budynek", "Nr pom.", "Kondygnacja, na której znajduje się pomieszczenie", "Liczba miejsc (studentów)**", "Opis", "Dysponent", "nazwa", "projekt"]
            // basically if any of the columns are missing, an exception is thrown
            // try to just ignore it if it is nullable column or add dummy heading if library does not support it 
            await _mediator.Send(new ImportRoomsCommand() { File = b34Content });
        }

        private async Task ExcelSeed<TEntity, TConfiguration>(string filePath)
           where TConfiguration : ExcelClassMap<TEntity>, new()
        {
            var path = Path.Combine(initializationDir, filePath);
            var file = File.ReadAllBytes(path);
            var entities = ExcelHelper.Load<TEntity, TConfiguration>(file).ToList();

            entities.ForEach(x => _context.Entry(x).State = EntityState.Added);

            await _context.SaveChangesAsync();
        }
    }
}