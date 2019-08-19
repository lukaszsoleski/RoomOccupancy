using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RoomOccupancy.Application.Campus.Rooms.Queries.GetRoomsList;
using RoomOccupancy.Application.Interfaces;
using RoomOccupancy.Domain.Entities.Campus;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace RoomOccupancy.Application.Campus.Rooms.Queries
{
    public class GetRoomsQuery : IRequest<RoomsListViewModel>
    {
        public Expression<Func<Room, bool>> ValuePropertyFilter { get; set; }

        /// <summary>
        /// Additionally adds building number to the label of the <see cref="RoomLookupModel"/>.
        /// </summary>
        public bool IncludeBuildingNo { get; set; }

        public class Handler : IRequestHandler<GetRoomsQuery, RoomsListViewModel>
        {
            private readonly IMapper _mapper;
            private readonly IReservationDbContext _context;

            public Handler(IMapper mapper, IReservationDbContext context)
            {
                _mapper = mapper;
                _context = context;
            }

            public async Task<RoomsListViewModel> Handle(GetRoomsQuery request, CancellationToken cancellationToken)
            {
                var select = _context.Rooms
                    .AsNoTracking();

                if (request.ValuePropertyFilter != null)
                    select = select.Where(request.ValuePropertyFilter);

                var rooms = await select
                    .ProjectTo<RoomLookupModel>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);

                await AssignBuildingNumber(request, rooms);

                return new RoomsListViewModel() { Rooms = rooms };
            }

            private async Task AssignBuildingNumber(GetRoomsQuery request, System.Collections.Generic.List<RoomLookupModel> rooms)
            {
                if (request.IncludeBuildingNo)
                {
                    var buildings = await _context.Buildings.AsNoTracking().ToListAsync();
                    rooms.ForEach(room =>
                    {
                        var building = buildings.FirstOrDefault(x => x.Id == room.BuildingId);
                        if (building == null) return;
                        room.Label = $"{room.Label} bud. {building.Number}";
                    });
                }
            }
        }
    }
}