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
    public class GetEquipmentQuery : IRequest<List<EquipmentModel>>
    {
        public int? RoomId { get; set; }
        public int? EquipmentId { get; set; }

        public class Handler : IRequestHandler<GetEquipmentQuery, List<EquipmentModel>>
        {
            private readonly IReservationDbContext context;
            private readonly IMapper mapper;

            public Handler(IReservationDbContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }
            public async Task<List<EquipmentModel>> Handle(GetEquipmentQuery request, CancellationToken cancellationToken)
            {
                
                var equipmentQuery = context.RoomEquipment
                    .AsNoTracking()
                    .Where(x => x.RoomId == request.RoomId || x.EquipmentId == request.EquipmentId);

                if (!request.RoomId.HasValue && !request.EquipmentId.HasValue)
                {
                    equipmentQuery = context.RoomEquipment.AsNoTracking();
                }

                var equipment = await equipmentQuery.Include(x => x.Equipment).ProjectTo<EquipmentModel>(mapper.ConfigurationProvider).ToListAsync();

                return equipment;
            }
        }
    }
}
