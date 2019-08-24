using AutoMapper;
using MediatR;
using RoomOccupancy.Application.Exceptions;
using RoomOccupancy.Application.Interfaces;
using RoomOccupancy.Domain.Entities.Campus;
using System.Threading;
using System.Threading.Tasks;

namespace RoomOccupancy.Application.Campus.Rooms.Queries.GetRoom
{
    public class GetRoomDetailQuery : IRequest<RoomModel>
    {
        public int Id { get; set; }

        public class Handler : IRequestHandler<GetRoomDetailQuery, RoomModel>
        {
            private readonly IMapper _mapper;
            private readonly IReservationDbContext _context;

            public Handler(IMapper mapper, IReservationDbContext context)
            {
                _mapper = mapper;
                _context = context;
            }

            public async Task<RoomModel> Handle(GetRoomDetailQuery request, CancellationToken cancellationToken)
            {
                var room = await _context.Rooms.FindAsync(request.Id) ?? throw new NotFoundException(typeof(Room), request.Id);

                var roomModel = _mapper.Map<RoomModel>(room);

                roomModel.BuildingNumber = (await _context.Buildings.FindAsync(room.BuildingId)).Number;

                return roomModel;
            }
        }
    }
}