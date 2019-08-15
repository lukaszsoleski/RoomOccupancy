using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using MoreLinq;
using RoomOccupancy.Application.Exceptions;
using RoomOccupancy.Application.Infrastructure.Mapping;
using RoomOccupancy.Application.Interfaces;
using RoomOccupancy.Domain.Entities.Campus;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RoomOccupancy.Application.Campus.Rooms.Commands.ImportRooms
{
    public class ImportRoomsCommand : IRequest
    {
        public byte[] File { get; set; }

        public class Handler : IRequestHandler<ImportRoomsCommand, Unit>
        {
            /// <summary>
            /// Zero based sheet row heading index.
            /// </summary>
            private int headingIndex = 1;

            private readonly IReservationDbContext _context;

            public Handler(IReservationDbContext context, IMediator mediator)
            {
                _context = context;
            }

            public async Task<Unit> Handle(ImportRoomsCommand request, CancellationToken cancellationToken)
            {
                var importedRooms = ExcelHelper.Load<Room, RoomClassMap>(request.File);

                // load cache
                var buildingsCache = await _context.Buildings.ToListAsync();

                var disponentsCache = await _context.Disponents.ToListAsync();

                var facultiesCache = await _context.Faculties.ToListAsync();

                ImportAll(importedRooms, buildingsCache, disponentsCache, facultiesCache);

                await _context.SaveChangesAsync();

                return Unit.Value;
            }

            private void ImportAll(IEnumerable<Room> importedRooms, List<Building> buildingsCache, List<Disponent> disponentsCache, List<Faculty> facultiesCache)
            {
                foreach (var room in importedRooms)
                {
                    // building should be in database
                    room.Building = buildingsCache.FirstOrDefault(x => x.Number == room.Building.Number)
                        ?? throw new NotFoundException($"{typeof(Building)} with property {nameof(room.Building.Number)}", room.Building.Number);

                    Disponent disponent = AssignDisponent(disponentsCache, room);

                    AssignFaculty(facultiesCache, room, disponent);

                    _context.Rooms.Add(room); 
                   
                }
            }

            private Disponent AssignDisponent(List<Disponent> disponentsCache, Room room)
            {
                var disponent = disponentsCache.FirstOrDefault(x => x.Name.Equals(room.Disponent.Name, StringComparison.OrdinalIgnoreCase));

                if (disponent is null)
                {
                    disponent = room.Disponent;

                    // change object state to added.
                    _context.Disponents.Add(disponent);

                    // update cache
                    disponentsCache.Add(disponent);
                }

                room.Disponent = disponent;

                return disponent;
            }

            private void AssignFaculty(List<Faculty> facultiesCache, Room room, Disponent disponent)
            {
                var importedFaculties = disponent.Name.Split('/').Select(x => x.Trim());
                var lookupFacultyNames = new List<string>(); 
                foreach (var facultyName in importedFaculties)
                {
                    var faculty = facultiesCache.
                             FirstOrDefault(x => x.Name.Equals(facultyName,StringComparison.OrdinalIgnoreCase) 
                                || x.Acronym.Equals(facultyName,StringComparison.OrdinalIgnoreCase));
                        
                    if (faculty is null)
                    {
                        continue;
                    }

                    var newFacultyRoom = new FacultyRoom() { Faculty = faculty, Room = room };

                    room.Faculties.Add(newFacultyRoom);

                    lookupFacultyNames.Add(faculty.Acronym); 
                }
                if(lookupFacultyNames.Any())
                     room.FacultyLookup = string.Join(" / ", lookupFacultyNames); 
            }
        }
    }
}