using AutoMapper;
using AutoMapper.QueryableExtensions;
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

namespace RoomOccupancy.Application.Campus.Faculties.Queries.GetFaculties
{
    public class GetFacultiesQuery: IRequest<List<FacultyModel>>
    {
        public class Handler : IRequestHandler<GetFacultiesQuery, List<FacultyModel>>
        {
            private readonly IMapper mapper;
            private readonly IReservationDbContext context;

            public Handler(IMapper mapper, IReservationDbContext context)
            {
                this.mapper = mapper;
                this.context = context;
            }
            public Task<List<FacultyModel>> Handle(GetFacultiesQuery request, CancellationToken cancellationToken)
            {
                return context.Faculties
                    .AsNoTracking()
                    .ProjectTo<FacultyModel>(mapper.ConfigurationProvider)
                    .ToListAsync();
            }
        }
    }
}
