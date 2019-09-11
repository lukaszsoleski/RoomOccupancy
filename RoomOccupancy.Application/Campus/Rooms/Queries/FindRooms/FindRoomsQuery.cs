using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using RoomOccupancy.Application.Campus.Rooms.Queries.GetRoomsList;
using RoomOccupancy.Application.Exceptions;
using RoomOccupancy.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RoomOccupancy.Application.Campus.Rooms.Queries.FindRooms
{
    using MomentSharp;
    using RoomOccupancy.Application.Notifications;
    using RoomOccupancy.Domain.Entities.Campus;
    using System.Collections;

    public class FindRoomsQuery : IRequest<FindRoomsResult>
    {
        public string RoomName { get; set; }
        public int? Faculty { get; set; }
        public int[] Equipment { get; set; }
        public int? Seats { get; set; }

        public class Handler : IRequestHandler<FindRoomsQuery, FindRoomsResult>
        {
            private readonly IReservationDbContext context;
            private readonly IMapper mapper;

            public Handler(IReservationDbContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }
            public async Task<FindRoomsResult> Handle(FindRoomsQuery request, CancellationToken cancellationToken)
            {
                var vm = new FindRoomsResult();

                var queryable = context.Rooms.AsNoTracking();
                bool filterByEquipment = request.Equipment != null && request.Equipment.Any();
                bool filterByFaculty = request.Faculty.HasValue;
                if (filterByEquipment)
                {
                    queryable = queryable.Include(x => x.Equipment);
                }
                if (filterByFaculty)
                {
                    queryable = queryable.Include(x => x.Faculties);
                }
                if (!string.IsNullOrEmpty(request.RoomName))
                {
                    queryable = queryable.Where(x => x.ActualUse == request.RoomName);
                }
                if (request.Seats.HasValue)
                {
                    queryable = queryable.Where(x => x.Seats >= request.Seats);
                }
                var rooms = await queryable.ToListAsync();

                if (filterByFaculty)
                {
                    rooms = rooms.Where(x => x.Faculties.Any(f => f.FacultyId == request.Faculty)).ToList();
                    if (!rooms.Any())
                        return CreateResponse(rooms, "Nie znaleziono pomieszczeń dla podanego wydziału, zastosowania oraz ilości miejsc.");
                }
                if (filterByEquipment)
                {
                    rooms = FindRoomsByEquipment(rooms,request.Equipment);
                  
                    var noResultMessage = await EquipmentHasEmptyResult(rooms, request.Equipment);

                    if (noResultMessage != null)
                    {
                        return CreateResponse(rooms, noResultMessage);
                    }
                }
               
                return CreateResponse(rooms, null);
            }
            private List<Room> FindRoomsByEquipment(List<Room> rooms, int[] equipment)
            {
                return rooms.Where(x => !equipment.Except(x.Equipment.Select(e => e.EquipmentId)).Any()).ToList();
            }
            private FindRoomsResult CreateResponse(List<Room> rooms, string message)
            {
                var vm = new FindRoomsResult();
                if (rooms != null && rooms.Any())
                {
                    vm.Rooms = mapper.Map<List<RoomLookupModel>>(rooms);
                }
                else
                {
                    vm.Rooms = new List<RoomLookupModel>();
                }
                vm.NoResultMessage = message;

                return vm;
            }
            private async Task<string> EquipmentHasEmptyResult(List<Room> rooms, int[] equipmentId)
            {
                if (rooms.Any())
                {
                    return null;
                }
                var equipment = await context.Equipment
                    .AsNoTracking()
                    .Where(x => equipmentId.Contains(x.Id))
                    .Select(x => x.Name)
                    .ToListAsync();

                var names = string.Join(", ", equipment);

                return $"Nie znaleziono pomieszczeń z wyposażeniem {names}.";
            }

        }


    }
}
