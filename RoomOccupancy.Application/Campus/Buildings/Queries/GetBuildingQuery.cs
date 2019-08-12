using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RoomOccupancy.Application.Exceptions;
using RoomOccupancy.Application.Interfaces;
using RoomOccupancy.Domain.Entities.Campus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RoomOccupancy.Application.Campus.Buildings.Queries
{
    public class GetBuildingQuery : IRequest<BuildingModel>
    {
        public int Number { get; set; }

        public class Handler : IRequestHandler<GetBuildingQuery, BuildingModel>
        {
            private readonly IReservationDbContext _context;
            private readonly IMapper _mapper;

            public Handler(IReservationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper; 
            }
            public async Task<BuildingModel> Handle(GetBuildingQuery request, CancellationToken cancellationToken)
            {
                var building = await _context.Buildings.FirstOrDefaultAsync(x => x.Number == request.Number)
                        ?? throw new NotFoundException($"{typeof(Building)} with property {nameof(request.Number)}", request.Number);

                return _mapper.Map<BuildingModel>(building);

            }
        }
    }
}
