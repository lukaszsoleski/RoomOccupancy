using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using RoomOccupancy.Application.Exceptions;
using RoomOccupancy.Application.Interfaces;
using RoomOccupancy.Application.Interfaces.Users;
using RoomOccupancy.Application.Notifications;
using RoomOccupancy.Application.Reservations.Queries.ValidateCollitions;
using RoomOccupancy.Domain.Entities.Reservation;
using RoomOccupancy.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RoomOccupancy.Application.Reservations.Commands.CreateReservation
{
    public class CreateReservationCommand : IRequest<int?>
    {
        public ReservationModel Reservation { get; set; }
        public string UserId { get; set; }
        public class Handler : IRequestHandler<CreateReservationCommand, int?>
        {
            private readonly IReservationDbContext context;
            private readonly IMapper mapper;
            private readonly IMediator mediator;
            private readonly INotificationService notificationService;
            private readonly IUserService userService;

            private AppUser user;

            public Handler(IReservationDbContext context, IMapper mapper, IMediator mediator, INotificationService notificationService, IUserService userService)
            {
                this.context = context;
                this.mapper = mapper;
                this.mediator = mediator;
                this.notificationService = notificationService;
                this.userService = userService;
            }
            public async Task<int?> Handle(CreateReservationCommand request, CancellationToken cancellationToken)
            {
                var reservation = mapper.Map<Reservation>(request.Reservation);
                user = await userService.GetUser();
                var conflicts = await mediator.Send(new GetReservationConflictsQuery() { Reservation = reservation, RoomId = reservation.RoomId });

                if (conflicts.Any())
                    throw new ReservationConflictException($"{string.Join(", ", conflicts.Select(x => x.ToString()))}", $"{reservation}");

                if (! await IsUserAuthorized(reservation))
                {
                    reservation.AwaitsAcceptance = true;
                    await SendReservationRequest(reservation, request.UserId);
                }
                if (user != null)
                    reservation.AppUser = user;

                context.Reservations.Add(reservation);

                await context.SaveChangesAsync();

                return reservation.Id;
            }


            private async Task<bool> IsUserAuthorized(Reservation reservation)
            {
                if (user == null)
                    return false;

                var userFaculty = user.FacultyId;

                if (!userFaculty.HasValue)
                    return false;

                var roomFacultyIds = await context.FacultyRooms
                    .Where(x => x.RoomId == reservation.RoomId)
                    .Select(x => x.FacultyId)
                    .ToListAsync();

                return roomFacultyIds.Contains(userFaculty.Value);
            }

            private async Task SendReservationRequest(Reservation reservation, string userId)
            {
            }
        }
    }
}
