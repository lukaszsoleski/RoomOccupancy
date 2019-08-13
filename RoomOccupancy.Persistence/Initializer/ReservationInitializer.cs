using ExcelMapper;
using MediatR;
using RoomOccupancy.Application.Campus.Rooms.Commands.ImportRooms;
using RoomOccupancy.Application.Infrastructure.Mapping;
using RoomOccupancy.Domain.Entities.Campus;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RoomOccupancy.Persistence
{
    public class ReservationInitializer
    {
        private readonly ReservationDbContext _context;
        private readonly IMediator _mediator;
        private readonly string initializationDir;
        private readonly string roomsFile = "Zajetosc_Sal_do_POLON_budynek_23.xlsx";
        private readonly string buildingsFile = "Budynki.xlsx";
        private readonly string facultiesFile = "Wydziały.xlsx";

        public ReservationInitializer(ReservationDbContext context, IMediator mediator)
        {
            _mediator = mediator;

            _context = context;

            initializationDir = Path.Combine(Directory.GetCurrentDirectory(), @"Initializer\Files\");
        }

        public async Task Initialize()
        {
            _context.Database.EnsureCreated();

            if (_context.Rooms.Any())
                // database has been seeded
                return;

            await SeedBuildings();
            await SeedFaculties();
            await SeedRooms();
        }

        private async Task SeedFaculties() => await ExcelSeed<Faculty, FacultyClassMap>(facultiesFile);

        private async Task SeedBuildings() => await ExcelSeed<Building, BuildingClassMap>(buildingsFile);

        private async Task SeedRooms()
        {
            var path = Path.Combine(initializationDir, roomsFile);

            var content = File.ReadAllBytes(path);

            await _mediator.Send(new ImportRoomsCommand() { File = content });
        }

        private async Task ExcelSeed<TEntity, TConfiguration>(string filePath)
           where TConfiguration : ExcelClassMap<TEntity>, new()
        {
            var path = Path.Combine(initializationDir, filePath);
            var file = File.ReadAllBytes(path);
            var entities = ExcelHelper.Load<TEntity, TConfiguration>(file);
            _context.AddRange(entities);
            await _context.SaveChangesAsync();
        }
    }
}