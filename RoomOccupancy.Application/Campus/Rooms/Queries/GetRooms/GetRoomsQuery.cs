using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RoomOccupancy.Application.Interfaces;
using RoomOccupancy.Domain.Entities.Campus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RoomOccupancy.Application.Campus.Rooms.Queries
{
    public class GetRoomsQuery : IRequest<IEnumerable<RoomModel>>
    {
        public Expression<Func<Room,bool>> Expression { get; set; }

        public class Handler : IRequestHandler<GetRoomsQuery, IEnumerable<RoomModel>>
        {
            private readonly IMapper _mapper;
            private readonly IReservationDbContext _context;

            public Handler(IMapper mapper, IReservationDbContext context)
            {
                _mapper = mapper;
                _context = context;
            }
            public async Task<IEnumerable<RoomModel>> Handle(GetRoomsQuery request, CancellationToken cancellationToken)
            {
                if (request.Expression == null)
                    throw new ArgumentNullException($"Expression {request.Expression.Type.Name} cannot be null.");

                return await _context.Rooms.Where(request.Expression)
                    .ProjectTo<RoomModel>()
                    .ToListAsync(cancellationToken);

            }

           
        }
    }
}
