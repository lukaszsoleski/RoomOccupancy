using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RoomOccupancy.Application.Exceptions;
using RoomOccupancy.Application.Interfaces;
using RoomOccupancy.Application.Interfaces.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RoomOccupancy.Application.Reservations.Queries.GetUserReservations
{
    public class GetUserReservationsQuery: IRequest<List<ReservationModel>>
    {
        public class Handler : IRequestHandler<GetUserReservationsQuery, List<ReservationModel>>
        {
            private readonly IUserService userService;
            private readonly IReservationDbContext context;
            private readonly IMapper mapper;

            public Handler(IUserService userService, IReservationDbContext context, IMapper mapper)
            {
                this.userService = userService;
                this.context = context;
                this.mapper = mapper;
            }
            public async Task<List<ReservationModel>> Handle(GetUserReservationsQuery request, CancellationToken cancellationToken)
            {
                var user = await userService.GetUser() ?? throw new UnauthorisedUserException();

                return await context.Reservations
                    .Where(x => x.AppUserId == user.Id)
                    .AsNoTracking()
                    .ProjectTo<ReservationModel>(mapper.ConfigurationProvider)
                    .ToListAsync();
            }
        }
    }
}
