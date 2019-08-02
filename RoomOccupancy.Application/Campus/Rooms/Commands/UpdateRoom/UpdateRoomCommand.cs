using AutoMapper;
using MediatR;
using RoomOccupancy.Application.Exceptions;
using RoomOccupancy.Application.Interfaces;
using RoomOccupancy.Domain.Entities.Campus;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RoomOccupancy.Application.Campus.Rooms
{
    public class UpdateRoomCommand : IRequest, IMapTo<Room>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float? Space { get; set; }
        public int? Seats { get; set; }
        public string DesignatedUse { get; set; }
        public string ActualUse { get; set; }
        public string Description { get; set; }
        public int Floor { get; set; }
        public int BuildingId { get; set; }
        public int? FacultyId { get; set; }


        public class Handler : IRequestHandler<UpdateRoomCommand, Unit>
        {
            private readonly IReservationDbContext _context;
            private readonly IMapper _mapper;

            public Handler(IReservationDbContext context,IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<Unit> Handle(UpdateRoomCommand request, CancellationToken cancellationToken)
            {
                var room = await _context.Rooms.FindAsync(request.Id)
                    ?? throw new NotFoundException(typeof(Room).Name, request.Id);

                _mapper.Map(request, room);

                if (request.FacultyId.HasValue)
                {
                    room.Faculty = await _context.Faculties.FindAsync(request.FacultyId)
                        ?? throw new NotFoundException(typeof(Faculty).Name, request.FacultyId);
                }

                await _context.SaveChangesAsync();

                return Unit.Value;
            }
        }
    }
}
