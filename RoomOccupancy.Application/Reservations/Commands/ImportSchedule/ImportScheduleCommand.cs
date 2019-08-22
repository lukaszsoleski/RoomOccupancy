using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using MoreLinq;
using RoomOccupancy.Application.Exceptions;
using RoomOccupancy.Application.Infrastructure.Mapping;
using RoomOccupancy.Application.Infrastructure.Mapping.Excel;
using RoomOccupancy.Application.Interfaces;
using RoomOccupancy.Application.Reservations.Queries.ValidateCollitions;
using RoomOccupancy.Common;
using RoomOccupancy.Common.Extentions;
using RoomOccupancy.Domain.Entities.Campus;
using RoomOccupancy.Domain.Entities.Reservation;
using System;
using System.Collections;
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
            private readonly IMediator _mediator;
            private readonly IReservationDbContext _context;

            public Handler(IReservationDbContext context, IMapper mapper, IMediator mediator)
            {
                _mapper = mapper;
                _mediator = mediator;
                _context = context;
            }

            public async Task<Unit> Handle(ImportScheduleCommand request, CancellationToken cancellationToken)
            {
                var importedReservations = ExcelHelper.Load<ReservationImportModel, ReservationClassMap>(request.FileContent).ToList();


                if (!importedReservations.Any())
                    return Unit.Value;

                var reservations = Map(importedReservations).ToList();
                    

                var rooms = await LoadRequiredRooms(reservations);
                
                foreach (var reservation in reservations)
                {
                    var room = GetRoom(rooms, reservation);

                    var collitions = await _mediator.Send(new GetReservationCollitionsQuery() {  Room = room, Reservation = reservation });
                    if (collitions.Any())
                        throw new ReservationCollitionException(collitions.FirstOrDefault(), reservation);
                    AddReservation(reservation, room);
                }
                await _context.SaveChangesAsync(); 

                return Unit.Value;
            }

            public IEnumerable<Reservation> Map(IEnumerable<ReservationImportModel> reservations)
            {
                foreach (var item in reservations)
                {
                    yield return item.Map();
                }
            }



            private void AddReservation(Reservation reservation, Room room)
            {
                reservation.Room = room;
                reservation.RoomId = room.Id;
                if(reservation.ReservationDays != DaysOfWeek.None)
                    reservation.IsCyclical = true;

                _context.Reservations.Add(reservation);
            }

            private static Room GetRoom(List<Room> rooms, Reservation reservation)
            {
                var reservationRooms = rooms.Where(x => reservation.Room.Number.Contains($"{x.Number}")
                                            && reservation.Room.Building.Number == x.Building.Number).ToList();
                                  
                if(reservationRooms.Count() > 1)
                {
                    reservationRooms.RemoveAll(x => x.Floor != reservation.Room.Floor);
                }
                
                return reservationRooms.FirstOrDefault()
                    ?? throw new NotFoundException(typeof(Room),
                                                    $"number={reservation.Room.Number}, building={reservation.Room.Building.Number}");
            }

            private async Task<List<Room>> LoadRequiredRooms(List<Reservation> importedSchedule)
            {
                // filter rooms
                var reservationRooms = importedSchedule
                   .Select(x => new { BuildingNumber = x.Room.Building.Number, RoomNumber = x.Room.Number, })
                   .Distinct()
                   .ToList();
                // load using filter
                var rooms = await _context.Rooms.Include(x => x.Building)
                    .Where(room =>
                    reservationRooms
                        .Any(rR => room.Building.Number == rR.BuildingNumber
                            && rR.RoomNumber.Equals(room.Number, StringComparison.OrdinalIgnoreCase)
                            ))
                    .ToListAsync();

                return rooms;
            }
        }
    }
}