using MediatR;
using Microsoft.EntityFrameworkCore;
using RoomOccupancy.Application.Exceptions;
using RoomOccupancy.Application.Interfaces;
using RoomOccupancy.Application.Interfaces.Users;
using RoomOccupancy.Application.Reservations.Queries.GetUserReservations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RoomOccupancy.Application.Reservations.Commands.CancelReservation
{
    public class CancelReservationCommand: IRequest<Unit>
    {
        public int Id { get; set; }

        public class Handler : IRequestHandler<CancelReservationCommand, Unit>
        {
            private readonly IUserService userService;
            private readonly IReservationDbContext context;

            public Handler(IUserService userService, IReservationDbContext context)
            {
                this.userService = userService;
                this.context = context;
            }
            public async Task<Unit> Handle(CancelReservationCommand request, CancellationToken cancellationToken)
            {
                var user = await userService.GetUser() ?? throw new UnauthorisedUserException();

                var reservation = await context.Reservations.FirstOrDefaultAsync(x => x.AppUserId == user.Id && x.Id == request.Id);

                if (reservation == null)
                    return Unit.Value;

                reservation.IsCancelled = true;

                await context.SaveChangesAsync();

                return Unit.Value;
            }
        }
    }
}
