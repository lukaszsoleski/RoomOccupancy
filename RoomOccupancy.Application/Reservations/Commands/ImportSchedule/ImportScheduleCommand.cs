using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using MoreLinq;
using RoomOccupancy.Application.Exceptions;
using RoomOccupancy.Application.Infrastructure.Mapping;
using RoomOccupancy.Application.Infrastructure.Mapping.Excel;
using RoomOccupancy.Application.Interfaces;
using RoomOccupancy.Common;
using RoomOccupancy.Domain.Entities.Campus;
using RoomOccupancy.Domain.Entities.Reservation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RoomOccupancy.Application.Reservations.Commands.ImportSchedule
{
    public class ImportScheduleCommand : IRequest
    {
        /// <summary>
        /// Content of the excel data sheet file.
        /// </summary>
        public byte[] FileContent { get; set; }

        public class Handler : IRequestHandler<ImportScheduleCommand, Unit>
        {
            private readonly IMapper _mapper;
            private readonly IReservationDbContext _context;

            public Handler(IReservationDbContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;
            }

            public async Task<Unit> Handle(ImportScheduleCommand request, CancellationToken cancellationToken)
            {
                var importedSchedule = ExcelHelper.Load<Reservation, ReservationClassMap>(request.FileContent).ToList();

                if (!importedSchedule.Any())
                    return Unit.Value;

                var rooms = await LoadRequiredRooms(importedSchedule);
                var reservations = await _context.Reservations
                    .AsNoTracking()
                    .ToListAsync();

                var roomIds = rooms.Select(x => x.Id).ToList();
                
                foreach (var reservation in importedSchedule)
                {
                    var room = GetRoom(rooms, reservation);

                    await ValidateCollitions(reservation, room);

                    AddReservation(reservation, room);

                }

                return Unit.Value;
            }

            private async Task ValidateCollitions(Reservation reservation, Room room)
            {
                var collitions = await _context.Reservations
                    .AsNoTracking()
                    .Where(x => HasTimeCollision(x, room, reservation))
                    .ToListAsync();
                if (collitions.Any())
                {
                    // check cancellation date
                    foreach (var collition in collitions)
                    {

                    }
                }
            }

            private static bool HasTimeCollision(Reservation x, Room room, Reservation newR)
            {
                return x.RoomId == room.Id
                    && !x.IsCancelled
                    && x.ReservationDays.HasAny(newR.ReservationDays)
                    && (        // starts in the middle
                    (newR.Start.IsTimeLaterThanInclusive(x.Start) && newR.Start.IsTimeEarlierThan(x.End))
                        ||     // ends too late
                    (newR.End.IsTimeLaterThan(x.Start) && newR.End.IsTimeEarlierThanInclusive(x.End))
                        ||     // starts before and ends after
                    (newR.Start.IsTimeEarlierThanInclusive(x.Start) && newR.End.IsTimeLaterThanInclusive(x.End))
                    );
            }

            private void AddReservation(Reservation reservation, Room room)
            {
                reservation.Room = room;
                reservation.RoomId = room.Id;
                reservation.IsCyclical = true;

                _context.Reservations.Add(reservation);
            }

            private static Room GetRoom(List<Room> rooms, Reservation reservation)
            {
                return rooms.FirstOrDefault(x => reservation.Room.Number.Contains($"{x.Number}")
                                            && reservation.Room.Building.Number == x.Building.Number)
                                                ?? throw new NotFoundException(typeof(Room),
                                                    $"number={reservation.Room.Number}, building={reservation.Room.Building.Number}");
            }

            private async Task<List<Room>> LoadRequiredRooms(List<Reservation> importedSchedule)
            {
                var @params = importedSchedule
                   .DistinctBy(x => x.Room.Number)
                   .Select(x =>
                   {
                       var roomNum = x.Room.Number;
                       // default sheet column format $"{buildingNo}/{roomNo}"
                       if (roomNum.Contains('/'))
                       {
                           roomNum = roomNum.Split('/').LastOrDefault()?.Trim();
                       }

                       return new { BuildingNumber = x.Room.Building.Number, RoomNumber = roomNum };
                   });
                var rooms = await _context.Rooms.Include(x => x.Building)
                    .Where(x => @params
                        .Any(p => p.RoomNumber.Equals(x.Number, StringComparison.OrdinalIgnoreCase) && p.BuildingNumber == x.Building.Number))
                    .ToListAsync();
                return rooms;
            }


        }
    }
}