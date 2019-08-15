using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RoomOccupancy.Application.Exceptions;
using RoomOccupancy.Application.Interfaces;
using RoomOccupancy.Domain.Entities.Campus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RoomOccupancy.Application.Campus.Rooms.Commands.UpdateRoom
{
    public class UpdateRoomCommand : IRequest, IMapTo<Room>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }
        public float? Space { get; set; }
        public int? Seats { get; set; }
        public string DesignatedUse { get; set; }
        public string ActualUse { get; set; }
        public string Description { get; set; }
        public int Floor { get; set; }
        public int BuildingId { get; set; }
        public ICollection<int> Faculties { get; set; }
        public int? DisponentId { get; set; }

        public bool ShouldUpdateFaculties { get; set; }
        public class Handler : IRequestHandler<UpdateRoomCommand, Unit>
        {
            private readonly IReservationDbContext _context;
            private readonly IMapper _mapper;

            public Handler(IReservationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Unit> Handle(UpdateRoomCommand request, CancellationToken cancellationToken)
            {
                var room = await _context.Rooms.Include(x => x.Faculties).FirstOrDefaultAsync(x => x.Id == request.Id)
                    ?? throw new NotFoundException(typeof(Room).Name, request.Id);

                _mapper.Map(request, room);

                #region TODO: [Faculties] compare collections and decide if update is required
                if(request.ShouldUpdateFaculties)
                {
                    room.Faculties.Clear();
                    //todo check FacultyRoom entity behavior
                    var roomFaculty = await _context.FacultyRooms
                        .Where(x => request.Faculties.Contains(x.FacultyId))
                        .ToListAsync();

                    _context.FacultyRooms.RemoveRange(roomFaculty);

                    var faculties = await _context.Faculties.Where(x => request.Faculties.Contains(x.Id)).ToListAsync();

                    faculties.ForEach(x => room.Faculties.Add(new FacultyRoom() { Faculty = x, Room = room }));

                }

                #endregion TODO: [Faculties] compare collections and decide if update is required

                if (request.DisponentId.HasValue)
                {
                    room.Disponent = await _context.Disponents.FindAsync(request.DisponentId)
                        ?? throw new NotFoundException(typeof(Disponent).Name, request.DisponentId);
                }
                await _context.SaveChangesAsync();

                return Unit.Value;
            }
        }
    }
}