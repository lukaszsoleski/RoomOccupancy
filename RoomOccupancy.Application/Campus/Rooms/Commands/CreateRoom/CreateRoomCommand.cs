using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Newtonsoft.Json;
using RoomOccupancy.Application.Exceptions;
using RoomOccupancy.Application.Interfaces;
using RoomOccupancy.Domain.Entities.Campus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RoomOccupancy.Application.Campus.Rooms.Commands.CreateRoom
{
    public class CreateRoomCommand : IRequest<int>, IMapTo<Room>
    {
        public CreateRoomCommand()
        {
            Faculties = new List<int>();
        }

        public string Name { get; set; }
        public float? Space { get; set; }
        public int? Seats { get; set; }
        public string DesignatedUse { get; set; }
        public string ActualUse { get; set; }
        public string Description { get; set; }
        public int Floor { get; set; }
        public int BuildingId { get; set; }
        public IEnumerable<int> Faculties { get; set; }
        public int? DisponentId { get; set; }

        public override string ToString() => JsonConvert.SerializeObject(this);

        public class Handler : IRequestHandler<CreateRoomCommand, int>
        {
            private readonly IMapper _mapper;
            private readonly IMediator _mediator;
            private readonly IReservationDbContext _dbContext;

            public Handler(IMapper mapper, IMediator mediator, IReservationDbContext dbContext)
            {
                _mapper = mapper;
                _mediator = mediator;
                _dbContext = dbContext;
            }

            public async Task<int> Handle(CreateRoomCommand request, CancellationToken cancellationToken)
            {
                var room = _mapper.Map<Room>(request);

                room.Building = await _dbContext.Buildings.FindAsync(room.BuildingId)
                    ?? throw new NotFoundException(typeof(Building).Name, room.BuildingId);

                if (room.Faculties.Any())
                {
                    var faculties = await _dbContext.Faculties
                        .Where(x => room.Faculties.Select(i => i.Id).Contains(x.Id))
                        .ToListAsync();
                    faculties.ForEach(x => room.Faculties.Add(x));
                }
                if (room.DisponentId.HasValue)
                {
                    room.Disponent = await _dbContext.Disponents.FindAsync(room.DisponentId)
                        ?? throw new NotFoundException(typeof(Disponent).Name, room.DisponentId);
                }
                _dbContext.Rooms.Add(room);

                await _dbContext.SaveChangesAsync(cancellationToken);

                var roomCreatedEvent = _mapper.Map<RoomCreated>(room);

                await _mediator.Publish(roomCreatedEvent);

                return room.Id;
            }
        }
    }
}