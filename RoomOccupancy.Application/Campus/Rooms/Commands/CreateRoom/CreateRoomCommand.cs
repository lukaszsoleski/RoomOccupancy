using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using RoomOccupancy.Application.Interfaces;
using RoomOccupancy.Domain.Entities.Campus;
using Newtonsoft.Json;

namespace RoomOccupancy.Application.Campus.Rooms.Commands.CreateRoom
{
    /* 
     callstack: 

    CreateRoomCommandValidator ctor
    RequestValidationBehaviour ctor
    RequestLogger Process
    RequestPerformanceBehaviour Handle => await next()
	    RequestValidationBehaviour Handle
	    MappingProfile ctor
	    CreateRoomCommandHandler
            RoomCreated
            NotificationService
         */
    public class CreateRoomCommand : IRequest
    {
        public string Name { get; set; }
        public float? Space { get; set; }
        public int? Seats { get; set; }
        public string DesignatedUse { get; set; }
        public string ActualUse { get; set; }
        public string Description { get; set; }
        public int Floor { get; set; }
        public int BuildingId { get; set; }
        public int? FacultyId { get; set; }

        public override string ToString() =>  JsonConvert.SerializeObject(this);

        public class Handler : IRequestHandler<CreateRoomCommand, Unit>
        {
            private readonly IMapper _mapper;
            private readonly IMediator _mediator;
            private readonly IReservationDbContext _dbContext;

            public Handler(IMapper mapper ,IMediator mediator, IReservationDbContext dbContext)
            {
                _mapper = mapper;
                _mediator = mediator;
                _dbContext = dbContext;
            }
            public async Task<Unit> Handle(CreateRoomCommand request, CancellationToken cancellationToken)
            {
                var room = _mapper.Map<Room>(request);

               // _dbContext.Rooms.Add(room);

                await _dbContext.SaveChangesAsync(cancellationToken);

                await _mediator.Publish(new RoomCreated{ RoomId = 1 });

                return Unit.Value;
            }
        }


    }
   
}
