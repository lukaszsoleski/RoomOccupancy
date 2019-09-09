using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RoomOccupancy.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RoomOccupancy.Application.Campus.Equipment.Queries
{
    public class GetRoomEquipmentQuery : IRequest<List<EquipmentModel>>
    {
        /// <summary>
        /// if room id is specified the query will return all equipment for this room.
        /// </summary>
        public int RoomId { get; set; }
        public class Handler : IRequestHandler<GetRoomEquipmentQuery, List<EquipmentModel>>
        {
            private readonly IReservationDbContext context;
            private readonly IMapper mapper;

            public Handler(IReservationDbContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }
            public Task<List<EquipmentModel>> Handle(GetRoomEquipmentQuery request, CancellationToken cancellationToken)
            {
                return context.RoomEquipment
                    .AsNoTracking()
                    .Include(x => x.Equipment)
                    .Where(x => x.RoomId == request.RoomId)
                    .ProjectTo<EquipmentModel>(mapper.ConfigurationProvider)
                    .ToListAsync();
            }
        }
    }
}
