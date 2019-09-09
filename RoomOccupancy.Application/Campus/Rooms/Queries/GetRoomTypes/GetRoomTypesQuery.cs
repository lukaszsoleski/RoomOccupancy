using MediatR;
using Microsoft.EntityFrameworkCore;
using RoomOccupancy.Application.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RoomOccupancy.Application.Campus.Rooms.Queries.GetRoomTypes
{
    public class GetRoomTypesQuery : IRequest<List<string>>
    {
        public class Handler : IRequestHandler<GetRoomTypesQuery, List<string>>
        {
            private readonly IReservationDbContext context;

            public Handler(IReservationDbContext context)
            {
                this.context = context;
            }
            public Task<List<string>> Handle(GetRoomTypesQuery request, CancellationToken cancellationToken)
            {
                return context
                    .Rooms
                    .AsNoTracking()
                    .Where(x => x.Seats > 0)
                    .GroupBy(x => x.ActualUse)
                    .Select(x => x.FirstOrDefault())
                    .Select(x => x.ActualUse)
                    .ToListAsync();
            }
        }
    }
}
